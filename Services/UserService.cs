using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Abstraction;
using Persistance;
using Domain.Models.Common;
using Domain.Models.User;
using Domain.Models;
using Domain;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Domain.Entities;
using Amazon.Runtime.Internal;

namespace Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UserService> _logger;
        private readonly ICommonServices _common;

        public UserService(AppDbContext appDbContext, ILogger<UserService> logger, ICommonServices common)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _common = common;
        }

        public async Task<DatabaseResponse> ValidateUser(UserFilterModel filterModel)
        {
            DatabaseResponse response = new DatabaseResponse();
            ErrorResponses errorResponse = new ErrorResponses();

            try
            {

                var dbResponse = _appDbContext.users.Where(x => x.username.Trim().ToLower() == filterModel.UserName.Trim().ToLower() && x.password == filterModel.Password && x.usertypeid == filterModel.UserType && x.status == "Active").FirstOrDefault();
                if (dbResponse == null)
                {
                    errorResponse.Success = false;
                    errorResponse.Error = new Error(GlobalVariables.receivedInvalidUser, GlobalVariables.receivedInvalidUserDescription);
                    _logger.LogError("User dosn't not exist", errorResponse);
                    response.status = false;
                    response.errorResponse = errorResponse;
                    return await Task.FromResult(response);
                }
                else
                {
                    response.status = true;
                    errorResponse.Success = true;
                    errorResponse.Error = new Error(GlobalVariables.ValidUserDetails, GlobalVariables.ValidUserDetailsDescription);
                    response.errorResponse = errorResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: ValidateUser", ex.Message);
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.errorUserValidation, GlobalVariables.errorUserValidationDescription);
                response.status = false;
                response.errorResponse = errorResponse;
                return await Task.FromResult(response);
            }
            return response;
        }
        public async Task<List<UserResponseModel>> GetUserProfile(string UserId)
        {
            List<UserResponseModel> lstUserResponse = new List<UserResponseModel>();
            try
            {
                lstUserResponse = (from U in _appDbContext.Set<Users>()
                                   join MI in _appDbContext.Set<UserMedicalInfo>() on U.id equals MI.userid
                                   join CO in _appDbContext.Set<LocationCountry>() on U.locationcountry equals CO.id
                                   join ST in _appDbContext.Set<LocationState>() on U.locationstate equals ST.id
                                   join G in _appDbContext.Set<GenderType>() on U.gender equals G.id
                                   join M in _appDbContext.Set<MaritalStatus>() on U.maritalstatus equals M.id

                                   where U.username.Trim() == UserId.Trim() && U.status == "Active"

                                   select new UserResponseModel()
                                   {
                                       Id = U.id,
                                       FirstNames = U.firstname,
                                       LastName = U.lastname,
                                       GenderId = G != null ? G.id : 0,
                                       GenderName = G != null ? G.gendername : "",
                                       LocationCountry = CO != null ? CO.id : 0,
                                       LocationCountryName = CO != null ? CO.countryname : "",
                                       LocationState = ST != null ? ST.id : 0,
                                       LocationStateName = ST != null ? ST.statename : "",
                                       MaritalStatusId = M != null ? M.id : 0,
                                       MaritalStatusName = M != null ? M.maritalname : "",
                                       AnniversaryDate = U.anniversarydate,
                                       DateOfBirth = U.dateofbirth,
                                       Address = U.address,
                                       Height = U.height,
                                       Weight = U.weight,
                                       MemberFrom = U.memberfrom,
                                       MemberTo = U.memberto,
                                       Email = U.email,
                                       ContactNumber = U.contactnumber,
                                       EmergencyContactPerson = U.emergencycontactperson,
                                       EmergencyContactNumber = U.emergencycontactnumber,
                                       MedicalCondition = MI.medicalcondition,
                                       MedicalConditionDetails = MI.medicaldetails,
                                       Medications = MI.medication,
                                       MedicationDetails = MI.medicationdetails,
                                       Status = U.status

                                   }).OrderByDescending(a => a.Id).ToList();

                return await Task.FromResult(lstUserResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetUserProfile", ex.Message);
                ex.ToString();
            }
            return await Task.FromResult(lstUserResponse);
        }
        public async Task<DatabaseResponse> InsertUser(UserInsertModel userModel)
        {
            DatabaseResponse response = new DatabaseResponse();
            ErrorResponses errorResponse = new ErrorResponses();

            try { }
            catch (Exception ex) { ex.ToString(); }
            return await Task.FromResult(response);
        }
        public async Task<DatabaseResponse> UpdateUserProfile(UserUpdateModel userModel)
        {
            DatabaseResponse response = new DatabaseResponse();
            ErrorResponses errorResponse = new ErrorResponses();
            var dbResponse = _appDbContext.users.Where(x => x.id == userModel.Id && x.status == "Active").AsNoTracking().FirstOrDefault();

            if (dbResponse == null)
            {
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.UserProfileUpdateNoData, GlobalVariables.UserProfileUpdateNoDataDescription);
                _logger.LogError("There is no record found for this primary key", errorResponse);
                response.status = false;
                response.errorResponse = errorResponse;
                return await Task.FromResult(response);
            }
            else
            {
                try
                {
                    dbResponse.id = userModel.Id;
                    dbResponse.sassid = userModel.SASSId;
                    dbResponse.usertypeid = userModel.UserType;
                    dbResponse.username = userModel.UserName;
                    dbResponse.firstname = userModel.FirstNames;
                    dbResponse.lastname = userModel.LastName;
                    dbResponse.dateofbirth = userModel.DateOfBirth;
                    dbResponse.gender = userModel.GenderId;
                    dbResponse.maritalstatus = userModel.MaritalStatusId;
                    dbResponse.anniversarydate = userModel.AnniversaryDate;
                    dbResponse.height = userModel.Height;
                    dbResponse.weight = userModel.Weight;
                    dbResponse.memberfrom = userModel.MemberFrom;
                    dbResponse.memberto = userModel.MemberTo;
                    dbResponse.email = userModel.Email;
                    dbResponse.contactnumber = userModel.ContactNumber;
                    dbResponse.emergencycontactnumber = userModel.EmergencyContactNumber;
                    dbResponse.emergencycontactperson = userModel.EmergencyContactPerson;
                    dbResponse.address = userModel.Address;
                    dbResponse.locationcountry = userModel.LocationCountry;
                    dbResponse.locationstate = userModel.LocationState;
                    dbResponse.modifiedby = "Admin";
                    dbResponse.modifiedat = DateTime.Now;
                    dbResponse.status = "Active";
                    _appDbContext.users.Update(dbResponse);
                    _appDbContext.SaveChanges();
                    _appDbContext.Entry(dbResponse).State = EntityState.Detached;
                    var MedicalRes = await UpdateMedicalInformation(userModel, userModel.Id);
                    response.status = MedicalRes.status;
                    response.errorResponse = MedicalRes.errorResponse;
                    return await Task.FromResult(response);

                }
                catch (Exception ex)
                {

                    errorResponse.Success = false;
                    errorResponse.Error = new Error(GlobalVariables.UserProfilesavechangesError, GlobalVariables.UserProfilesavechangesErrorDescription);
                    _logger.LogError("There is no record found for this primary key", errorResponse);
                    response.status = false;
                    response.errorResponse = errorResponse;
                    return await Task.FromResult(response);
                }
            }
        }
        public async Task<DatabaseResponse> UpdateMedicalInformation(UserUpdateModel userModel, int iUserID)
        {
            ErrorResponses errorResponse = new ErrorResponses();
            DatabaseResponse response = new DatabaseResponse();
            var dbResponse = _appDbContext.usermedicalinfo.Where(x => x.userid == iUserID && x.status == "Active").AsNoTracking().FirstOrDefault();
            try
            {
                if (dbResponse != null)
                {
                    dbResponse.id = userModel.Id;
                    dbResponse.userid = iUserID;
                    dbResponse.medicalcondition = userModel.MedicalCondition;
                    dbResponse.medicaldetails = userModel.MedicalConditionDetails;
                    dbResponse.medication = userModel.Medications;
                    dbResponse.medicationdetails = userModel.MedicationDetails;
                    dbResponse.modifiedby = "Admin";
                    dbResponse.modifiedat = DateTime.Now;
                    dbResponse.status = "Active";
                    _appDbContext.usermedicalinfo.Update(dbResponse);
                    _appDbContext.SaveChanges();
                    response.status = true;
                    return await Task.FromResult(response);
                }
            }
            catch (Exception ex)
            {
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.MedicalInfoUpdateNoData, GlobalVariables.MedicalInfoUpdateNoDataDescription);
                _logger.LogError("There is no record found for this primary key", errorResponse);
                response.status = false;
                response.errorResponse = errorResponse;
                return await Task.FromResult(response);
            }
            return await Task.FromResult(response);
        }
        public async Task<DatabaseResponse> DeleteUser(int id)
        {
            DatabaseResponse response = new DatabaseResponse();
            ErrorResponses errorResponse = new ErrorResponses();

            try { }
            catch (Exception ex) { }
            return await Task.FromResult(response);
        }

        public async Task<ErrorResponses> UserDataValidation(UserFilterModel filterModel)
        {
            ErrorResponses errorResponses = new ErrorResponses();
            try
            {
                if (!await _common.NullValidation(filterModel.SASSId.ToString()))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptySASSId, GlobalVariables.emptySASSIdDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for SASS Id", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.NullValidation(filterModel.UserType.ToString()))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyUserType, GlobalVariables.emptyUserTypeDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for User Type Id", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.NullValidation(filterModel.UserName))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyUserName, GlobalVariables.emptyUserNameDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for User Name", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.NullValidation(filterModel.Password))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyUserPassword, GlobalVariables.emptyUserPasswordDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Password", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                errorResponses.Success = true;
                _logger.Log(LogLevel.Error, "UserDataValidation :User validation completed", errorResponses);
                return await Task.FromResult(errorResponses);
            }
            catch (Exception ex)
            {
                errorResponses.Success = false;
                ex.ToString();
            }
            return await Task.FromResult(errorResponses);
        }

        public async Task<ErrorResponses> UpdateDataValidation(UserUpdateModel UserUpdateModel)
        {
            ErrorResponses errorResponses = new ErrorResponses();
            try
            {
                double parsedValue = 0;

                if (!await _common.SASSAvailabilityCheck(UserUpdateModel.SASSId))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptySASSId, GlobalVariables.emptySASSIdDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for SASS Id", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.UserTypeAvailabilityCheck(UserUpdateModel.UserType))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyUserType, GlobalVariables.emptyUserTypeDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for User Type Id", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.NullValidation(UserUpdateModel.UserName))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyUserName, GlobalVariables.emptyUserNameDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for User Name", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.NullValidation(UserUpdateModel.FirstNames))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyFirstname, GlobalVariables.emptyFirstnameDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for First Name", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.NullValidation(UserUpdateModel.LastName))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyLastname, GlobalVariables.emptyLastnameDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Last Name", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (UserUpdateModel.DateOfBirth == DateTime.MinValue)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyDateOfBirth, GlobalVariables.emptyDateOfBirthDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Date Of Birth", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (UserUpdateModel.MemberFrom == DateTime.MinValue)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyMemberFrom, GlobalVariables.emptyMemberFromDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Member From", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (UserUpdateModel.MemberTo == DateTime.MinValue)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyMemberTo, GlobalVariables.emptyMemberToDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Member To", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.GenderAvailabilityCheck(UserUpdateModel.GenderId))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyGender, GlobalVariables.emptyGenderDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Gender", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.StateAvailabilityCheck(UserUpdateModel.LocationState))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyState, GlobalVariables.emptyStateDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for State", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.CountryAvailabilityCheck(UserUpdateModel.LocationCountry))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyCountry, GlobalVariables.emptyCountryDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Country", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.MaritalStatusAvailabilityCheck(UserUpdateModel.MaritalStatusId))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyMaritalStatus, GlobalVariables.emptyMaritalStatusDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for MaritalStatus", errorResponses);
                    return await Task.FromResult(errorResponses);
                } 
                else if (UserUpdateModel.Height == 0 || !double.TryParse(UserUpdateModel.Height.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyHeight, GlobalVariables.emptyHeightDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Height", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (UserUpdateModel.Weight == 0 || !double.TryParse(UserUpdateModel.Weight.ToString(), out parsedValue) && parsedValue <= 0)
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyWeight, GlobalVariables.emptyWeightDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Weight", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.IsValidEmail(UserUpdateModel.Email))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyEmail, GlobalVariables.emptyEmailDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Email", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (!await _common.IsValidPhoneNumber(UserUpdateModel.ContactNumber))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyContactNumber, GlobalVariables.emptyContactNumberDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Contact Number", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (UserUpdateModel.EmergencyContactNumber.ToString().Trim().Length > 0 && !await _common.IsValidPhoneNumber(UserUpdateModel.EmergencyContactNumber))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyEmergencyContactNumber, GlobalVariables.emptyEmergencyContactNumberDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Emergency Contact Number", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (UserUpdateModel.EmergencyContactPerson.Trim().Length > 0 && !await _common.NullValidation(UserUpdateModel.EmergencyContactPerson))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyEmergencyContactPerson, GlobalVariables.emptyEmergencyContactPersonDescription);
                    _logger.Log(LogLevel.Error, "Received Empty value for Emergency Contact Person", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (UserUpdateModel.MedicalConditionDetails != null && UserUpdateModel.MedicalConditionDetails.Trim().Length > 0 && !await _common.NullValidation(UserUpdateModel.MedicalConditionDetails))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyFalseMedicalCondition, GlobalVariables.emptyFalseMedicalConditionDescription);
                    _logger.Log(LogLevel.Error, "Received Invalid value for Medical Condition", errorResponses);
                    return await Task.FromResult(errorResponses);
                }
                else if (UserUpdateModel.MedicationDetails != null && UserUpdateModel.MedicationDetails.Trim().Length > 0 && !await _common.NullValidation(UserUpdateModel.MedicationDetails))
                {
                    errorResponses.Success = false;
                    errorResponses.Error = new Error(GlobalVariables.emptyFalseMedication, GlobalVariables.emptyFalseMedicationDescription);
                    _logger.Log(LogLevel.Error, "Received Invalid value for Medication", errorResponses);
                    return await Task.FromResult(errorResponses);
                }



                errorResponses.Success = true;
                _logger.Log(LogLevel.Error, "UpdateUserDataValidation :Update User validation completed", errorResponses);
                return await Task.FromResult(errorResponses);
            }
            catch (Exception ex)
            {
                errorResponses.Success = false;
                ex.ToString();
            }
            return await Task.FromResult(errorResponses);
        }

        public async Task<bool> UserAvailabilityCheck(int iUserId)
        {
            var UserId = (from S in _appDbContext.Set<Users>()
                          where S.id == iUserId
                          select S.id).FirstOrDefault();

            return await Task.FromResult(UserId > 0 ? true : false);
        }
        public async Task<int> GetUserIdByUserName(string strUserName)
        {
            var UserId = (from S in _appDbContext.Set<Users>()
                          where S.username == strUserName
                          select S.id).FirstOrDefault();

            return await Task.FromResult(UserId);
        }
    }
}
