using Microsoft.EntityFrameworkCore;
using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Period;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class PeriodRepository: IPeriodRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public PeriodRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }

        /// <summary>
        /// Add Block
        /// </summary>
        /// <param name="blockAddViewModel"></param>
        /// <returns></returns>
        public BlockAddViewModel AddBlock(BlockAddViewModel blockAddViewModel)
        {
            try
            {
                int? BlockId = 1;

                var blockData = this.context?.Block.Where(x => x.SchoolId == blockAddViewModel.block.SchoolId && x.TenantId == blockAddViewModel.block.TenantId).OrderByDescending(x => x.BlockId).FirstOrDefault();

                if (blockData != null)
                {
                    BlockId = blockData.BlockId + 1;
                }

                blockAddViewModel.block.BlockId = (int)BlockId;
                blockAddViewModel.block.CreatedOn = DateTime.UtcNow;
                this.context?.Block.Add(blockAddViewModel.block);
                this.context?.SaveChanges();
                blockAddViewModel._failure = false;
            }
            catch (Exception es)
            {
                blockAddViewModel._failure = true;
                blockAddViewModel._message = es.Message;
            }
            return blockAddViewModel;
        }

        /// <summary>
        /// Update Block
        /// </summary>
        /// <param name="blockAddViewModel"></param>
        /// <returns></returns>
        public BlockAddViewModel UpdateBlock(BlockAddViewModel blockAddViewModel)
        {
            try
            {
                var blockUpdate = this.context?.Block.FirstOrDefault(x => x.TenantId == blockAddViewModel.block.TenantId && x.SchoolId == blockAddViewModel.block.SchoolId && x.BlockId == blockAddViewModel.block.BlockId);
                if (blockUpdate != null)
                {
                    blockUpdate.BlockTitle = blockAddViewModel.block.BlockTitle;
                    blockUpdate.BlockSortOrder = blockAddViewModel.block.BlockSortOrder;
                    blockUpdate.UpdatedOn = DateTime.UtcNow;
                    blockUpdate.UpdatedBy = blockAddViewModel.block.UpdatedBy;
                    this.context?.SaveChanges();
                    blockAddViewModel._failure = false;
                    blockAddViewModel._message = "Block Updated Successfully";
                }
            }
            catch (Exception es)
            {
                blockAddViewModel._failure = true;
                blockAddViewModel._message = es.Message;
            }
            return blockAddViewModel;
        }

        /// <summary>
        /// Delete Block
        /// </summary>
        /// <param name="blockAddViewModel"></param>
        /// <returns></returns>
        public BlockAddViewModel DeleteBlock(BlockAddViewModel blockAddViewModel)
        {
            try
            {
                var blockDelete = this.context?.Block.FirstOrDefault(x => x.TenantId == blockAddViewModel.block.TenantId && x.SchoolId == blockAddViewModel.block.SchoolId && x.BlockId == blockAddViewModel.block.BlockId);

                if (blockDelete != null)
                {
                    var blockPeriodExits = this.context?.BlockPeriod.FirstOrDefault(x => x.TenantId == blockDelete.TenantId && x.SchoolId == blockDelete.SchoolId && x.BlockId == blockDelete.BlockId);
                    if (blockPeriodExits != null)
                    {
                        blockAddViewModel._failure = true;
                        blockAddViewModel._message = "Cannot delete because it has association.";
                    }
                    else
                    {
                        this.context?.Block.Remove(blockDelete);
                        this.context?.SaveChanges();
                        blockAddViewModel._failure = false;
                        blockAddViewModel._message = "Deleted Successfully";
                    }
                }
            }
            catch (Exception es)
            {
                blockAddViewModel._failure = true;
                blockAddViewModel._message = es.Message;
            }
            return blockAddViewModel;
        }

        /// <summary>
        /// Add Block Period
        /// </summary>
        /// <param name="blockPeriodAddViewModel"></param>
        /// <returns></returns>
        public BlockPeriodAddViewModel AddBlockPeriod(BlockPeriodAddViewModel blockPeriodAddViewModel)
        {
            try
            {
                int? PeriodId = 1;
                int? SortOrder = 1;

                var blockPeriodData = this.context?.BlockPeriod.Where(x => x.TenantId == blockPeriodAddViewModel.blockPeriod.TenantId && x.SchoolId == blockPeriodAddViewModel.blockPeriod.SchoolId).OrderByDescending(x => x.PeriodId).FirstOrDefault();

                if (blockPeriodData != null)
                {
                    PeriodId = blockPeriodData.PeriodId + 1;
                }

                var sortOrderData = this.context?.BlockPeriod.Where(x => x.TenantId == blockPeriodAddViewModel.blockPeriod.TenantId && x.SchoolId == blockPeriodAddViewModel.blockPeriod.SchoolId && x.BlockId== blockPeriodAddViewModel.blockPeriod.BlockId).OrderByDescending(x => x.PeriodSortOrder).FirstOrDefault();

                if (sortOrderData != null)
                {
                    SortOrder = sortOrderData.PeriodSortOrder + 1;
                }
                blockPeriodAddViewModel.blockPeriod.PeriodId = (int)PeriodId;
                blockPeriodAddViewModel.blockPeriod.PeriodSortOrder = (int)SortOrder;
                blockPeriodAddViewModel.blockPeriod.CreatedOn = DateTime.UtcNow;
                this.context?.BlockPeriod.Add(blockPeriodAddViewModel.blockPeriod);
                this.context?.SaveChanges();
                blockPeriodAddViewModel._failure = false;
            }
            catch (Exception es)
            {
                blockPeriodAddViewModel._failure = true;
                blockPeriodAddViewModel._message = es.Message;
            }
            return blockPeriodAddViewModel;
        }

        /// <summary>
        /// Update BlockPeriod
        /// </summary>
        /// <param name="blockPeriodAddViewModel"></param>
        /// <returns></returns>
        public BlockPeriodAddViewModel UpdateBlockPeriod(BlockPeriodAddViewModel blockPeriodAddViewModel)
        {
            try
            {
                var blockPeriodUpdate = this.context?.BlockPeriod.FirstOrDefault(x => x.TenantId == blockPeriodAddViewModel.blockPeriod.TenantId && x.SchoolId == blockPeriodAddViewModel.blockPeriod.SchoolId && x.PeriodId == blockPeriodAddViewModel.blockPeriod.PeriodId);
                if (blockPeriodUpdate != null)
                {
                    blockPeriodUpdate.PeriodTitle = blockPeriodAddViewModel.blockPeriod.PeriodTitle;
                    blockPeriodUpdate.PeriodShortName = blockPeriodAddViewModel.blockPeriod.PeriodShortName;
                    blockPeriodUpdate.PeriodStartTime = blockPeriodAddViewModel.blockPeriod.PeriodStartTime;
                    blockPeriodUpdate.PeriodEndTime = blockPeriodAddViewModel.blockPeriod.PeriodEndTime;
                    blockPeriodUpdate.UpdatedOn = DateTime.UtcNow;
                    blockPeriodUpdate.UpdatedBy = blockPeriodAddViewModel.blockPeriod.UpdatedBy;
                    this.context?.SaveChanges();
                    blockPeriodAddViewModel._failure = false;
                    blockPeriodAddViewModel._message = "Block-Period Updated Successfully";
                }
            }
            catch (Exception es)
            {
                blockPeriodAddViewModel._failure = true;
                blockPeriodAddViewModel._message = es.Message;
            }
            return blockPeriodAddViewModel;
        }

        /// <summary>
        /// Delete BlockPeriod
        /// </summary>
        /// <param name="blockPeriodAddViewModel"></param>
        /// <returns></returns>
        public BlockPeriodAddViewModel DeleteBlockPeriod(BlockPeriodAddViewModel blockPeriodAddViewModel)
        {
            try
            {
                var blockPeriodDelete = this.context?.BlockPeriod.FirstOrDefault(x => x.TenantId == blockPeriodAddViewModel.blockPeriod.TenantId && x.SchoolId == blockPeriodAddViewModel.blockPeriod.SchoolId && x.PeriodId == blockPeriodAddViewModel.blockPeriod.PeriodId);

                if (blockPeriodDelete != null)
                {
                    this.context?.BlockPeriod.Remove(blockPeriodDelete);
                    this.context?.SaveChanges();
                    blockPeriodAddViewModel._failure = false;
                    blockPeriodAddViewModel._message = "Deleted Successfully";
                }
            }
            catch (Exception es)
            {
                blockPeriodAddViewModel._failure = true;
                blockPeriodAddViewModel._message = es.Message;
            }
            return blockPeriodAddViewModel;
        }

        /// <summary>
        /// Get All BlockList
        /// </summary>
        /// <param name="blockListViewModel"></param>
        /// <returns></returns>
        public BlockListViewModel GetAllBlockList(BlockListViewModel blockListViewModel)
        {
            BlockListViewModel blockList = new BlockListViewModel();
            try
            {
                var blockDataList = this.context?.Block.Include(x => x.BlockPeriod).Where(x => x.TenantId == blockListViewModel.TenantId && x.SchoolId == blockListViewModel.SchoolId).OrderBy(x => x.BlockSortOrder).ToList();

                if (blockDataList.Count > 0)
                {
                    foreach (var block in blockDataList)
                    {
                        block.BlockPeriod = block.BlockPeriod.OrderBy(x => x.PeriodSortOrder).ToList();
                    }
                    blockListViewModel.blockList = blockDataList;
                    blockListViewModel._tenantName = blockListViewModel._tenantName;
                    blockListViewModel._token = blockListViewModel._token;
                    blockListViewModel._failure = false;
                }
                else
                {
                    blockListViewModel.blockList = null;
                    blockListViewModel._message = NORECORDFOUND;
                    blockListViewModel._failure = true;
                    blockListViewModel._tenantName = blockListViewModel._tenantName;
                    blockListViewModel._token = blockListViewModel._token;
                }              
            }
            catch (Exception es)
            {
                blockListViewModel._message = es.Message;
                blockListViewModel._failure = true;
                blockListViewModel._tenantName = blockListViewModel._tenantName;
                blockListViewModel._token = blockListViewModel._token;
            }
            return blockListViewModel;
        }

        /// <summary>
        /// Update BlockPeriod SortOrder
        /// </summary>
        /// <param name="blockPeriodSortOrderViewModel"></param>
        /// <returns></returns>
        public BlockPeriodSortOrderViewModel UpdateBlockPeriodSortOrder(BlockPeriodSortOrderViewModel blockPeriodSortOrderViewModel)
        {
            try
            {
                var blockPeriodRecords = new List<BlockPeriod>();

                var targetBlockPeriod = this.context?.BlockPeriod.FirstOrDefault(x => x.PeriodSortOrder == blockPeriodSortOrderViewModel.PreviousSortOrder && x.SchoolId == blockPeriodSortOrderViewModel.SchoolId && x.TenantId == blockPeriodSortOrderViewModel.TenantId && x.BlockId == blockPeriodSortOrderViewModel.BlockId);
                targetBlockPeriod.PeriodSortOrder = blockPeriodSortOrderViewModel.CurrentSortOrder;

                if (blockPeriodSortOrderViewModel.PreviousSortOrder > blockPeriodSortOrderViewModel.CurrentSortOrder)
                {
                    blockPeriodRecords = this.context?.BlockPeriod.Where(x => x.PeriodSortOrder >= blockPeriodSortOrderViewModel.CurrentSortOrder && x.PeriodSortOrder < blockPeriodSortOrderViewModel.PreviousSortOrder && x.TenantId == blockPeriodSortOrderViewModel.TenantId && x.SchoolId == blockPeriodSortOrderViewModel.SchoolId && x.BlockId == blockPeriodSortOrderViewModel.BlockId).ToList();

                    if (blockPeriodRecords.Count > 0)
                    {
                        blockPeriodRecords.ForEach(x => x.PeriodSortOrder = x.PeriodSortOrder + 1);
                    }
                }
                if (blockPeriodSortOrderViewModel.CurrentSortOrder > blockPeriodSortOrderViewModel.PreviousSortOrder)
                {
                    blockPeriodRecords = this.context?.BlockPeriod.Where(x => x.PeriodSortOrder <= blockPeriodSortOrderViewModel.CurrentSortOrder && x.PeriodSortOrder > blockPeriodSortOrderViewModel.PreviousSortOrder && x.SchoolId == blockPeriodSortOrderViewModel.SchoolId && x.TenantId == blockPeriodSortOrderViewModel.TenantId && x.BlockId == blockPeriodSortOrderViewModel.BlockId).ToList();
                    if (blockPeriodRecords.Count > 0)
                    {
                        blockPeriodRecords.ForEach(x => x.PeriodSortOrder = x.PeriodSortOrder - 1);
                    }
                }
                this.context?.SaveChanges();
                blockPeriodSortOrderViewModel._failure = false;
            }
            catch (Exception es)
            {
                blockPeriodSortOrderViewModel._message = es.Message;
                blockPeriodSortOrderViewModel._failure = true;
            }
            return blockPeriodSortOrderViewModel;
        }
    }
}
