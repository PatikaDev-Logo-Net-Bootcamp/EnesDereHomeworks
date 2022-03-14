using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Models;

namespace WebApi.Middlewares
{
    public class VersionCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<AppVersion> _appVersion;
        
        public VersionCheckMiddleware(RequestDelegate next, IOptions<AppVersion> appVersion)
        {
            _next = next;
            _appVersion = appVersion;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            //get versions from request and appsetting.json
            string VersionInRequest  = httpContext.Request.GetValueFromHeader("app-version");
            string VersionInAppsettings = _appVersion.Value.Version;
            
            string path = httpContext.Request.Path.ToString().ToLower();

            //string actionName = httpContext.Request.RouteValues["action"].ToString();
            if(!CheckRequestPath(path)) // if action is register or login then move on
            {
                if (IsRequestVersionBiggerThanAppVersion(VersionInRequest, VersionInAppsettings))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
                    
            }
            await _next.Invoke(httpContext);
        }
        public bool IsRequestVersionBiggerThanAppVersion(string VersionInRequest, string VersionInAppsettings)
        {
            try
            {
                if (Convert.ToDouble(VersionInRequest) > Convert.ToDouble(VersionInAppsettings))
                    return true;
            }
            catch (Exception)
            {
                return true;
            }
            return false;
            
        }
        public bool CheckRequestPath(string path)
        {
            string[] validActions = new string[] {"register", "login" };
            foreach (string action in validActions)
            {
                if (path.Contains(action.ToLower()))
                    return true;
            }
            return false;
        }
    }
}
