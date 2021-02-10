﻿using opensis.data.ViewModels.CommonModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.ViewModels.Staff
{
    public class StaffCertificatePageResult : CommonFields
    {
        public Guid TenantId { get; set; }
        public int? SchoolId { get; set; }
        public int? StaffId { get; set; }

        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        // public string FilterText { get; set; }

        //public string SchoolNameFilter { get; set; }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public SortingModel SortingModel { get; set; }

        public List<FilterParams> FilterParams { get; set; }
    }
}
