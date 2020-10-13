using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using opensis.core.Room.Interfaces;
using opensis.data.Models;
using opensis.data.ViewModels.Room;
using opensis.data.ViewModels.School;

namespace opensisAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("{tenant}/Room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomRegisterService _roomRegisterService;
        public RoomController(IRoomRegisterService roomRegisterService)
        {
            _roomRegisterService = roomRegisterService;
        }

        [HttpPost("addRoom")]
        public ActionResult<RoomAddViewModel> AddRoom(RoomAddViewModel room)
        {
            RoomAddViewModel roomAdd = new RoomAddViewModel();
            try
            {
                roomAdd = _roomRegisterService.SaveRoom(room);
            }
            catch (Exception es)
            {
                roomAdd._failure = true;
                roomAdd._message = es.Message;
            }
            return roomAdd;
        }
        [HttpPost("viewRoom")]

        public ActionResult<RoomAddViewModel> ViewRoom(RoomAddViewModel room)
        {
            RoomAddViewModel roomAdd = new RoomAddViewModel();
            try
            {
                roomAdd = _roomRegisterService.ViewRoom(room);
            }
            catch (Exception es)
            {
                roomAdd._failure = true;
                roomAdd._message = es.Message;
            }
            return roomAdd;
        }
        [HttpPut("updateRoom")]

        public ActionResult<RoomAddViewModel> UpdateRoom(RoomAddViewModel room)
        {
            RoomAddViewModel RoomAdd = new RoomAddViewModel();
            try
            {
                RoomAdd = _roomRegisterService.UpdateRoom(room);
            }
            catch (Exception es)
            {
                RoomAdd._failure = true;
                RoomAdd._message = es.Message;
            }
            return RoomAdd;
        }
        [HttpPost("getAllRoom")]

        public ActionResult<RoomListModel> GetAllRoom(RoomListModel room)
        {
            RoomListModel roomList = new RoomListModel();
            try
            {
                roomList = _roomRegisterService.GetAllRoom(room);
            }
            catch (Exception es)
            {
                roomList._message = es.Message;
                roomList._failure = true;
            }
            return roomList;
        }
        [HttpPost("deleteRoom")]

        public ActionResult<RoomAddViewModel> DeleteRoom(RoomAddViewModel room)
        {
            RoomAddViewModel roomlDelete = new RoomAddViewModel();
            try
            {
                roomlDelete = _roomRegisterService.DeleteRoom(room);
            }
            catch (Exception es)
            {
                roomlDelete._failure = true;
                roomlDelete._message = es.Message;
            }
            return roomlDelete;
        }
    }
}