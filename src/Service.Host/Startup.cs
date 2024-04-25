namespace Linn.Stores.Service.Host
{
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;

    using Linn.Common.Authentication.Host.Extensions;
    using Linn.Common.Logging;
    using Linn.Common.Service.Core;
    using Linn.Common.Service.Core.Extensions;
    using Linn.Sales.IoC;
    using Linn.Stores.IoC;
    using Linn.Stores.Service.Host.Negotiators;
    using Linn.Stores.Service.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddCors();
            services.AddSingleton<IViewLoader, ViewLoader>();
            services.AddSingleton<IResponseNegotiator, HtmlNegotiator>();
            services.AddSingleton<IResponseNegotiator, UniversalResponseNegotiator>();

            services.AddCredentialsExtensions();
            services.AddSqsExtensions();
            services.AddLog();

            services.AddFacade();
            services.AddServices();
            services.AddPersistence();
            services.AddHandlers();
            services.AddRabbitConfiguration();
            services.AddMessageDispatchers();

            services.AddLinnAuthentication(
                options =>
                {
                    options.Authority = ApplicationSettings.Get().AuthorityUri;
                    options.CallbackPath = new PathString("/inventory/signin-oidc");
                    options.CookiePath = "/sales";
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles(new StaticFileOptions
                {
                    RequestPath = "/inventory/build",
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "client", "build"))
                });
            }
            else
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    RequestPath = "/inventory/build",
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "app", "client", "build"))
                });
            }

            app.UseAuthentication();

            app.UseBearerTokenAuthentication();
            app.UseExceptionHandler(
                c => c.Run(async context =>
                {
                    var exception = context.Features
                        .Get<IExceptionHandlerPathFeature>()
                        ?.Error;

                    var log = app.ApplicationServices.GetService<ILog>();
                    log.Error(exception?.Message, exception);

                    var response = new { error = $"{exception?.Message}  -  {exception?.StackTrace}" };
                    await context.Response.WriteAsJsonAsync(response);
                }));
            app.UseRouting();
            app.UseEndpoints(builder => { builder.MapEndpoints(); });
        }
    }
}
