using Domain.Models.User;
using Domain.Models.Common;

namespace Services.Abstraction
{
    public interface IUser
    {
        Task<DatabaseResponse> ValidateUser(UserFilterModel filterModel);
        Task<List<UserResponseModel>> GetUserProfile(string UserId);
        Task<DatabaseResponse> InsertUser(UserInsertModel userModel);
        Task<DatabaseResponse> UpdateUserProfile(UserUpdateModel userModel);
        Task<DatabaseResponse> DeleteUser(int id);
        Task<ErrorResponses> UserDataValidation(UserFilterModel filterModel);
        Task<ErrorResponses> UpdateDataValidation(UserUpdateModel UserUpdateModel);
        Task<DatabaseResponse> UpdateMedicalInformation(UserUpdateModel userModel, int iUserID);
        Task<bool> UserAvailabilityCheck(int iUserId);
        Task<int> GetUserIdByUserName(string strUserName);
    }
}
