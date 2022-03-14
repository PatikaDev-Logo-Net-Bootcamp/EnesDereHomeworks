using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using WebApi.Middlewares;
using WebApi.Models;

namespace WebApi.Extensions
{
    public static class VersionCheckMiddlewareExtension
    {
        public static IApplicationBuilder UseVersionCheck(this IApplicationBuilder applicationBuilder, IOptions<AppVersion> appVersion)
        {
            //var productVersion = FileVersionInfo.GetVersionInfo(typeof(VersionCheckMiddlewareExtension).Assembly.Location).ProductVersion;
            return applicationBuilder.UseMiddleware<VersionCheckMiddleware>(appVersion);
        }
    }
}
