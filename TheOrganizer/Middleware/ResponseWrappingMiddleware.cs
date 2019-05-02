using Microsoft.AspNetCore.Http;
using System.Text;
using System.Reflection;
using System.Net;
using System.Threading.Tasks;
using TheOrganizer.Entities;
using System;
using System.IO;

namespace TheOrganizer.Middleware
{
    public class ResponseWrappingMiddleware
    {
        private readonly RequestDelegate next;
        public ResponseWrappingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var memStream = new MemoryStream())
            {
                Stream originalBody = context.Response.Body;

                try
                {
                    context.Response.Body = memStream;
                    await next(context);
                }
                catch
                {
                    context.Response.StatusCode = 500;
                }
                finally
                {
                    memStream.Position = 0;
                    if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 600)
                        await WrapResponse(context);

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);

                    // this is not actually needed
                    context.Response.Body = originalBody;
                }
            }
        }

        private async Task WrapResponse(HttpContext context)
        {
            ResponseData response = new ResponseData()
            {
                StatusCode = (HttpStatusCode)context.Response.StatusCode
            };

            string bodyContent = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.SetLength(0);

            try
            {
                object bodyObject = Newtonsoft.Json.JsonConvert.DeserializeObject(bodyContent);

                switch (context.Response.StatusCode)
                {
                    case 500:
                        response.ErrorMessage = "Something went wrong";
                        break;
                    case 401:
                        response.ErrorMessage = "Unauthorized";
                        break;
                    case 405:
                        response.ErrorMessage = "Wrong HTTP method";
                        break;
                    default:
                        response.ErrorMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorRequest>(bodyContent).Title;
                        break;
                }
            }
            catch
            {
                if (context.Response.StatusCode == 404)
                    response.ErrorMessage = "Wrong url";
                else
                    response.ErrorMessage = bodyContent;
            }

            string jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            context.Response.Body.Position = 0;
            //await new StreamWriter(context.Response.Body).WriteAsync(jsonResponse);
            var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
            await context.Response.Body.WriteAsync(responseBytes);
        }
    }
}
