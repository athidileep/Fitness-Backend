using Domain;
using Domain.Entities;
using Domain.Models.Activity;
using Domain.Models.Common;
using Domain.Models.Goal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using Services;
using Services.Abstraction;
using System.Text.Json;

namespace ApplicationService.Controllers
{
    [Route("api/[goalsetting]")]
    [ApiController]
    public class GoalSettingController : ControllerBase
    {
        public readonly IGoalSettingServices _goalSettingServices;
        public readonly ILogger<GoalSettingController> _logger;
        public readonly IUser _user;

        public GoalSettingController(IGoalSettingServices goalSettingServices, ILogger<GoalSettingController> logger, IUser user)
        {
            _goalSettingServices = goalSettingServices;
            _logger = logger;
            _user = user;
        }

        [HttpGet]
        [Route("/getgoalsetting/{iUserID}")]
        public async Task<string?> GetGoalSetting(int iUserID)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                var res = await this._goalSettingServices.GetGoalSetting(iUserID);
                response.data = res.Cast<dynamic>().ToList();
                response.totalData = res.Count;
                sResponse = JsonSerializer.Serialize(response).ToString();
                return sResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetGoalSetting", ex.Message.ToString());
                return "Error while getting the records";
            }
        }

        [HttpPost]
        [Route("/insertgoalsetting")]
        public async Task<IActionResult> InsertGoalSetting([FromBody] GoalSettingModelInsertModel _data)
        {

            _logger.LogInformation("Request received for Insert GoalSetting");

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
                var InsertvalidateResponse = await _goalSettingServices.InsertGoalValidation(_data );
                if (!InsertvalidateResponse.Success)
                {
                    _logger.LogError("Received Empty/invalid request JSON", InsertvalidateResponse);
                    return Ok(InsertvalidateResponse);
                }
                var response = await _goalSettingServices.InsertGoalSetting(_data );
                if (response.status)
                {
                    _logger.LogError("Record got inserted for GoalSetting ");
                    errorResponse.Success = true;
                    errorResponse.Error = new Error(GlobalVariables.GoalSettingInsertSuccess, GlobalVariables.GoalSettingInsertSuccessDescription);
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
                errorResponse.Error = new Error(GlobalVariables.GoalSettingInsertError, GlobalVariables.GoalSettingInsertErrorDescription);
                res.status = false;
                res.errorResponse = errorResponse;
                _logger.LogError("Error on Method: InsertGoalSetting", ex.Message.ToString());
            }
            return Ok(res);



        }

        [HttpPost]
        [Route("/updategoalsetting")]
        public async Task<IActionResult> updateGoalSetting([FromBody] GoalSettingUpdateModel _data)
        {

            _logger.LogInformation("Request received for Insert GoalSetting");

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
                var validateResponse = await _goalSettingServices.UpdateGoalValidation(_data);
                if (!validateResponse.Success)
                {
                    errorResponse.Success = false;
                    errorResponse.Error = validateResponse.Error;
                    _logger.LogError("Received Empty/invalid request JSON", errorResponse);
                    return Ok(errorResponse);
                }
                var response = await _goalSettingServices.UpdateGoalSetting(_data);
                if (response.status)
                {
                    _logger.LogError("Record got inserted for Goal ");
                    errorResponse.Success = true;
                    errorResponse.Error = new Error(GlobalVariables.GoalSettingUpdateSuccess, GlobalVariables.ActivityInsertSuccessDescription);
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
                errorResponse.Error = new Error(GlobalVariables.GoalSettingUpdateError, GlobalVariables.GoalSettingUpdateErrorDescription);
                res.status = false;
                res.errorResponse = errorResponse;
                _logger.LogError("Error on Method: InsertGoalSetting", ex.Message.ToString());
            }
            return Ok(res);



        }

        [HttpGet]
        [Route("/gettargetprogress/{iUserID}")]
        public async Task<string?> GetTragetGoalDetails(int iUserID)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                var dResponse = await _goalSettingServices.GetTragetGoalDetails(iUserID);
                if (dResponse != null)
                {
                    response.data = dResponse.Cast<dynamic>().ToList();
                    response.totalData = dResponse.Count;
                    sResponse = JsonSerializer.Serialize(response).ToString();
                    return await Task.FromResult(sResponse);
                }

            }
            catch (Exception ex) { }
            return await Task.FromResult(sResponse);
        }
    }



}

