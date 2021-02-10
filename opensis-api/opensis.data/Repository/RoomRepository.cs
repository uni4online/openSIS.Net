﻿using opensis.data.Helper;
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
            try
            {
                var checkRoomTitle = this.context?.Rooms.Where(x => x.SchoolId == rooms.tableRoom.SchoolId && x.TenantId == rooms.tableRoom.TenantId && x.Title.ToLower() == rooms.tableRoom.Title.ToLower()).FirstOrDefault();

                if (checkRoomTitle !=null)
                {
                    rooms._failure = true;
                    rooms._message = "Room Title Already Exists";
                }
                else
                {
                    //int? RoomlId = Utility.GetMaxPK(this.context, new Func<Rooms, int>(x => x.RoomId));

                    int? RoomlId = 1;

                    var RoomlIdData = this.context?.Rooms.Where(x => x.SchoolId == rooms.tableRoom.SchoolId && x.TenantId == rooms.tableRoom.TenantId).OrderByDescending(x => x.RoomId).FirstOrDefault();

                    if (RoomlIdData != null)
                    {
                        RoomlId = RoomlIdData.RoomId + 1;
                    }
                    rooms.tableRoom.RoomId = (int)RoomlId;
                    rooms.tableRoom.LastUpdated = DateTime.UtcNow;
                    rooms.tableRoom.TenantId = rooms.tableRoom.TenantId;
                    rooms.tableRoom.IsActive = rooms.tableRoom.IsActive;
                    this.context?.Rooms.Add(rooms.tableRoom);
                    this.context?.SaveChanges();
                }               
            }
            catch (Exception es)
            {
                rooms._message = es.Message;
                rooms._failure = true;
            }            
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
                if (roomMaster !=null)
                {
                    var checkRoomTitle = this.context?.Rooms.Where(x => x.SchoolId == room.tableRoom.SchoolId && x.TenantId == room.tableRoom.TenantId && x.RoomId != room.tableRoom.RoomId && x.Title.ToLower() == room.tableRoom.Title.ToLower()).FirstOrDefault();

                    if (checkRoomTitle !=null)
                    {
                        room._failure = true;
                        room._message = "Room Title Already Exists";
                    }
                    else
                    {
                        room.tableRoom.LastUpdated = DateTime.UtcNow;
                        this.context.Entry(roomMaster).CurrentValues.SetValues(room.tableRoom);
                        this.context?.SaveChanges();
                        room._failure = false;
                    }                    
                }
                else
                {
                    room.tableRoom = null;
                    room._failure = true;
                    room._message = NORECORDFOUND;
                }
            }
            catch (Exception ex)
            {
                
                room._failure = true;
                room._message = ex.Message;
                
            }
            return room;
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

                var room = this.context?.Rooms.Where(x => x.TenantId == roomList.TenantId && x.SchoolId == roomList.SchoolId && x.IsActive == true).OrderBy(x => x.SortOrder).ToList();
                if (room.Count > 0)
                {
                    roomListModel.TableroomList = room;
                    roomListModel._tenantName = roomList._tenantName;
                    roomListModel._token = roomList._token;
                    roomListModel._failure = false;
                }
                else
                {
                    roomListModel.TableroomList = null;
                    roomListModel._tenantName = roomList._tenantName;
                    roomListModel._token = roomList._token;
                    roomListModel._failure = true;
                    roomListModel._message = NORECORDFOUND;
                }
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
    
