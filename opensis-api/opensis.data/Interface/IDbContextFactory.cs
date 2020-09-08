using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Interface
{
   public interface IDbContextFactory
    {
        string TenantName { get; set; }

        

        CRMContext Create();
    }
}
