using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApiResultFormat
    {
        public List<dynamic> data { get; set; }
        public int totalData { get; set; }
    }
}
