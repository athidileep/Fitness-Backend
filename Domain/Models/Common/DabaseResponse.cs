using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Common
{
    public class DatabaseResponse
    {
        public bool status { get; set; }
        public ErrorResponses? errorResponse { get; set; }
    }
}
