using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Period
{
    public class BlockListViewModel : CommonFields
    {
        public BlockListViewModel()
        {
            getBlockListForView = new List<GetBlockListForView>();
        }
        public List<GetBlockListForView> getBlockListForView { get; set; }
        //public List<Block> blockList { get; set; }
        public Guid? TenantId { get; set; }
        public int? SchoolId { get; set; }
    }
}
