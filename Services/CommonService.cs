using Persistance;
using Services.Abstraction;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Domain.Models.Common;
using Domain.Entities;
using Domain.Models.User;
using Domain.Entities.Activity;
using Domain.Models.Activity;

namespace Services
{

    public class CommonService : ICommonServices
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CommonService> _logger;
        public CommonService(AppDbContext appDbContext, ILogger<CommonService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<bool> NullValidation(string strValue)
        {
            try
            {
                if (strValue.Trim() == null || string.IsNullOrEmpty(strValue) || string.IsNullOrWhiteSpace(strValue))
                {
                    return await Task.FromResult(false);
                }
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while validating, Method Name: NullValidation", ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> IsValidPhoneNumber(long? phoneNumber)
        {
            string PNumber = phoneNumber.ToString();
            try
            {
                if (phoneNumber != 0)
                {
                    string pattern = @"^([0]|\+91)?[789]\d{9}$|^([0-9]{2})?[2-9]\d{7}$";
                    Regex regex = new Regex(pattern);
                    return regex.IsMatch(PNumber);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while validating, Method Name: IsValidPhoneNumber", ex.Message);
                return await Task.FromResult(false);
            } 
            return await Task.FromResult(true); 
        }
      
        public async Task<bool> IsValidEmail(string email)
        {
            try
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(pattern);
                return regex.IsMatch(email);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured while validating, Method Name: IsValidEmail", ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<List<LocationCountryModel>> GetLocationCountry(int iLocationid)
        {
            List<LocationCountryModel> lstLocationCountryModels = new List<LocationCountryModel>();
            ErrorResponses errorResponse = new ErrorResponses();
            try
            {
                var dbResponse = _appDbContext.locationcountry.Where(x => x.id == (iLocationid == 0 ? x.id : iLocationid) && x.status == "Active").ToList();
                if (dbResponse != null)
                {
                    foreach (LocationCountry countryDetails in dbResponse)
                    {
                        LocationCountryModel locationCountryModel = new LocationCountryModel();
                        locationCountryModel.Id = countryDetails.id;
                        locationCountryModel.CountryCode = countryDetails.countrycode;
                        locationCountryModel.CountryName = countryDetails.countryname;
                        locationCountryModel.Status = countryDetails.status;
                        lstLocationCountryModels.Add(locationCountryModel);

                    }
                    return await Task.FromResult(lstLocationCountryModels);
                }
                else
                {
                    return await Task.FromResult(lstLocationCountryModels);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetLocationCountry", ex.Message);
                return await Task.FromResult(lstLocationCountryModels);
            }
        }

        public async Task<List<LocationStateModel>> GetLocationState(int iLocationid)
        {
            List<LocationStateModel> lstLocationStateModel = new List<LocationStateModel>();
            try
            {
                lstLocationStateModel = (from S in _appDbContext.Set<LocationState>()
                                         join C in _appDbContext.Set<LocationCountry>() on S.locationcountryid equals C.id
                                         where S.id == (iLocationid == 0 ? S.id : iLocationid) && S.status == "Active"

                                         select new LocationStateModel()
                                         {
                                             Id = S.id,
                                             StateCode = S.statecode,
                                             StateName = S.statename,
                                             LocationCountryId = C != null ? C.id : 0,
                                             LocationCountryName = C != null ? C.countryname : "",
                                             Status = S.status

                                         }).OrderByDescending(a => a.Id).ToList();

                return await Task.FromResult(lstLocationStateModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetLocationState", ex.Message);
                return await Task.FromResult(lstLocationStateModel);
            }
        }

        public async Task<List<GenderTypeModel>> GetGenderType(int iGenderid)
        {
            List<GenderTypeModel> lstGenderTypeModel = new List<GenderTypeModel>();
            ErrorResponses errorResponse = new ErrorResponses();
            try
            {
                var dbResponse = _appDbContext.gendertype.Where(x => x.id == (iGenderid == 0 ? x.id : iGenderid) && x.status == "Active").ToList();
                if (dbResponse != null)
                {
                    foreach (GenderType genderDetails in dbResponse)
                    {
                        GenderTypeModel genderTypeModel = new GenderTypeModel();
                        genderTypeModel.Id = genderDetails.id;
                        genderTypeModel.GenderCode = genderDetails.gendercode;
                        genderTypeModel.GenderName = genderDetails.gendername;
                        genderTypeModel.Status = genderDetails.status;
                        lstGenderTypeModel.Add(genderTypeModel);

                    }
                    return await Task.FromResult(lstGenderTypeModel);
                }
                else
                {
                    return await Task.FromResult(lstGenderTypeModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetGenderType", ex.Message);
                return await Task.FromResult(lstGenderTypeModel);
            }
        }

        public async Task<List<MaritalStatusModel>> GetMaritalStatus(int iMaritalid)
        {
            List<MaritalStatusModel> lstMaritalStatusModel = new List<MaritalStatusModel>();
            ErrorResponses errorResponse = new ErrorResponses();
            try
            {
                var dbResponse = _appDbContext.maritalstatus.Where(x => x.id == (iMaritalid == 0 ? x.id : iMaritalid) && x.status == "Active").ToList();
                if (dbResponse != null)
                {
                    foreach (MaritalStatus maritalDetails in dbResponse)
                    {
                        MaritalStatusModel maritalStatusModel = new MaritalStatusModel();
                        maritalStatusModel.Id = maritalDetails.id;
                        maritalStatusModel.Maritalcode = maritalDetails.maritalcode;
                        maritalStatusModel.MaritalName = maritalDetails.maritalname;
                        maritalStatusModel.Status = maritalDetails.status;
                        lstMaritalStatusModel.Add(maritalStatusModel);

                    }
                    return await Task.FromResult(lstMaritalStatusModel);
                }
                else
                {
                    return await Task.FromResult(lstMaritalStatusModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetMaritalStatus", ex.Message);
                return await Task.FromResult(lstMaritalStatusModel);
            }
        }

        public async Task<List<UserTypeModel>> GetUserType(int iUserTypeId)
        {
            List<UserTypeModel> lstUserTypeModel = new List<UserTypeModel>();
            ErrorResponses errorResponse = new ErrorResponses();
            try
            { 
                lstUserTypeModel = (from S in _appDbContext.Set<UserType>()
                                         join C in _appDbContext.Set<Users>() on S.id equals C.usertypeid
                                         where S.id == (iUserTypeId == 0 ? S.id : iUserTypeId) && S.status == "Active"

                                         select new UserTypeModel()
                                         {
                                             Id = S.id,
                                             TypeCode = S.typecode,
                                             TypeName = S.typename,
                                             UserName = C.firstname, 
                                             UserId = C.id,
                                             Status = S.status 

                                         }).OrderByDescending(a => a.Id).ToList();

                return await Task.FromResult(lstUserTypeModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetUserType", ex.Message);
                return await Task.FromResult(lstUserTypeModel);
            }
        }

        public async Task<List<ActivityTypeModel>> GetActivityType(int iActivityTypeId)
        {
            List<ActivityTypeModel> lstActivityTypeModel = new List<ActivityTypeModel>();
            ErrorResponses errorResponse = new ErrorResponses();
            try
            {
                var dbResponse = _appDbContext.activitytype.Where(x => x.id == (iActivityTypeId == 0 ? x.id : iActivityTypeId) && x.status == "Active").ToList();
                if (dbResponse != null)
                {
                    foreach (ActivityType activityTypeDetails in dbResponse)
                    {
                        ActivityTypeModel activityTypeModel = new ActivityTypeModel();
                        activityTypeModel.Id = activityTypeDetails.id;
                        activityTypeModel.ActivityCode = activityTypeDetails.activitycode;
                        activityTypeModel.ActivityName = activityTypeDetails.activityname;
                        activityTypeModel.Status = activityTypeDetails.status;
                        lstActivityTypeModel.Add(activityTypeModel);

                    }
                    return await Task.FromResult(lstActivityTypeModel);
                }
                else
                {
                    return await Task.FromResult(lstActivityTypeModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetActivityType", ex.Message);
                return await Task.FromResult(lstActivityTypeModel);
            }
        }

        public async Task<List<IntensityLevelModel>> GetIntensityLevel(int iIntensitylevelId)
        {
            List<IntensityLevelModel> lstIntensityLevelModel = new List<IntensityLevelModel>();
            ErrorResponses errorResponse = new ErrorResponses();
            try
            {
                var dbResponse = _appDbContext.intensitylevel.Where(x => x.id == (iIntensitylevelId == 0 ? x.id : iIntensitylevelId) && x.status == "Active").ToList();
                if (dbResponse != null)
                {
                    foreach (IntensityLevel intensityLevelDetails in dbResponse)
                    {
                        IntensityLevelModel intensityLevelModel = new IntensityLevelModel();
                        intensityLevelModel.Id = intensityLevelDetails.id;
                        intensityLevelModel.IntensityName = intensityLevelDetails.intensityname;
                        intensityLevelModel.IntensityCode = intensityLevelDetails.intensitycode;
                        intensityLevelModel.Status = intensityLevelDetails.status;
                        lstIntensityLevelModel.Add(intensityLevelModel);

                    }
                    return await Task.FromResult(lstIntensityLevelModel);
                }
                else
                {
                    return await Task.FromResult(lstIntensityLevelModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetIntensityLevel", ex.Message);
                return await Task.FromResult(lstIntensityLevelModel);
            }
        }

        public async Task<bool> UserTypeAvailabilityCheck(int iUserTypeId)
        {
            var UserTypeId = (from UT in _appDbContext.Set<UserType>()
                              where UT.id == iUserTypeId
                              select UT.id).FirstOrDefault();

            return await Task.FromResult(UserTypeId > 0 ? true : false);
        } 
        public async Task<bool> GenderAvailabilityCheck(int iGenderid)
        {
            var Genderid = (from G in _appDbContext.Set<GenderType>()
                              where G.id == iGenderid
                              select G.id).FirstOrDefault();

            return await Task.FromResult(Genderid > 0 ? true : false);
        } 
        public async Task<bool> StateAvailabilityCheck(int iStateid)
        {
            var Stateid = (from G in _appDbContext.Set<LocationState>()
                            where G.id == iStateid
                            select G.id).FirstOrDefault();

            return await Task.FromResult(Stateid > 0 ? true : false);
        } 
        public async Task<bool> CountryAvailabilityCheck(int iCountryid)
        {
            var Countryid = (from C in _appDbContext.Set<LocationState>()
                           where C.id == iCountryid
                           select C.id).FirstOrDefault();

            return await Task.FromResult(Countryid > 0 ? true : false);
        } 
        public async Task<bool> MaritalStatusAvailabilityCheck(int iMaritalid)
        {
            var Maritalid = (from M in _appDbContext.Set<LocationState>()
                             where M.id == iMaritalid
                             select M.id).FirstOrDefault();

            return await Task.FromResult(Maritalid > 0 ? true : false);
        } 
        public async Task<bool> SASSAvailabilityCheck(int iSASSId)
        {
            var SASSId = (from S in _appDbContext.Set<SASSDetails>()
                          where S.id == iSASSId
                          select S.id).FirstOrDefault();

            return await Task.FromResult(SASSId > 0 ? true : false);
        }
        public async Task<bool> ActivityTypeAvailabilityCheck(int iTypeId)
        {
            var TypeId = (from T in _appDbContext.Set<ActivityType>()
                          where T.id == iTypeId
                          select T.id).FirstOrDefault();

            return await Task.FromResult(TypeId > 0 ? true : false);
        }
        public async Task<bool> IntensityAvailabilityCheck(int? iIntensityId)
        {
            int IntensityId = 0;

            if (iIntensityId > 0)
            {
                IntensityId = (from I in _appDbContext.Set<IntensityLevel>()
                               where I.id == iIntensityId
                               select I.id).FirstOrDefault();
            }
            return await Task.FromResult(IntensityId > 0 ? true : false);
        }

    }
}
