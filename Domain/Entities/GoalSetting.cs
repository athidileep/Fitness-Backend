using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GoalSetting
    {
        public int id { get; set; }
        public int userid { get; set; }
        public int goaltypeid { get; set; }
        public decimal targetweight { get; set; }
        public decimal targetbodyfat { get; set; }
        public decimal targetmusclemass { get; set; }
        public decimal targetduration { get; set; }
        public int workoutfrequency { get; set; }
        public int workoutduration { get; set; }
        public DateTime targetdate { get; set; }
        public decimal protein {  get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; }

    }
}
