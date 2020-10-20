using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.ViewModels.GradeLevel
{
    public class GradelevelListViewModel : CommonFields
    {
        public List<GradeLevelView> TableGradelevelList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
    }
}
