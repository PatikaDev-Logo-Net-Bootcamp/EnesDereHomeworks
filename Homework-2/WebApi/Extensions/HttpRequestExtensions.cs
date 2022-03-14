using Microsoft.AspNetCore.Http;
using System.Linq;

namespace WebApi.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetValueFromHeader(this HttpRequest httpRequest, string key)
        {
            return httpRequest.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
        }
    }
}
