using opensis.data.Helper;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.ViewModels.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace opensis.data.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private CRMContext context;
        private static readonly string NORECORDFOUND = "NO RECORD FOUND";
        public RoomRepository(IDbContextFactory dbContextFactory)
        {
            this.context = dbContextFactory.Create();
        }
        //public List<TableRooms> GetAllRooms()
        //{
        //    return this.context?.TableRooms.ToList<TableRooms>();
        //}
        //
        /// <summary>
        /// Room Add
        /// </summary>
        /// <param name="rooms"></param>
        /// <returns></returns>
        public RoomAddViewModel AddRoom(RoomAddViewModel rooms)
        {
            int? RoomlId = Utility.GetMaxPK(this.context, new Func<Rooms, int>(x => x.RoomId));
            rooms.tableRoom.RoomId = (int)RoomlId;
            rooms.tableRoom.LastUpdated = DateTime.UtcNow;
            rooms.tableRoom.TenantId = rooms.tableRoom.TenantId;
            rooms.tableRoom.IsActive = rooms.tableRoom.IsActive;
            this.context?.Rooms.Add(rooms.tableRoom);
            this.context?.SaveChanges();

            return rooms;
        }
        /// <summary>
        /// Get Room By Id
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public RoomAddViewModel ViewRoom(RoomAddViewModel room)
        {
            try
            {
                RoomAddViewModel roomAddViewModel = new RoomAddViewModel();
                var roomMaster = this.context?.Rooms.FirstOrDefault(x => x.TenantId == room.tableRoom.TenantId && x.SchoolId == room.tableRoom.SchoolId && x.RoomId == room.tableRoom.RoomId);
                if (roomMaster != null)
                {
                    room.tableRoom = roomMaster;                    
                    room._tenantName = room._tenantName;
                    room._failure = false;
                    return room;
                }
                else
                {
                    roomAddViewModel._failure = true;
                    roomAddViewModel._message = NORECORDFOUND;
                    return roomAddViewModel;
                }
            }
            catch (Exception es)
            {

                throw;
            }
        }
        /// <summary>
        /// Update Room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public RoomAddViewModel UpdateRoom(RoomAddViewModel room)
        {
            try
            {
                var roomMaster = this.context?.Rooms.FirstOrDefault(x => x.TenantId == room.tableRoom.TenantId && x.SchoolId == room.tableRoom.SchoolId && x.RoomId == room.tableRoom.RoomId);
                roomMaster.SchoolId = room.tableRoom.SchoolId;
                roomMaster.TenantId = room.tableRoom.TenantId;
                roomMaster.Title = room.tableRoom.Title;
                roomMaster.Capacity = room.tableRoom.Capacity;
                roomMaster.Description = room.tableRoom.Description;
                roomMaster.SortOrder = room.tableRoom.SortOrder;
                roomMaster.IsActive = room.tableRoom.IsActive;
                room.tableRoom.LastUpdated = DateTime.UtcNow;
                roomMaster.UpdatedBy = room.tableRoom.UpdatedBy;
                this.context?.SaveChanges();
                room._failure = false;
                return room;
            }
            catch (Exception ex)
            {
                room.tableRoom = null;
                room._failure = true;
                room._message = NORECORDFOUND;
                return room;
            }
        }
        /// <summary>
        /// Get All Room
        /// </summary>
        /// <param name="roomList"></param>
        /// <returns></returns>
        public RoomListModel GetAllRooms(RoomListModel roomList)
        {
            RoomListModel roomListModel = new RoomListModel();
            try
            {

                var room = this.context?.Rooms.Where(x => x.TenantId == roomList.TenantId && x.SchoolId == roomList.SchoolId).OrderBy(x => x.SortOrder).ToList();
                roomListModel.TableroomList = room;
                roomListModel._tenantName = roomList._tenantName;
                roomListModel._token = roomList._token;
                roomListModel._failure = false;
            }
            catch (Exception es)
            {
                roomListModel._message = es.Message;
                roomListModel._failure = true;
                roomListModel._tenantName = roomList._tenantName;
                roomListModel._token = roomList._token;
            }
            return roomListModel;

        }
        /// <summary>
        /// Delete Room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public RoomAddViewModel DeleteRoom(RoomAddViewModel room)
        {
            try
            {
                var Room= this.context?.Rooms.FirstOrDefault(x => x.TenantId == room.tableRoom.TenantId && x.SchoolId == room.tableRoom.SchoolId && x.RoomId == room.tableRoom.RoomId);
                this.context?.Rooms.Remove(Room);
                this.context?.SaveChanges();
                room._failure = false;
                room._message = "Deleted";
            }
            catch (Exception es)
            {
                room._failure = true;
                room._message = es.Message;
            }
            return room;
        }
    }
}
    
