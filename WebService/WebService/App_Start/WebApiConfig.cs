using Newtonsoft.Json;
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Konfiguracja i usługi składnika Web API
            config.Formatters.Add(new BrowserJsonFormatter());

            // Trasy składnika Web API
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public class BrowserJsonFormatter : JsonMediaTypeFormatter
        {
            public BrowserJsonFormatter()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                this.SerializerSettings.Formatting = Formatting.Indented;
            }

            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }
    }
}
