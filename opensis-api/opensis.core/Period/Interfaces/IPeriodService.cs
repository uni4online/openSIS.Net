using opensis.data.ViewModels.Period;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.Period.Interfaces
{
    public interface IPeriodService
    {
        public BlockAddViewModel AddBlock(BlockAddViewModel blockAddViewModel);
        public BlockAddViewModel UpdateBlock(BlockAddViewModel blockAddViewModel);
        public BlockAddViewModel DeleteBlock(BlockAddViewModel blockAddViewModel);
        public BlockPeriodAddViewModel AddBlockPeriod(BlockPeriodAddViewModel blockAddViewModel);
        public BlockPeriodAddViewModel UpdateBlockPeriod(BlockPeriodAddViewModel blockAddViewModel);
        public BlockPeriodAddViewModel DeleteBlockPeriod(BlockPeriodAddViewModel blockAddViewModel);
        public BlockListViewModel GetAllBlockList(BlockListViewModel blockListViewModel);
        public BlockPeriodSortOrderViewModel UpdateBlockPeriodSortOrder(BlockPeriodSortOrderViewModel blockPeriodSortOrderViewModel);
    }
}
