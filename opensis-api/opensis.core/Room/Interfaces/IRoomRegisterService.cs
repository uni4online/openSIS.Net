using opensis.data.Models;
using opensis.data.ViewModels.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.core.Room.Interfaces
{
    public interface IRoomRegisterService
    {
        //public RoomListViewModel getAllRooms(RoomListViewModel objModel);

        public RoomAddViewModel SaveRoom(RoomAddViewModel room);
        public RoomAddViewModel ViewRoom(RoomAddViewModel room);
        public RoomAddViewModel UpdateRoom(RoomAddViewModel room);
        public RoomListModel GetAllRoom(RoomListModel roomList);
        public RoomAddViewModel DeleteRoom(RoomAddViewModel room);

    }
}
