using Microsoft.EntityFrameworkCore;
using opensis.data.Interface;
using opensis.data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Factory
{
    public class DbContextFactory : IDbContextFactory
    {
        private string connectionStringTemplate;

        public string TenantName { get; set; }

       

        public DbContextFactory(string connectionStringTemplate)
        {
            this.connectionStringTemplate = connectionStringTemplate;
          
        }

        public CRMContext Create()
        {
            CRMContext context = null;

            if (!string.IsNullOrWhiteSpace(this.TenantName))
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder();
               
                    dbContextOptionsBuilder.UseSqlServer(this.connectionStringTemplate
                                           .Replace("{tenant}", this.TenantName), x => x.UseNetTopologySuite());
                

                context = new CRMContext(dbContextOptionsBuilder.Options);
            }

            return context;
        }


    }
}
