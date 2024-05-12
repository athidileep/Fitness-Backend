using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Lookup;
using System.Text.Json;

namespace ApplicationService.Controllers.Lookup
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalTypeController : ControllerBase
    {
        private readonly IGoalTypeServicescs _goalTypeServices;
        private readonly ILogger<LookupController> _logger;

        public GoalTypeController(IGoalTypeServicescs goalTypeServices, ILogger<LookupController> logger)
        {
            this._goalTypeServices = goalTypeServices;
            _logger = logger;
        }

        [HttpGet]
        [Route("/getgoaltype")]
        public async Task<string> GetGoalType(int iGoalTypeModelId)
        {
            string sResponse = string.Empty;
            ApiResultFormat response = new ApiResultFormat();
            try
            {
                var dResponse = await _goalTypeServices.GetGoalType(iGoalTypeModelId);
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
    }
}
