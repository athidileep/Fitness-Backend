using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LocationState
    {
        public int id { get; set; }
        public string statecode { get; set; }
        public string statename { get; set; }
        public int locationcountryid { get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; }
    }

    public class LocationCountry
    {
        public int id { get; set; }
        public string countrycode { get; set; }
        public string countryname { get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; }
    }

    public class GenderType
    {
        public int id { get; set; }
        public string gendercode { get; set; }
        public string gendername { get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; }
    }
    public class MaritalStatus
    {
        public int id { get; set; }
        public string maritalcode { get; set; }
        public string maritalname { get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; }
    }

}
