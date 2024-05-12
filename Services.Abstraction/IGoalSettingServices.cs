using Domain.Models.Common;
using Domain.Models.Goal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IGoalSettingServices
    {
        Task<List<GoalSettingModel>> GetGoalSetting(int iUserId);
        Task<DatabaseResponse> InsertGoalSetting(GoalSettingModelInsertModel goalSettingModel );
        Task<DatabaseResponse> UpdateGoalSetting(GoalSettingUpdateModel goalSettingModel);
        Task<ErrorResponses> InsertGoalValidation(GoalSettingModelInsertModel goalInsertModel );

        Task<ErrorResponses> UpdateGoalValidation(GoalSettingUpdateModel goalUpdateModel);


        Task<List<TargetGoalModel>> GetTragetGoalDetails(int UserId);
    }
}
