using opensis.data.Models;
using opensis.data.ViewModels.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
    public interface IRoomRepository
    {       
        //public List<TableRooms> AddRooms(TableRooms rooms);
        public RoomAddViewModel AddRooms(RoomAddViewModel rooms);
        public RoomAddViewModel ViewRooms(RoomAddViewModel rooms);
        public RoomAddViewModel UpdateRooms(RoomAddViewModel rooms);
        public RoomListModel GetAllRooms(RoomListModel roomList);
        public RoomAddViewModel DeleteRooms(RoomAddViewModel room);
    }
}
