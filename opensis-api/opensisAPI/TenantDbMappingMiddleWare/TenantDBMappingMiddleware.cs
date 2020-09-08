using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using opensis.data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace opensisAPI.TenantDbMappingMiddleWare
{
    public class TenantDBMappingMiddleware
    {
        private readonly RequestDelegate next;

        public TenantDBMappingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string[] urlParts = null;
            
            urlParts = httpContext.Request.Path.Value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

          //  urlParts = httpContext.Request.Host.Host.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);


            if (urlParts != null && urlParts.Any())
            {
                httpContext.RequestServices.GetService<IDbContextFactory>().TenantName = urlParts[0];
            }

            await this.next(httpContext);
        }
    }

    public static class TenantDBMappingMiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantDBMapper(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<TenantDBMappingMiddleware>();
        }
    }
}
