using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Room
{
    public class RoomListModel: CommonFields
    {
       
            public List<Rooms> TableroomList { get; set; }
            public Guid? TenantId { get; set; }
            public int? SchoolId { get; set; }

    }    
}
