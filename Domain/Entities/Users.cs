using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Users
    {
        public int id { get; set; }
        public int sassid { get; set; }
        public int usertypeid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime dateofbirth { get; set; }
        public int gender { get; set; }
        public int maritalstatus { get; set; }
        public DateTime? anniversarydate { get; set; }
        public decimal height { get; set; }
        public decimal weight { get; set; }
        public DateTime memberfrom { get; set; }
        public DateTime memberto { get; set; }
        public string email { get; set; }
        public long contactnumber { get; set; }
        public string emergencycontactperson { get; set; }
        public long emergencycontactnumber { get; set; }
        public string address { get; set; }
        public int locationstate { get; set; }
        public int locationcountry { get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; }
    }

    public class UserType
    {
        public int id { get; set; }
        public string typecode { get; set; }
        public string typename { get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; }
    }
    public class UserMedicalInfo
    {
        public int id { get; set; }
        public int userid { get; set; }
        public bool medicalcondition { get; set; }
        public string? medicaldetails { get; set; }
        public bool medication { get; set; }
        public string? medicationdetails { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; }
    }
}
