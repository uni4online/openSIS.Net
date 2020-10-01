using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class SchoolMaster
    {
        public SchoolMaster()
        {
            Membership = new HashSet<Membership>();
            Quarters = new HashSet<Quarters>();
            SchoolCalendars = new HashSet<SchoolCalendars>();
            SchoolDetail = new HashSet<SchoolDetail>();
            SchoolPeriods = new HashSet<SchoolPeriods>();
            SchoolYears = new HashSet<SchoolYears>();
            Semesters = new HashSet<Semesters>();
        }

        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public string SchoolInternalId { get; set; }
        public string SchoolAltId { get; set; }
        public string SchoolStateId { get; set; }
        public string SchoolDistrictId { get; set; }
        public string SchoolLevel { get; set; }
        public string SchoolClassification { get; set; }
        public string SchoolName { get; set; }
        public string AlternateName { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Division { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public DateTime? CurrentPeriodEnds { get; set; }
        public int? MaxApiChecks { get; set; }
        public string Features { get; set; }
        public int? PlanId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModifed { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public virtual Plans Plans { get; set; }
        public virtual ICollection<Membership> Membership { get; set; }
        public virtual ICollection<Quarters> Quarters { get; set; }
        public virtual ICollection<SchoolCalendars> SchoolCalendars { get; set; }
        public virtual ICollection<SchoolDetail> SchoolDetail { get; set; }
        public virtual ICollection<SchoolPeriods> SchoolPeriods { get; set; }
        public virtual ICollection<SchoolYears> SchoolYears { get; set; }
        public virtual ICollection<Semesters> Semesters { get; set; }
    }
}
