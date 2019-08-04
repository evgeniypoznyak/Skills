using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Skills.API.Middleware
{
    public static class NotFoundMiddleware
    {
        public static async Task Process(HttpContext context)
        {
            var statusCode = (int) HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new CustomErrorResponse
                {
                    Message = "The endpoint you are trying to reach is not found. Please try different route",
                    HttpStatusCode = statusCode
                }
            ));
        }
    }
}