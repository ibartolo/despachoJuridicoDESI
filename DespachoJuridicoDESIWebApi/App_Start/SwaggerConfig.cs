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
                c.IgnoreObsoleteActions();
            })
            .EnableSwaggerUi(c =>
            {
                // Agrega un campo para Authorization header (Bearer token)
                c.EnableApiKeySupport("Authorization", "header");
            });
        }
    }
}
