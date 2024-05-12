using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Activity
{ 

    public class ActivitysTrackingInsertModel
    {
        public int UserId { get; set; }
        public int UserDailyTrackId { get; set; } 
        public decimal BodyWeight { get; set; } 
        public decimal BodyFat { get; set; }
        public decimal MuscleMass { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string? Status { get; set; }
        public List<ActivityTotalData> activityTotalData { get; set; }
        public class ActivityTotalData
        {
            public int ActivityTypeId { get; set; }
            public string ActivityName { get; set; }
            public int? Duration { get; set; }
            public int? Reps { get; set; }
            public int? SetsInfo { get; set; }
            public int BodyWeight { get; set; }
            public int? LiftWeight { get; set; }
            public int? CaloriesBurned { get; set; }
            public int? IntensityLevelId { get; set; }
            public string Notes { get; set; }
            public DateTime Date { get; set; }
        }
    }
    public class DailyActivityTrackModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime InfoDate { get; set; }
        public decimal Weight { get; set; }
        public decimal BodyFat { get; set; }
        public decimal MuscleMass { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Status { get; set; }
    }
    public class ActivityTrackingResponseModel
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal BodyWeight { get; set; }
        public decimal BodyFat { get; set; }
        public decimal MuscleMass { get; set; }
        public int ActivityTrackId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ActivityTypeName { get; set; }
        public string ActivityName { get; set; }
        public int? Duration { get; set; }
        public int? Reps { get; set; }
        public int? SetsInfo { get; set; }
        public int? LiftWeight { get; set; }
        public int? CaloriesBurned { get; set; }
        public int IntensityLevelId { get; set; }
        public string IntensityLevelIdName {get;set;}
        public string? Notes { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Status { get; set; }
    }


}
