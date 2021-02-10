﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opensis.core.Period.Interfaces;
using opensis.data.ViewModels.Period;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/Period")]
    [ApiController]
    public class PeriodController : ControllerBase
    {
        private IPeriodService _periodService;
        public PeriodController(IPeriodService periodService)
        {
            _periodService = periodService;
        }

        [HttpPost("addBlock")]
        public ActionResult<BlockAddViewModel> AddBlock(BlockAddViewModel blockAddViewModel)
        {
            BlockAddViewModel blockAdd = new BlockAddViewModel();
            try
            {
                blockAdd = _periodService.AddBlock(blockAddViewModel);
            }
            catch(Exception es)
            {
                blockAdd._failure = false;
                blockAdd._message = es.Message;
            }
            return blockAdd;
        }

        [HttpPut("updateBlock")]
        public ActionResult<BlockAddViewModel> UpdateBlock(BlockAddViewModel blockAddViewModel)
        {
            BlockAddViewModel blockUpdate = new BlockAddViewModel();
            try
            {
                blockUpdate = _periodService.UpdateBlock(blockAddViewModel);
            }
            catch (Exception es)
            {
                blockUpdate._failure = false;
                blockUpdate._message = es.Message;
            }
            return blockUpdate;
        }

        [HttpPost("deleteBlock")]
        public ActionResult<BlockAddViewModel> DeleteBlock(BlockAddViewModel blockAddViewModel)
        {
            BlockAddViewModel blockDelete = new BlockAddViewModel();
            try
            {
                blockDelete = _periodService.DeleteBlock(blockAddViewModel);
            }
            catch (Exception es)
            {
                blockDelete._failure = false;
                blockDelete._message = es.Message;
            }
            return blockDelete;
        }

        [HttpPost("addBlockPeriod")]
        public ActionResult<BlockPeriodAddViewModel> AddBlockPeriod(BlockPeriodAddViewModel blockPeriodAddViewModel)
        {
            BlockPeriodAddViewModel blockPeriodAdd = new BlockPeriodAddViewModel();
            try
            {
                blockPeriodAdd = _periodService.AddBlockPeriod(blockPeriodAddViewModel);
            }
            catch (Exception es)
            {
                blockPeriodAdd._failure = false;
                blockPeriodAdd._message = es.Message;
            }
            return blockPeriodAdd;
        }

        [HttpPut("updateBlockPeriod")]
        public ActionResult<BlockPeriodAddViewModel> UpdateBlockPeriod(BlockPeriodAddViewModel blockPeriodAddViewModel)
        {
            BlockPeriodAddViewModel blockPeriodUpdate = new BlockPeriodAddViewModel();
            try
            {
                blockPeriodUpdate = _periodService.UpdateBlockPeriod(blockPeriodAddViewModel);
            }
            catch (Exception es)
            {
                blockPeriodUpdate._failure = false;
                blockPeriodUpdate._message = es.Message;
            }
            return blockPeriodUpdate;
        }

        [HttpPost("deleteBlockPeriod")]
        public ActionResult<BlockPeriodAddViewModel> DeleteBlockPeriod(BlockPeriodAddViewModel blockPeriodAddViewModel)
        {
            BlockPeriodAddViewModel blockPeriodDelete = new BlockPeriodAddViewModel();
            try
            {
                blockPeriodDelete = _periodService.DeleteBlockPeriod(blockPeriodAddViewModel);
            }
            catch (Exception es)
            {
                blockPeriodDelete._failure = false;
                blockPeriodDelete._message = es.Message;
            }
            return blockPeriodDelete;
        }

        [HttpPost("getAllBlockList")]
        public ActionResult<BlockListViewModel> GetAllBlockList(BlockListViewModel blockListViewModel)
        {
            BlockListViewModel blockList = new BlockListViewModel();
            try
            {
                blockList = _periodService.GetAllBlockList(blockListViewModel);
            }
            catch (Exception es)
            {
                blockList._failure = false;
                blockList._message = es.Message;
            }
            return blockList;
        }

        [HttpPut("updateBlockPeriodSortOrder")]
        public ActionResult<BlockPeriodSortOrderViewModel> UpdateBlockPeriodSortOrder(BlockPeriodSortOrderViewModel blockPeriodSortOrderViewModel)
        {
            BlockPeriodSortOrderViewModel blockPeriodSortOrderUpdate = new BlockPeriodSortOrderViewModel();
            try
            {
                blockPeriodSortOrderUpdate = _periodService.UpdateBlockPeriodSortOrder(blockPeriodSortOrderViewModel);
            }
            catch (Exception es)
            {
                blockPeriodSortOrderUpdate._failure = true;
                blockPeriodSortOrderUpdate._message = es.Message;
            }
            return blockPeriodSortOrderUpdate;
        }
    }
}
