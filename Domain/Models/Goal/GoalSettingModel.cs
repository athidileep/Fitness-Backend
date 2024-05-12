using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Goal
{
    public class GoalSettingModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GoalTypeId { get; set; }
        public string GoalType { get; set; }
        public decimal TargetWeight { get; set; }
        public decimal TargetBodyFat { get; set; }
        public decimal TargetMuscleMass { get; set; }
        public decimal TargetDuration { get; set; }
        public int WorkoutFrequency { get; set; }
       // public int WorkoutDuration { get; set; }
        public DateTime TargetDate { get; set; }
        
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string? Status { get; set; }
    }

    public class GoalSettingModelInsertModel
    {
        public int UserId { get; set; }
        public int GoalTypeId { get; set; }
        public decimal TargetWeight { get; set; }
        public decimal TargetBodyFat { get; set; }
        public decimal TargetMuscleMass { get; set; }
        public decimal TargetDuration { get; set; }
        public int WorkoutFrequency { get; set; } 
        public DateTime TargetDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Status { get; set; }
    }

    public class GoalSettingUpdateModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GoalTypeId { get; set; }

        public decimal TargetWeight { get; set; }
        public decimal TargetBodyFat { get; set; }
        public decimal TargetMuscleMass { get; set; }
        public decimal TargetDuration { get; set; }
        public int WorkoutFrequency { get; set; }
        //public int WorkoutDuration { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Status { get; set; }
    }
    public class TargetGoalModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GoalTypeId { get; set; } 
        public string? GoalTypeName { get; set; }
        public decimal TargetWeight { get; set; }
        public decimal TargetBodyFat { get; set; }
        public decimal TargetMuscleMass { get; set; }
        public decimal TargetDuration { get; set; }
        public int WorkoutFrequency { get; set; } 
        public DateTime TargetDate { get; set; } 
        public string Status { get; set; }
    }


}
