using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using Domain;
using Domain.Entities;
using Domain.Entities.Activity;
using Domain.Models.Activity;
using Domain.Models.Common;
using Domain.Models.Goal;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Persistance;
using Services.Abstraction;

namespace Services
{
    public class ActivityService : IActivityService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ActivityService> _logger;
        private readonly ICommonServices _common;
        private readonly IUser _user;
        public ActivityService(AppDbContext appDbContext, ILogger<ActivityService> logger, ICommonServices common, IUser user)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _common = common;
            _user = user;
        }

        public async Task<List<ActivityTrackingResponseModel>> GetActivityTracking(int UserId)
        {
            List<ActivityTrackingResponseModel> lstActivityTrackingResponseModel = new List<ActivityTrackingResponseModel>();
            try
            {
                lstActivityTrackingResponseModel = (from A in _appDbContext.Set<ActivityTracking>() 
                                                    join T in _appDbContext.Set<ActivityType>() on A.activitytypeid equals T.id
                                                    join I in _appDbContext.Set<IntensityLevel>() on A.intensitylevelid equals I.id
                                                    join D in _appDbContext.Set<DailyActivityTracking>() on A.userdailytrackid equals D.id
                                                    where A.userid  == UserId  && A.status == "Active"

                                                    select new ActivityTrackingResponseModel()
                                                    {
                                                        Id = D.id,
                                                        ActivityTrackId = A != null ? A.id : 0,
                                                        BodyFat = D.bodyfat,
                                                        MuscleMass = D.musclemass,
                                                        UserId = A.userid,
                                                        IntensityLevelId = I != null ? I.id : 0,
                                                        IntensityLevelIdName = I != null ? I.intensityname : "",
                                                        ActivityTypeId = T != null ? T.id : 0,
                                                        ActivityTypeName = T != null ? T.activityname : "",
                                                        Date = D.infodate,
                                                        BodyWeight = D.weight,
                                                        ActivityName = A.activityname,
                                                        Duration = A.duration,
                                                        Reps = A.reps,
                                                        SetsInfo = A.setsinfo,
                                                        LiftWeight = A.weight,
                                                        CaloriesBurned = A.caloriesburned,
                                                        Notes = A.note,
                                                        Status = A.status

                                                    }).OrderByDescending(a => a.Id).ToList();

                return await Task.FromResult(lstActivityTrackingResponseModel);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetActivityTracking", ex.Message);
                ex.ToString();

            }
            return await Task.FromResult(lstActivityTrackingResponseModel);
        }
        public async Task<DatabaseResponse> InsertDailyActivityTracking(ActivitysTrackingInsertModel activityModel )
        {
            DatabaseResponse dbResponse = new DatabaseResponse();
            ErrorResponses errorResponse = new ErrorResponses();
            dbResponse.status = false;
            try
            {
                var validationResponse = _appDbContext.userdailytrack.Where(x => x.userid == activityModel.UserId && x.infodate == activityModel.Date && x.status == "Active").ToList();
                if (validationResponse.Count > 0)
                {
                    errorResponse.Success = false;
                    errorResponse.Error = new Error(GlobalVariables.DailyActivityAlreadyExist, GlobalVariables.DailyActivityAlreadyExistDescription);
                    _logger.LogError("Activity Already Exist", errorResponse);
                    dbResponse.status = false;
                    dbResponse.errorResponse = errorResponse;
                    return await Task.FromResult(dbResponse);
                }
                else
                {
                    var activityDailyTrack = new DailyActivityTracking
                    {
                        userid = activityModel.UserId,
                        infodate = activityModel.Date,
                        weight = activityModel.BodyWeight,
                        bodyfat = activityModel.BodyFat,
                        musclemass = activityModel.MuscleMass,
                        createdby = "Admin",
                        createdat = DateTime.Now,
                        status = "Active"
                    };
                    _appDbContext.Set<DailyActivityTracking>().Add(activityDailyTrack);
                    _appDbContext.SaveChanges();
                    int IDailyActivityID = await DailyActivityId(activityModel.UserId, activityModel.Date);
                    var ActivityRes = await InsertActivityTracking(activityModel, IDailyActivityID );
                    dbResponse.status = ActivityRes.status;
                    dbResponse.errorResponse = ActivityRes.errorResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: InsertDailyActivityTracking", ex.Message);
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.ErrorOnInsertingActivity, GlobalVariables.ErrorOnInsertingActivityDescription);
                dbResponse.status = false;
                dbResponse.errorResponse = errorResponse;
                return await Task.FromResult(dbResponse);
            }
            return await Task.FromResult(dbResponse);
        }
        public async Task<DatabaseResponse> InsertActivityTracking(ActivitysTrackingInsertModel activityModel, int IDailyActivityID )
        {
            DatabaseResponse dbResponse = new DatabaseResponse();
            ErrorResponses errorResponse = new ErrorResponses();
            IDbContextTransaction transactionLong = _appDbContext.Database.BeginTransaction();
            try
            {  
                ActivityTracking activityTracking = new ActivityTracking();

                for (int i = 0; i < activityModel.activityTotalData.Count(); i++)
                {
                    activityTracking.id = 0;
                    activityTracking.userid = activityModel.UserId;
                    activityTracking.userdailytrackid = IDailyActivityID;
                    activityTracking.activitytypeid = activityModel.activityTotalData[i].ActivityTypeId;
                    activityTracking.activityname = activityModel.activityTotalData[i].ActivityName;
                    activityTracking.duration = activityModel.activityTotalData[i].Duration;
                    activityTracking.reps = activityModel.activityTotalData[i].Reps;
                    activityTracking.setsinfo = activityModel.activityTotalData[i].SetsInfo;
                    activityTracking.weight = activityModel.activityTotalData[i].LiftWeight;
                    activityTracking.caloriesburned = activityModel.activityTotalData[i].CaloriesBurned;
                    activityTracking.intensitylevelid = activityModel.activityTotalData[i].IntensityLevelId;
                    activityTracking.note = activityModel.activityTotalData[i].Notes;
                    activityTracking.createdat = DateTime.UtcNow;
                    activityTracking.status = "Active";
                    activityTracking.createdby = "Admin";
                    _appDbContext.Set<ActivityTracking>().Add(activityTracking);
                    _appDbContext.SaveChanges();
                }
                transactionLong.Commit();
                dbResponse.status = true;
                 
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: InsertActivityTracking", ex.Message);
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.ErrorOnInsertingActivity, GlobalVariables.ErrorOnInsertingActivityDescription);
                dbResponse.status = false;
                dbResponse.errorResponse = errorResponse;
                return await Task.FromResult(dbResponse);
            }
            return await Task.FromResult(dbResponse);
        }

        public async Task<DatabaseResponse> UpdateActivityDailyTracking(ActivityTrackingResponseModel activityModel)
        {
            DatabaseResponse response = new DatabaseResponse();
            ErrorResponses errorResponse = new ErrorResponses();
            var dbResponse = _appDbContext.userdailytrack.Where(x => x.id == activityModel.Id && x.status == "Active").AsNoTracking().FirstOrDefault();

            if (dbResponse == null)
            {
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.ActivityUpdateNoData, GlobalVariables.ActivityUpdateNoDataDescription);
                _logger.LogError("There is no record found for this primary key", errorResponse);
                response.status = false;
                response.errorResponse = errorResponse;
                return await Task.FromResult(response);
            }
            else
            {
                try
                {
                    dbResponse.id = activityModel.Id;
                    dbResponse.userid = activityModel.UserId;
                    dbResponse.infodate = activityModel.Date;
                    dbResponse.weight = activityModel.BodyWeight;
                    dbResponse.bodyfat = activityModel.BodyFat;
                    dbResponse.musclemass = activityModel.MuscleMass;
                    dbResponse.modifiedby = "Admin";
                    dbResponse.modifiedat = DateTime.Now;
                    dbResponse.status = "Active";
                    _appDbContext.userdailytrack.Update(dbResponse);
                    _appDbContext.SaveChanges();
                    _appDbContext.Entry(dbResponse).State = EntityState.Detached;
                    var activitylRes = await UpdateActivityTracking(activityModel);
                    response.status = activitylRes.status;
                    response.errorResponse = activitylRes.errorResponse;
                    return await Task.FromResult(response);

                }
                catch (Exception ex)
                {

                    errorResponse.Success = false;
                    errorResponse.Error = new Error(GlobalVariables.ActivitysavechangesError, GlobalVariables.ActivitysavechangesErrorDescription);
                    _logger.LogError("There is no record found for this primary key", errorResponse);
                    response.status = false;
                    response.errorResponse = errorResponse;
                    return await Task.FromResult(response);
                }
            }
        }
        public async Task<DatabaseResponse> UpdateActivityTracking(ActivityTrackingResponseModel activityModel)
        {
            DatabaseResponse response = new DatabaseResponse();
            ErrorResponses errorResponse = new ErrorResponses();

            var dbResponse = _appDbContext.useractivitytrack.Where(x => x.userid == activityModel.UserId && x.id == activityModel.ActivityTrackId && x.status == "Active").AsNoTracking().FirstOrDefault();
            try
            {
                if (dbResponse != null)
                {
                    dbResponse.id = activityModel.ActivityTrackId;
                    dbResponse.userid = activityModel.UserId;
                    dbResponse.userdailytrackid = activityModel.Id;
                    dbResponse.activitytypeid = activityModel.ActivityTypeId;
                    dbResponse.activityname = activityModel.ActivityName;
                    dbResponse.duration = activityModel.Duration;
                    dbResponse.reps = activityModel.Reps;
                    dbResponse.setsinfo = activityModel.SetsInfo;
                    dbResponse.weight = activityModel.LiftWeight;
                    dbResponse.caloriesburned = activityModel.CaloriesBurned;
                    dbResponse.intensitylevelid = activityModel.IntensityLevelId;
                    dbResponse.note = activityModel.Notes;
                    dbResponse.modifiedby = "Admin";
                    dbResponse.modifiedat = DateTime.Now;
                    dbResponse.status = "Active";
                    _appDbContext.useractivitytrack.Update(dbResponse);
                    _appDbContext.SaveChanges();
                    response.status = true;
                    return await Task.FromResult(response);
                }
                else
                {
                    errorResponse.Success = false;
                    errorResponse.Error = new Error(GlobalVariables.ActivityUpdateNoData, GlobalVariables.ActivityUpdateNoDataDescription);
                    _logger.LogError("There is no record found for this primary key", errorResponse);
                    response.status = false;
                    response.errorResponse = errorResponse;
                    return await Task.FromResult(response);
                }
            }
            catch (Exception ex)
            {
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.MedicalInfoUpdateNoData, GlobalVariables.MedicalInfoUpdateNoDataDescription);
                _logger.LogError("There is no record found for this primary key", errorResponse);
                ex.ToString();
                response.status = false;
                response.errorResponse = errorResponse;
                return await Task.FromResult(response);
            }
        }
        public async Task<ErrorResponses> UpdateActivityValidation(ActivityTrackingResponseModel activityUpdateModel)
        {
            ErrorResponses errorResponses = new ErrorResponses();
            try
            {
                double parsedValue = 0;

                if (!await _user.UserAvailabilityCheck(activityUpdateModel.UserId))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyUserName, GlobalVariables.emptyUserNameDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for User Id", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await ActivityAvailabilityCheck(activityUpdateModel.Id))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.InvalidActivityId, GlobalVariables.InvalidActivityIdDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for  Id", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.ActivityTypeAvailabilityCheck(activityUpdateModel.ActivityTypeId))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyActivityType, GlobalVariables.emptyActivityTypeDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Activity Type Id", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (activityUpdateModel.Date == DateTime.MinValue)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyActivityDate, GlobalVariables.emptyActivityDateDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Date", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (activityUpdateModel.BodyWeight == 0 || !double.TryParse(activityUpdateModel.BodyWeight.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyActivityWeight, GlobalVariables.emptyActivityWeightDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Weight", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (activityUpdateModel.BodyFat == 0 || !double.TryParse(activityUpdateModel.BodyFat.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyActivityBodyFat, GlobalVariables.emptyActivityBodyFatDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Body Fat", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (activityUpdateModel.MuscleMass == 0 || !double.TryParse(activityUpdateModel.MuscleMass.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyActivitMuscleMass, GlobalVariables.emptyActivitMuscleMassDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Muscel Mass", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.NullValidation(activityUpdateModel.ActivityName))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyActivityName, GlobalVariables.emptyActivityNameDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Activity Name", errorResponses);
                    return await Task.FromResult(errorResponses);
                }


                errorResponses.Success = true;
                _logger.Log(LogLevel.Error, "UpdateUserDataValidation :Update Activity type validation completed", errorResponses);
                return await Task.FromResult(errorResponses);
            }
            catch (Exception ex)
            {
                errorResponses.Success = false;
                ex.ToString();
            }
            return await Task.FromResult(errorResponses);
        }
        public async Task<ErrorResponses> InsertDailyActivityValidation(ActivitysTrackingInsertModel activityInsertModel )
        {
            ErrorResponses errorResponses = new ErrorResponses();
            try
            {
                double parsedValue = 0;

                if (!await _user.UserAvailabilityCheck(activityInsertModel.UserId))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyUserName, GlobalVariables.emptyUserNameDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for User Id", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (activityInsertModel.Date == DateTime.MinValue)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyActivityDate, GlobalVariables.emptyActivityDateDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Date", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (activityInsertModel.BodyWeight == 0 || !double.TryParse(activityInsertModel.BodyWeight.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyActivityWeight, GlobalVariables.emptyActivityWeightDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Weight", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (activityInsertModel.BodyFat == 0 || !double.TryParse(activityInsertModel.BodyFat.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyActivityBodyFat, GlobalVariables.emptyActivityBodyFatDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Body Fat", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (activityInsertModel.MuscleMass == 0 || !double.TryParse(activityInsertModel.MuscleMass.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyActivitMuscleMass, GlobalVariables.emptyActivitMuscleMassDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Muscel Mass", errorResponses);
                    return await Task.FromResult(errorResponses);
                }

                errorResponses.Success = true;
                return await Task.FromResult(errorResponses);
            }
            catch (Exception ex)
            {
                errorResponses.Success = false;
                ex.ToString();
            }
            return await Task.FromResult(errorResponses);
        }
        public async Task<ErrorResponses> InsertActivityTrackingValidation(ActivitysTrackingInsertModel activityModel)
        {
            ErrorResponses errorResponses = new ErrorResponses();
            try
            {
                double parsedValue = 0;

                if (activityModel.activityTotalData.Count() > 0)
                {
                    for (int i = 0; i < activityModel.activityTotalData.Count(); i++)
                    {
                        if (!await _common.ActivityTypeAvailabilityCheck(activityModel.activityTotalData[i].ActivityTypeId))
                        {
                            errorResponses.Success = false;
                            errorResponses.Error = new Error(GlobalVariables.emptyActivityType, GlobalVariables.emptyActivityTypeDescription);
                            _logger.Log(LogLevel.Error, "Received Empty value for Activity Type Id", errorResponses);
                            return await Task.FromResult(errorResponses);
                        }
                        else if (!await _common.NullValidation(activityModel.activityTotalData[i].ActivityName))
                        {
                            errorResponses.Success = false;
                            errorResponses.Error = new Error(GlobalVariables.emptyActivityName, GlobalVariables.emptyActivityNameDescription);
                            _logger.Log(LogLevel.Error, "Received Empty value for Activity Name", errorResponses);
                            return await Task.FromResult(errorResponses);
                        }
                        else if (!double.TryParse(activityModel.activityTotalData[i].Duration.ToString(), out parsedValue) && parsedValue <= 0)
                        {
                            errorResponses.Success = false;
                            errorResponses.Error = new Error(GlobalVariables.emptyActivityName, GlobalVariables.emptyActivityNameDescription);
                            _logger.Log(LogLevel.Error, "Received Empty value for Activity Name", errorResponses);
                            return await Task.FromResult(errorResponses);
                        }
                        else if (!double.TryParse(activityModel.activityTotalData[i].Reps.ToString(), out parsedValue) && parsedValue <= 0)
                        {
                            errorResponses.Success = false;
                            errorResponses.Error = new Error(GlobalVariables.emptyReps, GlobalVariables.emptyRepsDescription);
                            _logger.Log(LogLevel.Error, "Received Empty value for Reps", errorResponses);
                            return await Task.FromResult(errorResponses);
                        }
                        else if (!double.TryParse(activityModel.activityTotalData[i].SetsInfo.ToString(), out parsedValue) && parsedValue <= 0)
                        {
                            errorResponses.Success = false;
                            errorResponses.Error = new Error(GlobalVariables.emptySets, GlobalVariables.emptySetsDescription);
                            _logger.Log(LogLevel.Error, "Received Empty value for Sets", errorResponses);
                            return await Task.FromResult(errorResponses);
                        }
                        else if (!double.TryParse(activityModel.activityTotalData[i].LiftWeight.ToString(), out parsedValue) && parsedValue <= 0)
                        {
                            errorResponses.Success = false;
                            errorResponses.Error = new Error(GlobalVariables.emptyLiftWeight, GlobalVariables.emptyLiftWeightDescription);
                            _logger.Log(LogLevel.Error, "Received Empty value for LiftWeight", errorResponses);
                            return await Task.FromResult(errorResponses);
                        }
                        else if (!double.TryParse(activityModel.activityTotalData[i].CaloriesBurned.ToString(), out parsedValue) && parsedValue <= 0)
                        {
                            errorResponses.Success = false;
                            errorResponses.Error = new Error(GlobalVariables.emptyCaloriesBurned, GlobalVariables.emptyCaloriesBurnedDescription);
                            _logger.Log(LogLevel.Error, "Received Empty value for CaloriesBurned", errorResponses);
                            return await Task.FromResult(errorResponses);
                        }
                        else if (activityModel.activityTotalData[i].IntensityLevelId != 0 && (!await _common.IntensityAvailabilityCheck(activityModel.activityTotalData[i].IntensityLevelId)))
                        {
                            errorResponses.Success = false;
                            errorResponses.Error = new Error(GlobalVariables.emptyActivityIntensity, GlobalVariables.emptyActivityIntensityDescription);
                            _logger.Log(LogLevel.Error, "Received Empty value for Intensity level", errorResponses);
                            return await Task.FromResult(errorResponses);
                        }
                        else if (activityModel.activityTotalData[i].Notes.Trim().Length > 0 && !await _common.NullValidation(activityModel.activityTotalData[i].Notes))
                        {
                            errorResponses.Success = false;
                            errorResponses.Error = new Error(GlobalVariables.emptyNotes, GlobalVariables.emptyNotesDescription);
                            _logger.Log(LogLevel.Error, "Received Empty value for Notes", errorResponses);
                            return await Task.FromResult(errorResponses);
                        }
                    }
                }
                else
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.ActivityValidateError, GlobalVariables.ActivityValidateErrorDescription);
                    return await Task.FromResult(errorResponses);
                }
                errorResponses.Success = true;
                _logger.LogInformation("InsertActivityTrackingValidation :Activity validation completed");
                return await Task.FromResult(errorResponses);
            }
            catch (Exception ex)
            {
                errorResponses.Success = false;
                errorResponses.Error = new Error(GlobalVariables.ActivityValidateError, GlobalVariables.ActivityValidateErrorDescription);
                _logger.Log(LogLevel.Error, ex, "Error Occured while validating the received designation data", errorResponses);
                ex.ToString();
                return await Task.FromResult(errorResponses);
            }
        }
        public async Task<bool> ActivityAvailabilityCheck(int iActivityId)
        {
            var ActivityId = (from A in _appDbContext.Set<DailyActivityTracking>()
                              where A.id == iActivityId
                              select A.id).FirstOrDefault();

            return await Task.FromResult(ActivityId > 0 ? true : false);
        }
        public async Task<int> DailyActivityId(int iUserId, DateTime dtDate)
        {
            var ActivityId = (from A in _appDbContext.Set<DailyActivityTracking>()
                              where A.userid == iUserId && A.infodate == dtDate
                              select A.id).FirstOrDefault();

            return await Task.FromResult(ActivityId);
        }


    }
}
