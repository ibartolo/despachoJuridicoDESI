using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.WebApi;

[assembly: OwinStartup(typeof(DespachoJuridicoDESIWebApi.App_Start.Startup))]
namespace DespachoJuridicoDESIWebApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            // Registrar rutas / configuraciones Web API
            WebApiConfig.Register(config);

            // ---- Configurar DI con Unity ----
            var container = new UnityContainer();

            // Registrar implementaciones concretas
            //container.RegisterType<IUserProxy, UserProxy>(new HierarchicalLifetimeManager());
            //container.RegisterType<IUserApp, UserApp>(new HierarchicalLifetimeManager());

            // Asignar resolver a Web API
            config.DependencyResolver = new UnityDependencyResolver(container);
            // ----------------------------------

            // Registrar Swagger (si ya lo tienes implementado)
            SwaggerConfig.Register(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }

    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            context.SetError("invalid_grant", "The user name or password is incorrect.");
            return;

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }
    }
}