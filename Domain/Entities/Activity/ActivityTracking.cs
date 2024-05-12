using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Activity
{
    public class ActivityTracking
    {
        public int id { get; set; }
        public int userid { get; set; } 
        public int userdailytrackid { get; set; } 
        public int activitytypeid { get; set; } 
        public int? intensitylevelid { get; set; } 
        public string activityname { get; set; }  
        public int? duration { get; set; } 
        public int? reps { get; set; } 
        public int? setsinfo { get; set; } 
        public int? weight { get; set; } 
        public int? caloriesburned { get; set; } 
        public string? note { get; set; } 
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? status { get; set; }
    }
    public class DailyActivityTracking
    {
        public int id { get; set; }
        public int userid { get; set; }
        public DateTime infodate { get; set; }
        public decimal weight { get; set; }
        public decimal bodyfat { get; set; }
        public decimal musclemass { get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? status { get; set; }
    }
}
