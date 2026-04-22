namespace Linn.Stores.Service.Host
{
    using System.IdentityModel.Tokens.Jwt;

    using Linn.Common.Authentication.Host.Extensions;
    using Linn.Common.Configuration;
    using Linn.Stores.Service.Models;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Nancy;
    using Nancy.Owin;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddConsole();
                    builder.AddFilter("Microsoft", LogLevel.Warning);
                    builder.AddFilter("System", LogLevel.Warning);
                    builder.AddFilter("Linn", LogLevel.Information);
                });

            // 1) legacy (Linn OpenID Connect + Linn JWT)
            services.AddLinnAuthentication(
            options =>
            {
                options.Authority = ConfigurationManager.Configuration["LEGACY_AUTHORITY_URI"];

                options.CallbackPath = new PathString("/products/maint/signin-oidc");
            });

            // 2) new cognito JWT provider
            var appSettings = ApplicationSettings.Get();
            var cognitoIssuer = appSettings.CognitoHost;
            var cognitoClientId = appSettings.CognitoClientId;

            services.AddAuthentication().AddJwtBearer(
                "cognito-provider",
                options =>
                {

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = cognitoIssuer,
                        ValidateAudience = false,
                        ValidAudience = cognitoClientId,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };

                    options.MetadataAddress = $"{cognitoIssuer}/.well-known/openid-configuration";
                });

            services.AddAuthentication(
                options =>
                {
                    options.DefaultScheme = "MultiAuth";
                    options.DefaultChallengeScheme = "MultiAuth";
                }).AddScheme<MultiAuthOptions, MultiAuthHandler>(
                "MultiAuth",
                opts =>
                {
                    opts.CognitoIssuer = appSettings.CognitoHost;
                    opts.CognitoScheme = "cognito-provider";
                    opts.LegacyScheme = JwtBearerDefaults.AuthenticationScheme;
                });

                services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
                                        {
                                            ForwardedHeaders = ForwardedHeaders.XForwardedProto
                                        });
            app.UseAuthentication();

            app.UseBearerTokenAuthentication();

            app.UseOwin(x => x.UseNancy(
                config =>
                    {
                        config.PassThroughWhenStatusCodesAre(HttpStatusCode.Unauthorized, HttpStatusCode.Forbidden);
                    }));
            app.Use((context, next) => context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme));
        }
    }
}
