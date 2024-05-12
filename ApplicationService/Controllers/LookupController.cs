using Domain.Entities;
using Domain.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using System.Text.Json;

namespace ApplicationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        public readonly ICommonServices _commonServices;
        private readonly ILogger<LookupController> _logger;

        public LookupController(ICommonServices commonServices, ILogger<LookupController> logger)
        {
            this._commonServices = commonServices;
            _logger = logger;
        }

        [HttpGet]
        [Route("/getcountry")]
        public async Task<string?> GetCountry(int iCountryId)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                var dResponse = await _commonServices.GetLocationCountry(iCountryId);
                if (dResponse != null)
                {
                    response.data = dResponse.Cast<dynamic>().ToList();
                    response.totalData = dResponse.Count;
                    sResponse = JsonSerializer.Serialize(response).ToString();
                    return sResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetCountry", ex.Message.ToString());
            }

            return await Task.FromResult(sResponse);
        }

        [HttpGet]
        [Route("/getstate")]
        public async Task<string?> GetState(int iStateId)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                var dResponse = await _commonServices.GetLocationState(iStateId);
                if (dResponse != null)
                {
                    response.data = dResponse.Cast<dynamic>().ToList();
                    response.totalData = dResponse.Count;
                    sResponse = JsonSerializer.Serialize(response).ToString();
                    return sResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetState", ex.Message.ToString());
            }

            return await Task.FromResult(sResponse);
        }

        [HttpGet]
        [Route("/getgender")]
        public async Task<string?> GetGender(int iGenderId)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                var dResponse = await _commonServices.GetGenderType(iGenderId);
                if (dResponse != null)
                {
                    response.data = dResponse.Cast<dynamic>().ToList();
                    response.totalData = dResponse.Count;
                    sResponse = JsonSerializer.Serialize(response).ToString();
                    return sResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetGender", ex.Message.ToString());
            }

            return await Task.FromResult(sResponse);
        }

        [HttpGet]
        [Route("/getmaritalstatus")]
        public async Task<string?> GetMarital(int iMartialId)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                var dResponse = await _commonServices.GetMaritalStatus(iMartialId);
                if (dResponse != null)
                {
                    response.data = dResponse.Cast<dynamic>().ToList();
                    response.totalData = dResponse.Count;
                    sResponse = JsonSerializer.Serialize(response).ToString();
                    return sResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetGender", ex.Message.ToString());
            }

            return await Task.FromResult(sResponse);
        }

        [HttpGet]
        [Route("/getusertype")]
        public async Task<string?> GetUserType(int iUserTypeId)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                var dResponse = await _commonServices.GetUserType(iUserTypeId);
                if (dResponse != null)
                {
                    response.data = dResponse.Cast<dynamic>().ToList();
                    response.totalData = dResponse.Count;
                    sResponse = JsonSerializer.Serialize(response).ToString();
                    return sResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetUserType", ex.Message.ToString());
            }

            return await Task.FromResult(sResponse);
        }
        [HttpGet]
        [Route("/getactivitytype")]
        public async Task<string?> GetActivityType(int iActivityTypeId)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                var dResponse = await _commonServices.GetActivityType(iActivityTypeId);
                if (dResponse != null)
                {
                    response.data = dResponse.Cast<dynamic>().ToList();
                    response.totalData = dResponse.Count;
                    sResponse = JsonSerializer.Serialize(response).ToString();
                    return sResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetActivityType", ex.Message.ToString());
            }

            return await Task.FromResult(sResponse);
        }
        [HttpGet]
        [Route("/getintensitylevel")]
        public async Task<string?> GetIntensityLevel(int iIntensityLevelId)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                var dResponse = await _commonServices.GetIntensityLevel(iIntensityLevelId);
                if (dResponse != null)
                {
                    response.data = dResponse.Cast<dynamic>().ToList();
                    response.totalData = dResponse.Count;
                    sResponse = JsonSerializer.Serialize(response).ToString();
                    return sResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetIntensityLevel", ex.Message.ToString());
            }

            return await Task.FromResult(sResponse);
        }
    }
}
