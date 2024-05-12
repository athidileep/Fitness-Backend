using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public int? MaritalStatusId { get; set; }
        public string? MaritalStatusName { get; set; }
        public DateTime? AnniversaryDate { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public DateTime MemberFrom { get; set; }
        public DateTime MemberTo { get; set; }
        public string? Email { get; set; }
        public long ContactNumber { get; set; }
        public string? EmergencyContactPerson { get; set; }
        public long? EmergencyContactNumber { get; set; }
        public string? Address { get; set; }
        public int? LocationState { get; set; }
        public string? LocationStateName { get; set; }
        public int? LocationCountry { get; set; }
        public string? LocationCountryName { get; set; }
        public bool MedicalCondition { get; set; }
        public string? MedicalConditionDetails { get; set; }
        public bool Medications { get; set; }
        public string? MedicationDetails { get; set; }
        public string? Status { get; set; }
        public int? SASSId { get; set; }
        public int? UserType { get; set; }
        public string? UserName { get; set; }
    }
}
