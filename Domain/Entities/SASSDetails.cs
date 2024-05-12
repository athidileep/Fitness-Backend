using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SASSDetails
    {
        public int id { get; set; }
        public string sasscode { get; set; }
        public string sassname { get; set; }
        public DateTime memberfrom { get; set; }
        public DateTime memberto { get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; }
    }
}
