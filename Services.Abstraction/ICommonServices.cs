using Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Common;
using System.Text.RegularExpressions;
using Domain.Models.Activity;

namespace Services.Abstraction
{
    public interface ICommonServices
    {
        Task<bool> NullValidation(string strValue);
        Task<bool> IsValidPhoneNumber(long? phoneNumber);
        Task<bool> IsValidEmail(string email);
        Task<List<LocationCountryModel>> GetLocationCountry(int iLocationid);
        Task<List<LocationStateModel>> GetLocationState(int iLocationid);
        Task<List<GenderTypeModel>> GetGenderType(int iGenderid);
        Task<List<MaritalStatusModel>> GetMaritalStatus(int iMaritalid);
        Task<List<UserTypeModel>> GetUserType(int iUserTypeId);
        Task<bool> UserTypeAvailabilityCheck(int iUserTypeId);
        Task<bool> GenderAvailabilityCheck(int iGenderid);
        Task<bool> StateAvailabilityCheck(int iStateid);
        Task<bool> CountryAvailabilityCheck(int iCountryid);
        Task<bool> MaritalStatusAvailabilityCheck(int iMaritalid);
        Task<bool> SASSAvailabilityCheck(int iSASSId);
        Task<bool> ActivityTypeAvailabilityCheck(int iTypeId);
        Task<bool> IntensityAvailabilityCheck(int? iIntensityId);
        Task<List<ActivityTypeModel>> GetActivityType(int iActivityTypeId);
        Task<List<IntensityLevelModel>> GetIntensityLevel(int iIntensitylevelId);
    }
}
