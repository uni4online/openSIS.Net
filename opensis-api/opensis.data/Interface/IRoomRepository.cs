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
        public RoomAddViewModel AddRoom(RoomAddViewModel rooms);
        public RoomAddViewModel ViewRoom(RoomAddViewModel rooms);
        public RoomAddViewModel UpdateRoom(RoomAddViewModel rooms);
        public RoomListModel GetAllRooms(RoomListModel roomList);
        public RoomAddViewModel DeleteRoom(RoomAddViewModel room);
    }
}
