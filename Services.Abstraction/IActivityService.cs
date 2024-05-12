using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Activity;
using Domain.Models.Common;

namespace Services.Abstraction
{
    public interface IActivityService
    {
        Task<List<ActivityTrackingResponseModel>> GetActivityTracking(int UserId);
        Task<bool> ActivityAvailabilityCheck(int iActivityId);
        Task<DatabaseResponse> InsertDailyActivityTracking(ActivitysTrackingInsertModel activityModel ); 
        Task<DatabaseResponse> UpdateActivityDailyTracking(ActivityTrackingResponseModel activityModel);
        Task<ErrorResponses> UpdateActivityValidation(ActivityTrackingResponseModel activityUpdateModel);
        Task<ErrorResponses> InsertActivityTrackingValidation(ActivitysTrackingInsertModel activityModel);
        Task<ErrorResponses> InsertDailyActivityValidation(ActivitysTrackingInsertModel activityInsertModel );
    }
}
