using Domain.Entities;
using Domain.Models.Common;
using Domain.Models.Goal;
using Domain.Models.User;
using Persistance;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Amazon.SecretsManager.Model.Internal.MarshallTransformations;
using Amazon.Runtime.Internal;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Abstraction;
using Domain.Entities.Activity;
using Domain.Entities.Lookup;
using Domain.Models.Activity;

namespace Services
{
    public class GoalSettingServices : IGoalSettingServices
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GoalSettingServices> _logger;
        private readonly ICommonServices _common;
        private readonly IUser _user;

        public GoalSettingServices(AppDbContext appDbContext, ILogger<GoalSettingServices> logger, ICommonServices common, IUser user)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _common = common;
            _user = user;
        }
        public async Task<List<GoalSettingModel>> GetGoalSetting(int iUserId)
        {
            List<GoalSettingModel> lstGoalSettingModels = new List<GoalSettingModel>();
            GoalSettingModel goalSettingModel = new GoalSettingModel();
            ErrorResponses errorResponse = new ErrorResponses();
            try
            {
                lstGoalSettingModels = (from S in _appDbContext.Set<GoalSetting>()
                                        join SW in _appDbContext.Set<GoalType>() on S.goaltypeid equals SW.id
                                        where S.userid==iUserId && S.status == "Active"

                                        select new GoalSettingModel()
                                       {
                                           Id = S.id,
                                           UserId = S.userid,
                                           GoalTypeId = SW.id,
                                           GoalType = SW.name,
                                           TargetWeight = S.targetweight,
                                           TargetBodyFat = S.targetbodyfat,
                                           TargetMuscleMass = S.targetmusclemass,
                                           TargetDate=S.targetdate,
                                           TargetDuration = S.targetduration, 
                                           WorkoutFrequency = S.workoutfrequency,
                                           Status= S.status,

                                       }).OrderByDescending(a => a.Id).ToList();

                return await Task.FromResult(lstGoalSettingModels);
            }  
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetGoalSetting", ex.Message);
                return await Task.FromResult(lstGoalSettingModels);
            }
        }

        public async Task<DatabaseResponse> InsertGoalSetting(GoalSettingModelInsertModel goalSettingModel )
        {
            DatabaseResponse dbResponse = new DatabaseResponse();
            ErrorResponses errorResponse = new ErrorResponses();
            dbResponse.status = false;
            try
            {
                var validationResponse = _appDbContext.goalsetting.Where(x => x.userid == goalSettingModel.UserId && x.status == "Active").ToList();
                if (validationResponse.Count > 0)
                {
                    errorResponse.Success = false;
                    errorResponse.Error = new Error(GlobalVariables.GoalAlreadyExist, GlobalVariables.GoalAlreadyExistDescription);
                    _logger.LogError("Goal Already Exist", errorResponse);
                    dbResponse.status = false;
                    dbResponse.errorResponse = errorResponse;
                    return await Task.FromResult(dbResponse);
                }
                else
                {
                    var goalSetting = new GoalSetting
                    {
                        userid = goalSettingModel.UserId,
                        goaltypeid = goalSettingModel.GoalTypeId,
                        targetweight = goalSettingModel.TargetWeight,
                        targetbodyfat = goalSettingModel.TargetBodyFat,
                        targetmusclemass = goalSettingModel.TargetMuscleMass,
                        targetduration = goalSettingModel.TargetDuration,
                        workoutfrequency = goalSettingModel.WorkoutFrequency, 
                        targetdate = goalSettingModel.TargetDate,
                        createdby = goalSettingModel.CreatedBy,
                        createdat = DateTime.Now,
                        status = "Active"
                    };
                    _appDbContext.Set<GoalSetting>().Add(goalSetting);
                    _appDbContext.SaveChanges();
                    dbResponse.status = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: InsertGoalSetting", ex.Message);
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.ErrorOnInsertingGoal, GlobalVariables.ErrorOnInsertingGoalDescription);
                dbResponse.status = false;
                dbResponse.errorResponse = errorResponse;
                return await Task.FromResult(dbResponse);
            }
            return await Task.FromResult(dbResponse);

        }
        public async Task<DatabaseResponse> UpdateGoalSetting(GoalSettingUpdateModel goalSettingModel)
        {
            DatabaseResponse dbResponse = new DatabaseResponse();
            ErrorResponses errorResponse = new ErrorResponses();
            dbResponse.status = false;

            try
            {
                var validationResponse = _appDbContext.goalsetting.Where(x => x.id == goalSettingModel.Id && x.status == "Active").FirstOrDefault();
                if (validationResponse == null)
                {
                    errorResponse.Success = false;
                    errorResponse.Error = new Error(GlobalVariables.GoalDosntExist, GlobalVariables.GoalDosntExistDescription);
                    _logger.LogError("Goal dosn't exist for the given user", errorResponse);
                    dbResponse.status = false;
                    dbResponse.errorResponse = errorResponse;
                    return await Task.FromResult(dbResponse);
                }
                else
                {
                    validationResponse.id = goalSettingModel.Id;
                    validationResponse.userid = goalSettingModel.UserId;
                    validationResponse.goaltypeid = goalSettingModel.GoalTypeId;
                    validationResponse.targetweight = goalSettingModel.TargetWeight;
                    validationResponse.targetbodyfat = goalSettingModel.TargetBodyFat;
                    validationResponse.targetmusclemass = goalSettingModel.TargetMuscleMass;
                    validationResponse.targetduration = goalSettingModel.TargetDuration;
                    validationResponse.workoutfrequency = goalSettingModel.WorkoutFrequency;
                    validationResponse.targetdate = goalSettingModel.TargetDate;
                    validationResponse.modifiedby = goalSettingModel.ModifiedBy;
                    validationResponse.modifiedat = DateTime.Now;
                    validationResponse.status = "Active";
                    _appDbContext.goalsetting.Update(validationResponse);
                    _appDbContext.SaveChanges();
                    _appDbContext.Entry(validationResponse).State = EntityState.Detached;
                    dbResponse.status = true;
                }

                return await Task.FromResult(dbResponse);
            }
            catch (Exception ex) { }
            return await Task.FromResult(dbResponse);
        }

        public async Task<ErrorResponses> InsertGoalValidation(GoalSettingModelInsertModel goalInsertModel )
        {
            ErrorResponses errorResponses = new ErrorResponses();
            try
            {
                double parsedValue = 0;
                if (!await _user.UserAvailabilityCheck(goalInsertModel.UserId))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyUserName, GlobalVariables.emptyUserNameDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for User Id", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                if (goalInsertModel.TargetWeight == 0 || !double.TryParse(goalInsertModel.TargetWeight.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGoalSettingTargetWeight, GlobalVariables.emptyGoalSettingTargetWeightDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for TargetWeight", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if ( !double.TryParse(goalInsertModel.TargetBodyFat.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGoalSettingTargetBodyFat, GlobalVariables.emptyGoalSettingTargetBodyFatDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for TargetBodyFat", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if ( !double.TryParse(goalInsertModel.TargetMuscleMass.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGoalSettingTargetMuscleMass, GlobalVariables.emptyGoalSettingTargetMuscleMassDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for TargetMuscleMass", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (  !double.TryParse(goalInsertModel.TargetDuration.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGoalSettingTargetDuration, GlobalVariables.emptyGoalSettingTargetDurationDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for TargetDuration", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if ( !double.TryParse(goalInsertModel.WorkoutFrequency.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGoalSettingWorkoutFrequency, GlobalVariables.emptyGoalSettingWorkoutFrequencyDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for WorkoutFrequency", errorResponses);
                    return await Task.FromResult(errorResponses);
                } 

                errorResponses.Success = true;
                _logger.Log(LogLevel.Error, "StringNullValidation :GoalSetting validation completed", errorResponses);
                return await Task.FromResult(errorResponses);
            }
            catch (Exception ex)
            {
                errorResponses.Success = false;
                errorResponses.Error = new Error(GlobalVariables.ErrorOnInsertingGoalSetting, GlobalVariables.ErrorOnInsertingGoalSettingDescription);
                _logger.Log(LogLevel.Error, ex, "Error Occured while validating the received GoalSetting data", errorResponses);
                ex.ToString();
                return await Task.FromResult(errorResponses);
            }
        }

        public async Task<ErrorResponses> UpdateGoalValidation(GoalSettingUpdateModel goalUpdateModel)
        {
            ErrorResponses errorResponses = new ErrorResponses();
            try
            {
                double parsedValue = 0;
                if (!await _user.UserAvailabilityCheck(goalUpdateModel.UserId))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyUserName, GlobalVariables.emptyUserNameDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for User Id", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (goalUpdateModel.TargetWeight == 0 || !double.TryParse(goalUpdateModel.TargetWeight.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGoalSettingTargetWeight, GlobalVariables.emptyGoalSettingTargetWeightDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for TargetWeight", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (  !double.TryParse(goalUpdateModel.TargetBodyFat.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGoalSettingTargetBodyFat, GlobalVariables.emptyGoalSettingTargetBodyFatDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for TargetBodyFat", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if ( !double.TryParse(goalUpdateModel.TargetMuscleMass.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGoalSettingTargetMuscleMass, GlobalVariables.emptyGoalSettingTargetMuscleMassDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for TargetMuscleMass", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (  !double.TryParse(goalUpdateModel.TargetDuration.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGoalSettingTargetDuration, GlobalVariables.emptyGoalSettingTargetDurationDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for TargetDuration", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (  !double.TryParse(goalUpdateModel.WorkoutFrequency.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGoalSettingWorkoutFrequency, GlobalVariables.emptyGoalSettingWorkoutFrequencyDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for WorkoutFrequency", errorResponses);
                    return await Task.FromResult(errorResponses);
                } 

                errorResponses.Success = true;
                _logger.Log(LogLevel.Error, "StringNullValidation :goalsetting validation completed", errorResponses);
                return await Task.FromResult(errorResponses);
            }
            catch (Exception ex)
            {
                errorResponses.Success = false;
                ex.ToString();
            }
            return await Task.FromResult(errorResponses);
        } 
        public async Task<List<TargetGoalModel>> GetTragetGoalDetails(int UserId)
        {
            List<TargetGoalModel> lstGoalSettingModel = new List<TargetGoalModel>();
            try
            {
                lstGoalSettingModel = (from S in _appDbContext.Set<GoalSetting>()
                                       join C in _appDbContext.Set<GoalType>() on S.goaltypeid equals C.id
                                       where S.userid == UserId && S.status == "Active"

                                       select new TargetGoalModel()
                                       {
                                           TargetWeight = S.targetweight,
                                           TargetBodyFat = S.targetbodyfat,
                                           TargetMuscleMass = S.targetmusclemass,
                                           TargetDate = S.targetdate,
                                           Id = S.id,
                                           UserId = S.userid,
                                           GoalTypeId = C != null ? C.id : 0,
                                           GoalTypeName = C != null ? C.name : "",
                                           Status = "Active",
                                       }).OrderByDescending(a => a.Id).ToList();

                return await Task.FromResult(lstGoalSettingModel);

            }
            catch (Exception ex) { }

            return await Task.FromResult(lstGoalSettingModel);
        }

    }
}

