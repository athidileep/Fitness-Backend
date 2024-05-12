using Domain.Entities.Activity;
using Domain.Entities.Lookup;
using Domain.Models.Activity;
using Domain.Models.Common;
using Domain.Models.Goal;
using Domain.Models.Lookup;
using Microsoft.Extensions.Logging;
using Persistance;
using Services.Abstraction;
using Services.Abstraction.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lookup
{
    public class GoalTypeServices : IGoalTypeServicescs
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GoalTypeServices> _logger;
        private readonly ICommonServices _common;
        private readonly IUser _user;

        public GoalTypeServices(AppDbContext appDbContext, ILogger<GoalTypeServices> logger, ICommonServices common, IUser user)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _common = common;
            _user = user;
        }
        

        public async Task<List<GoalTypeModel>> GetGoalType(int iGoalTypeModelId)
        {
            List<GoalTypeModel> lstGoalTypeModel = new List<GoalTypeModel>();
            ErrorResponses errorResponse = new ErrorResponses();
            try
            {
                var dbResponse = _appDbContext.goaltype.Where(x => x.id == (iGoalTypeModelId == 0 ? x.id : iGoalTypeModelId) && x.status == "Active").ToList();
                if (dbResponse != null)
                {
                    foreach (GoalType GoalTypeModelDetails in dbResponse)
                    {
                        GoalTypeModel GoalTypeModelModel = new GoalTypeModel();
                        GoalTypeModelModel.id = GoalTypeModelDetails.id;
                        GoalTypeModelModel.code = GoalTypeModelDetails.code;
                        GoalTypeModelModel.type = GoalTypeModelDetails.type;
                        GoalTypeModelModel.name = GoalTypeModelDetails.name;
                        GoalTypeModelModel.status = GoalTypeModelDetails.status;
                        lstGoalTypeModel.Add(GoalTypeModelModel);

                    }
                    return await Task.FromResult(lstGoalTypeModel);
                }
                else
                {
                    return await Task.FromResult(lstGoalTypeModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Method: GetActivityType", ex.Message);
                return await Task.FromResult(lstGoalTypeModel);
            }
        }
    }

   
}
