using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class TableSchoolDetail
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public int? SchoolId { get; set; }
        public string Affiliation { get; set; }
        public string Associations { get; set; }
        public string Locale { get; set; }
        public string LowestGradeLevel { get; set; }
        public string HighestGradeLevel { get; set; }
        public DateTime? DateSchoolOpened { get; set; }
        public DateTime? DateSchoolClosed { get; set; }
        public bool? Status { get; set; }
        public string Gender { get; set; }
        public bool? Internet { get; set; }
        public bool? Electricity { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string LinkedIn { get; set; }
        public string NameOfPrincipal { get; set; }
        public string NameOfAssistantPrincipal { get; set; }
        public byte[] SchoolLogo { get; set; }
        public bool? RunningWater { get; set; }
        public string MainSourceOfDrinkingWater { get; set; }
        public bool? CurrentlyAvailable { get; set; }
        public string FemaleToiletType { get; set; }
        public short? TotalFemaleToilets { get; set; }
        public short? TotalFemaleToiletsUsable { get; set; }
        public string FemaleToiletAccessibility { get; set; }
        public string MaleToiletType { get; set; }
        public short? TotalMaleToilets { get; set; }
        public short? TotalMaleToiletsUsable { get; set; }
        public string MaleToiletAccessibility { get; set; }
        public string ComonToiletType { get; set; }
        public short? TotalCommonToilets { get; set; }
        public short? TotalCommonToiletsUsable { get; set; }
        public string CommonToiletAccessibility { get; set; }
        public bool? HandwashingAvailable { get; set; }
        public bool? SoapAndWaterAvailable { get; set; }
        public string HygeneEducation { get; set; }

        public virtual TableSchoolMaster TableSchoolMaster { get; set; }
    }
}
