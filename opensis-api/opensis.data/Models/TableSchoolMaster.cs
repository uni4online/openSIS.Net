using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class TableSchoolMaster
    {
        public TableSchoolMaster()
        {
            TableMembership = new HashSet<TableMembership>();
            TableQuarters = new HashSet<TableQuarters>();
            TableSchoolCalendars = new HashSet<TableSchoolCalendars>();
            TableSchoolDetail = new HashSet<TableSchoolDetail>();
            TableSchoolPeriods = new HashSet<TableSchoolPeriods>();
            TableSchoolYears = new HashSet<TableSchoolYears>();
            TableSemesters = new HashSet<TableSemesters>();
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
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModifed { get; set; }

        public virtual TablePlans TablePlans { get; set; }
        public virtual ICollection<TableMembership> TableMembership { get; set; }
        public virtual ICollection<TableQuarters> TableQuarters { get; set; }
        public virtual ICollection<TableSchoolCalendars> TableSchoolCalendars { get; set; }
        public virtual ICollection<TableSchoolDetail> TableSchoolDetail { get; set; }
        public virtual ICollection<TableSchoolPeriods> TableSchoolPeriods { get; set; }
        public virtual ICollection<TableSchoolYears> TableSchoolYears { get; set; }
        public virtual ICollection<TableSemesters> TableSemesters { get; set; }
    }
}
