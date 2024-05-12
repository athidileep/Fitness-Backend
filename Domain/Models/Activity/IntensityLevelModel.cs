using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Activity
{
    public class IntensityLevelModel
    {
        public int Id { get; set; }
        public string IntensityCode { get; set; }
        public string IntensityName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Status { get; set; }
    }

}
