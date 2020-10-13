using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class Rooms
    {
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int RoomId { get; set; }
        public string Title { get; set; }
        public int? Capacity { get; set; }
        public string Description { get; set; }
        public int? SortOrder { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public bool? IsActive { get; set; }
    }
}
