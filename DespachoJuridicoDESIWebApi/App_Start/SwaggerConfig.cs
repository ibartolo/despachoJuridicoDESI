using DespachoJuridicoDESIWebApi;
using Swashbuckle.Application;
using System;
using System.IO;
using System.Web.Http;
using WebActivatorEx;

namespace DespachoJuridicoDESIWebApi.App_Start
{
    public static class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "DespachoJuridicoDESIWebApi Web API");
                //c.Description("API para reportes de incidencias");
                var xmlPath = XmlCommentsFilePath();
                if (!string.IsNullOrEmpty(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
                c.IgnoreObsoleteActions();
            })
            .EnableSwaggerUi(c =>
            {
                // Agrega un campo para Authorization header (Bearer token)
                c.EnableApiKeySupport("Authorization", "header");
            });
        }

        private static string XmlCommentsFilePath()
        {
            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = "DespachoJuridicoDESIWebApi.xml";
                var full = Path.Combine(basePath, "bin", fileName);
                return File.Exists(full) ? full : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
