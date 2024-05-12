using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Activity
{
    public class ActivityTypeModel
    { 
        public int Id { get; set; }
        public string ActivityCode { get; set; }
        public string ActivityName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Status { get; set; }
    }
}
