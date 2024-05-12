using Domain.Models.Common;
using Domain.Models.User;
using Domain;
using Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Domain.Entities;
using Services;
using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ApplicationService.Controllers
{
    [Route("api/[user]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUser _userService;
        public readonly ICommonServices _common;
        public readonly ILogger<UserController> _logger;

        public UserController(IUser userService,ILogger<UserController> logger, ICommonServices common) 
        {
            this._userService = userService;
            this._logger = logger;
            this._common = common;
        }

        [HttpPost]
        [Route("/validateuser")]
        public async Task<IActionResult> ValidateUser([FromBody] UserFilterModel filterModel)
        {
            ErrorResponses errorResponse = new ErrorResponses();
            DatabaseResponse response = new DatabaseResponse();
            try
            {
                if (filterModel == null)
                {
                    errorResponse.Success = false;
                    errorResponse.Error = new Error(GlobalVariables.emptyUserJSON, GlobalVariables.emptyUserJSONDescription);
                    return Ok(errorResponse);
                }
                 

                var validateResponse = await _userService.UserDataValidation(filterModel);

                if (!validateResponse.Success)
                {
                    _logger.LogError("Received Empty/invalid request JSON", validateResponse);
                    return Ok(validateResponse);
                }

                response = await _userService.ValidateUser(filterModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.errorUserValidation, GlobalVariables.errorUserValidationDescription);
                response.status = false;
                response.errorResponse = errorResponse;
                _logger.LogError("Error on Method: ValidateUser", ex.Message.ToString());
                return BadRequest(errorResponse);
            }
        }
        [HttpGet]
        [Route("/getuserprofile/{strUserID}")]
        public async Task<string?> GetUserProfile(string strUserID)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                if (!await _common.NullValidation(strUserID))
                {
                    _logger.Log(LogLevel.Error, "Received Empty value for User Id", "UserId:" + strUserID);
                    return await Task.FromResult(sResponse);
                }
                var dResponse = await _userService.GetUserProfile(strUserID);
                if (dResponse != null)
                {
                    response.data = dResponse.Cast<dynamic>().ToList();
                    response.totalData = dResponse.Count;
                    sResponse = JsonSerializer.Serialize(response).ToString();
                    return await Task.FromResult(sResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetUserProfile", ex.Message.ToString());
            }

            return await Task.FromResult(sResponse);
        }
        [HttpPost]
        [Route("/updateuserprofile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserUpdateModel _data)
        {
            _logger.LogInformation("Request received for update UserProfile");

            ErrorResponses errorResponse = new ErrorResponses();
            DatabaseResponse res = new DatabaseResponse();
            try
            {

                if (_data.Id > 0)
                {
                    var validateResponse = await _userService.UpdateDataValidation(_data);
                    if (!validateResponse.Success)
                    {
                        errorResponse.Success = false;
                        errorResponse.Error = validateResponse.Error;
                        _logger.LogError("Received Empty/invalid request JSON", errorResponse);
                        return Ok(errorResponse);
                    }
                    var response = await _userService.UpdateUserProfile(_data);
                    if (response.status)
                    {
                        _logger.LogError("Record got updated for UserProfile");
                        errorResponse.Success = true;
                        errorResponse.Error = new Error(GlobalVariables.UserProfileUpdateSuccess, GlobalVariables.UserProfileUpdateSuccessDescription);
                        return Ok(errorResponse);
                    }
                    else
                    {
                        return Ok(response.errorResponse);
                    }
                }
                else
                {
                    _logger.LogError("Received invalid Primary key for update UserProfile");
                    errorResponse.Success = true;
                    errorResponse.Error = new Error(GlobalVariables.UserProfileUpdateIDError, GlobalVariables.UserProfileUpdateIDErrorDescription);
                    return Ok(errorResponse);
                }

            }
            catch (Exception ex)
            {
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.UserProfileUpdateError, GlobalVariables.UserProfileUpdateErrorDescription);
                res.status = false;
                res.errorResponse = errorResponse;
                _logger.LogError("Error on Method: updateUserProfile", ex.Message.ToString());
            }
            return Ok(res);

        }
    }
}
