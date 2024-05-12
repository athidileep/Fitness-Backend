using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Common
{
    public class LocationCountryModel
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Status { get; set; }

    }

    public class LocationStateModel
    {
        public int Id { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int LocationCountryId { get; set; }
        public string? LocationCountryName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Status { get; set; }
    }

    public class GenderTypeModel
    {
        public int Id { get; set; }
        public string GenderCode { get; set; }
        public string GenderName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Status { get; set; }
    }
    public class MaritalStatusModel
    {
        public int Id { get; set; }
        public string Maritalcode { get; set; }
        public string MaritalName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Status { get; set; }
    }
}
