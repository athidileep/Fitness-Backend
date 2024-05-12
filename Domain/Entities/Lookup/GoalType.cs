﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Lookup
{
    public  class GoalType
    {
        public int id { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; }

    }
}
