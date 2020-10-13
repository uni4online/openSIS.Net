using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using opensis.core.School.Interfaces;
using opensis.core.School.Services;
using opensis.core.User.Services;
using opensis.core.User.Interfaces;
using opensis.data.Factory;
using opensis.data.Interface;
using opensis.data.Models;
using opensis.data.Repository;
using opensisAPI.TenantDbMappingMiddleWare;
using Swashbuckle.AspNetCore.Swagger;
using opensis.core.Common.Interfaces;
using opensis.core.Common.Services;
using opensis.core.Gradelevel.Interfaces;
using opensis.core.Gradelevel.Services;
using opensis.core.Room.Interfaces;
using opensis.core.Room.Services;
using opensis.core.Section.Interfaces;
using opensis.core.Section.Services;
using opensis.core.MarkingPeriods.Interfaces;
using opensis.core.MarkingPeriods.Services;
using opensis.core.SchoolPeriod.Interfaces;
using opensis.core.SchoolPeriod.Services;
using opensis.core.Calender.Services;
using opensis.core.Calender.Interfaces;
using opensis.core.CalendarEvents.Interfaces;
using opensis.core.CalendarEvents.Services;

namespace opensisAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<INoticeService, NoticeService>();
            services.AddScoped<INoticeRepository, NoticeRepository>();
            services.AddScoped<ISchoolRegisterService, SchoolRegister>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IGradelevelService, GradelevelService>();
            services.AddScoped<IGradelevelRepository, GradelevelRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomRegisterService, RoomRegister>();
            services.AddScoped<ISectionRepositiory, SectionRepository>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IMarkingperiodRepository, MarkingperiodRepository>();
            services.AddScoped<IMarkingPeriodService, MarkingPeriodService>();
            services.AddScoped<ISchoolPeriodService, SchoolPeriodService>();
            services.AddScoped<ISchoolPeriodRepository, SchoolPeriodRepository>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();
            services.AddScoped<ICalendarEventService, CalendarEventService>();



            if (Configuration["dbtype"] == "sqlserver")
            {
                services.AddScoped<IDbContextFactory, DbContextFactory>(serviceProvider => new DbContextFactory(Configuration["ConnectionStringTemplate"]));
            }
            else if (Configuration["dbtype"] == "mysql")
            {
                services.AddScoped<IDbContextFactory, MySQLContextFactory>(serviceProvider => new MySQLContextFactory(Configuration["ConnectionStringTemplateMySQL"]));
            }
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", builder =>
                {
                    builder.WithOrigins("*")
                        .SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod();
                });
            });

            ////services.AddDbContext<catalogDBContext>(ServiceLifetime.Scoped);
            ////services.AddDbContext<opensisContext>(ServiceLifetime.Scoped);

            ////services.AddTransient<ITenantProvider, DatabaseTenantProvider>();
            ////services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenSIS2 API", Version = "V1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseTenantDBMapper();

            app.UseCors(options => options.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenSIS2WebAPI V1");
            });

            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

        }
    }
}
