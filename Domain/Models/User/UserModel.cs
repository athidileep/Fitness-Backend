using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class UserInsertModel
    {
        public int sassid { get; set; }
        public int usertypeid { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime dateofbirth { get; set; }
        public int gender { get; set; }
        public int? maritalstatus { get; set; }
        public DateTime? anniversarydate { get; set; }
        public decimal height { get; set; }
        public decimal weight { get; set; }
        public DateTime memberfrom { get; set; }
        public DateTime memberto { get; set; }
        public string? email { get; set; }
        public long contactnumber { get; set; }
        public string? emergencycontactperson { get; set; }
        public long? emergencycontactnumber { get; set; }
        public string? address { get; set; }
        public int? locationstate { get; set; }
        public int? locationcountry { get; set; }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? modifiedby { get; set; }
        public string? status { get; set; } 
    }
    public class UserUpdateModel 
    {
        public int Id { get; set; }
        public int SASSId { get; set; }
        public int UserType { get; set; } 
        public string UserName { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenderId { get; set; } 
        public int MaritalStatusId { get; set; } 
        public DateTime? AnniversaryDate { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public DateTime MemberFrom { get; set; }
        public DateTime MemberTo { get; set; }
        public string Email { get; set; }
        public long ContactNumber { get; set; }
        public string EmergencyContactPerson { get; set; }
        public long EmergencyContactNumber { get; set; }
        public string Address { get; set; }
        public int LocationState { get; set; } 
        public int LocationCountry { get; set; } 
        public bool MedicalCondition { get; set; }
        public string? MedicalConditionDetails { get; set; }
        public bool Medications { get; set; }
        public string? MedicationDetails { get; set; }
        public string? Status { get; set; } 
    }

    public class UserTypeModel
    {
        public int Id { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Status { get; set; }
    }


}
