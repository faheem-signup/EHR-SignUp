using Business;
using Business.Helpers;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IO;
using System.Text.Json.Serialization;

namespace WebAPI
{
    /// <summary>
    ///
    /// </summary>
    public partial class Startup : BusinessStartup
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostEnvironment"></param>
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
            : base(configuration, hostEnvironment)
        {
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public override void ConfigureServices(IServiceCollection services)
        {
            // Business katmanında olan dependency tanımlarının bir metot üzerinden buraya implemente edilmesi.

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddCors(options =>
            {
               // options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://www.signup-demo.com/", "http://www.signup-demo.com/api/", "https://www.signup-demo.com/", "https://www.signup-demo.com/api/", "https://connect.medlineplus.gov/", "http://ehr-testingdev.signupdemo.com/").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:3000", "http://192.168.20.80:180", "https://www.signup-demo.com/api", "https://www.signup-demo.com", "http://192.168.20.61:180", "http://192.168.18.17:180", "http://192.168.20.73:180", "http://192.168.20.57:180", "http://192.168.20.65:180", "http://192.168.20.60:180", "http://192.168.20.55:180", "https://connect.medlineplus.gov/", "http://192.168.20.79:180", "http://192.168.18.140:180", "http://192.168.20.79:180", "http://192.168.18.147:180", "http://192.168.18.13", "http://192.168.20.66", "http://192.168.20.66/vcareapi", "https://localhost:5001", "https://vcaredevapi.azurewebsites.net/api/", "http://ehr-testingdev.signupdemo.com", "http://192.168.20.79:180", "http://192.168.20.79:180", "http://192.168.18.147:180", "http://192.168.18.13", "http://192.168.18.13:180", "http://192.168.20.66", "http://192.168.20.66/vcareapi", "https://localhost:5001", "http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            //    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://192.168.20.79:180", "http://192.168.20.79:180", "http://192.168.18.147:180", "http://192.168.18.13", "http://192.168.20.66", "http://192.168.20.66/vcareapi", "https://localhost:5001", "http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            //    //options.AddPolicy
            //    //    "AllowOrigin",
            //    //    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml"));
            });

            services.AddTransient<FileLogger>();
            services.AddTransient<PostgreSqlLogger>();
            services.AddTransient<MsSqlLogger>();

            base.ConfigureServices(services);
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // VERY IMPORTANT. Since we removed the build from AddDependencyResolvers, let's set the Service provider manually.
            // By the way, we can construct with DI by taking type to avoid calling static methods in aspects.
            ServiceTool.ServiceProvider = app.ApplicationServices;


            var configurationManager = app.ApplicationServices.GetService<ConfigurationManager>();
            switch (configurationManager.Mode)
            {
                case ApplicationMode.Development:
                    app.UseDbFakeDataCreator();
                    break;

                case ApplicationMode.Profiling:
                case ApplicationMode.Staging:

                    break;
                case ApplicationMode.Production:
                    break;
            }



            app.UseDeveloperExceptionPage();

            app.ConfigureCustomExceptionMiddleware();

            app.UseDbOperationClaimCreator();
            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "DevArchitecture"); });
            app.UseCors("CorsPolicy");
   

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // Make Turkish your default language. It shouldn't change according to the server.
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr-TR"),
            });

            var cultureInfo = new CultureInfo("tr-TR");
            cultureInfo.DateTimeFormat.ShortTimePattern = "HH:mm";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseStaticFiles();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}