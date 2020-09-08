using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace opensis.data.Models
{
    public class CRMContext : DbContext
    {
        private DbContextOptions contextOptions;

        public CRMContext(): base()
        {
        }

        public CRMContext(DbContextOptions options) : base(options)
        {
            this.contextOptions = options;
        }

        public DbSet<Schools> tblSchool { get; set; }
        public DbSet<Users> tblUser { get; set; }

        public DbSet<tblUserMaster> Table_User_Master { get; set; }

        public DbSet<tblSchoolMaster> Table_School_Master { get; set; }
        public DbSet<tblSchoolDetail> Table_School_Detail { get; set; }
        public DbSet<tblPlans> Table_Plans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schools>().HasKey(e => e.school_id);
            modelBuilder.Entity<tblUserMaster>().HasKey(ba => new { ba.Tenant_Id, ba.School_id, ba.User_id });
            modelBuilder.Entity<tblPlans>().HasKey(ba => new { ba.Tenant_id, ba.School_id, ba.id });
            modelBuilder.Entity<tblSchoolMaster>().HasKey(ba => new { ba.Tenant_Id, ba.School_Id });
            modelBuilder.Entity<tblSchoolDetail>().HasKey(ba => new { ba.ID});
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string[] tenants = new string[] { "TenantA" };
                string connectionString = "Server=DESKTOP-OS2L82E\\SQLEXPRESS2019;Database={tenant};User Id=sa; Password=admin@123;MultipleActiveResultSets=true";
                optionsBuilder.UseSqlServer(connectionString.Replace("{tenant}", "TenantA"),x => x.UseNetTopologySuite());

                //foreach (string tenant in tenants)
                //{
                //    optionsBuilder.UseSqlServer(connectionString.Replace("{tenant}", "TenantA"));
                //}
            }
        }
    }
}
