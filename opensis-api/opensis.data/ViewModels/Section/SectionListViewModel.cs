using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Section
{
    public class SectionListViewModel: CommonFields
    {
        public List<Sections> tableSectionsList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
    }
}
