using Agenda.Application;
using Agenda.Proxy;
using Case.Application;
using Case.Proxy;
using Court.Application;
using Court.Proxy;
using Customer.Application;
using Customer.Proxy;
using Dashboard.Application;
using Dashboard.Proxy;
using EventType.Application;
using EventType.Proxy;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Record.Application;
using Record.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using User.Application;
using User.Proxy;

[assembly: OwinStartup(typeof(DespachoJuridicoDESIWebApi.App_Start.Startup))]
namespace DespachoJuridicoDESIWebApi.App_Start
{
    public class Startup
    {
        // Exponer el contenedor para que el provider pueda resolver dependencias
        public static IUnityContainer Container { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            // Registrar rutas / configuraciones Web API
            WebApiConfig.Register(config);

            // ---- Configurar DI con Unity ----
            var container = new UnityContainer();
            Container = container;

            // Registrar implementaciones concretas
            container.RegisterType<IUserProxy, UserProxy>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserApp, UserApp>(new HierarchicalLifetimeManager());

            container.RegisterType<IClientProxy, ClientProxy>(new HierarchicalLifetimeManager());
            container.RegisterType<IClientApp, ClientApp>(new HierarchicalLifetimeManager());

            container.RegisterType<IStatusCaseProxy, StatusCaseProxy>(new HierarchicalLifetimeManager());
            container.RegisterType<ICaseApp, CaseApp>(new HierarchicalLifetimeManager());

            container.RegisterType<ICaseProxy, CaseProxy>(new HierarchicalLifetimeManager());

            container.RegisterType<IRecordProxy, RecordProxy>(new HierarchicalLifetimeManager());
            container.RegisterType<IRecordApp, RecordApp>(new HierarchicalLifetimeManager());

            container.RegisterType<ICourtProxy, CourtProxy>(new HierarchicalLifetimeManager());
            container.RegisterType<ICourtApp, CourtApp>(new HierarchicalLifetimeManager()); // Si existe

            container.RegisterType<IEventTypeProxy, EventTypeProxy>(new HierarchicalLifetimeManager());
            container.RegisterType<IEventTypeApp, EventTypeApp>(new HierarchicalLifetimeManager());

            container.RegisterType<IAgendaProxy, AgendaProxy>(new HierarchicalLifetimeManager());
            container.RegisterType<IAgendaApp, AgendaApp>(new HierarchicalLifetimeManager());

            container.RegisterType<IDashboardProxy, DashboardProxy>(new HierarchicalLifetimeManager());
            container.RegisterType<IDashboardApp, DashboardApp>(new HierarchicalLifetimeManager());

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
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(6),
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

            // Intentar resolver IUserApp desde el contenedor público; si no está, usar el DependencyResolver global
            IUserApp userApp = null;
            try
            {
                if (Startup.Container != null)
                {
                    userApp = Startup.Container.Resolve<IUserApp>();
                }
                else
                {
                    userApp = System.Web.Http.GlobalConfiguration.Configuration
                        .DependencyResolver.GetService(typeof(IUserApp)) as IUserApp;
                }
            }
            catch
            {
                userApp = null;
            }

            if (userApp == null)
            {
                context.SetError("invalid_grant", "Error interno: no se pudo validar al usuario.");
                return;
            }

            // Llamar a AutenticacionParaToken con usuario y contraseña
            var user = userApp.AutenticacionParaToken(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }
    }
}