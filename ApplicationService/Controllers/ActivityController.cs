using System.Text.Json;
using Domain;
using Domain.Entities;
using Domain.Models.Activity;
using Domain.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstraction;

namespace ApplicationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        public readonly IActivityService _activityService;
        public readonly IUser _user;
        public readonly ICommonServices _common;
        public readonly ILogger<ActivityController> _logger;

        public ActivityController(IActivityService activityService, ILogger<ActivityController> logger, ICommonServices common, IUser user)
        {
            this._activityService = activityService;
            this._logger = logger;
            this._common = common;
            this._user = user;
        }

        [HttpPost]
        [Route("/insertactivitytracking")]
        public async Task<IActionResult> InsertActivityTracking([FromBody] ActivitysTrackingInsertModel _data)
        {
            _logger.LogInformation("Request received for Insert ActivityTracking");

            ErrorResponses errorResponse = new ErrorResponses();
            DatabaseResponse res = new DatabaseResponse();
            try
            { 
                if (_data == null)
                {
                    errorResponse.Success = false;
                    errorResponse.Error = new Error(GlobalVariables.emptyJSON, GlobalVariables.emptyJSONDescription);
                    _logger.LogError("Received invalid request JSON", errorResponse);
                    return Ok(errorResponse);
                } 
                var DailyvalidateResponse = await _activityService.InsertDailyActivityValidation(_data);
                if (!DailyvalidateResponse.Success)
                {
                    _logger.LogError("Received Empty/invalid request JSON", DailyvalidateResponse);
                    return Ok(DailyvalidateResponse);
                }
                else
                {
                    var  validateResponse = await _activityService.InsertActivityTrackingValidation(_data);
                    if (!validateResponse.Success)
                    {
                        _logger.LogError("Received Empty/invalid request JSON", validateResponse);
                        return Ok(validateResponse);
                    }
                }
                var response = await _activityService.InsertDailyActivityTracking(_data );
                if (response.status)
                {
                    _logger.LogError("Record got inserted for Activity ");
                    errorResponse.Success = true;
                    errorResponse.Error = new Error(GlobalVariables.ActivityInsertSuccess, GlobalVariables.ActivityInsertSuccessDescription);
                    return Ok(errorResponse);

                }
                else
                {
                    return Ok(response.errorResponse);
                }
            }
            catch (Exception ex)
            {
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.ActivityInsertError, GlobalVariables.ActivityInsertErrorDescription);
                res.status = false;
                res.errorResponse = errorResponse;
                _logger.LogError("Error on Method: InsertActivityTracking", ex.Message.ToString());
            }
            return Ok(res);
        }

        [HttpGet]
        [Route("/getactivitytracking/{iUserID}")]   
        public async Task<string> GetActivityTracking(int iUserID)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            { 
                var dResponse = await _activityService.GetActivityTracking(iUserID);
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
        [Route("/updateactivitytracking")]
        public async Task<IActionResult> UpdateActivityTracking([FromBody] ActivityTrackingResponseModel _data)
        {

            _logger.LogInformation("Request received for Update Activity Tracking");

            ErrorResponses errorResponse = new ErrorResponses();
            DatabaseResponse res = new DatabaseResponse();
            try
            {

                if (_data.Id > 0)
                {
                    var validateResponse = await _activityService.UpdateActivityValidation(_data);
                    if (!validateResponse.Success)
                    {
                        errorResponse.Success = false;
                        errorResponse.Error = validateResponse.Error;
                        _logger.LogError("Received Empty/invalid request JSON", errorResponse);
                        return Ok(errorResponse);
                    }
                    var response = await _activityService.UpdateActivityDailyTracking(_data);
                    if (response.status)
                    {
                        _logger.LogError("Record got updated for UpdateActivityTracking");
                        errorResponse.Success = true;
                        errorResponse.Error = new Error(GlobalVariables.ActivityeUpdateSuccess, GlobalVariables.ActivityUpdateSuccessDescription);
                        return Ok(errorResponse);
                    }
                    else
                    {
                        return Ok(response.errorResponse);
                    }
                }
                else
                {
                    _logger.LogError("Received invalid Primary key for update ActivityTracking");
                    errorResponse.Success = true;
                    errorResponse.Error = new Error(GlobalVariables.ActivityUpdateIDError, GlobalVariables.ActivityUpdateIDErrorDescription);
                    return Ok(errorResponse);
                }

            }
            catch (Exception ex)
            {
                errorResponse.Success = false;
                errorResponse.Error = new Error(GlobalVariables.ActivityUpdateError, GlobalVariables.ActivityUpdateErrorDescription);
                res.status = false;
                res.errorResponse = errorResponse;
                _logger.LogError("Error on Method: UpdateActivityTracking", ex.Message.ToString());
            }
            return Ok(res);
        }
    }
}
