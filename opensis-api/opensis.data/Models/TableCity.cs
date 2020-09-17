using System;
using System.Collections.Generic;

namespace opensis.data.Models
{
    public partial class TableCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
    }
}
