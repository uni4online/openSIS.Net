using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Period
{
    public class GetBlockListForView
    {
        public GetBlockListForView()
        {
            BlockPeriod = new List<BlockPeriod>();
        }
        //public List<Block> blockList { get; set; }
        public Guid TenantId { get; set; }
        public int SchoolId { get; set; }
        public int BlockId { get; set; }
        public string BlockTitle { get; set; }
        public long? BlockSortOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public List<BlockPeriod> BlockPeriod { get; set; }
    }
}
