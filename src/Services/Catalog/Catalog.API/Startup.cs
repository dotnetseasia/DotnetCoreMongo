using CompanyAdmin.API.Common;
using CompanyAdmin.API.Data;
using CompanyAdmin.API.Data.Interfaces;
using CompanyAdmin.API.Repositories;
using CompanyAdmin.API.Repositories.Interfaces;
using CompanyAdmin.API.Services;
using CompanyAdmin.API.Services.CommonMethods;
using CompanyAdmin.API.Services.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace CompanyAdmin.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds a default implementation for the IHttpContextAccessor service.
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<Seed>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICompanyAdminContext, CompanyAdminContext>();
            services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();
            services.AddScoped<IRoleManagementRepository, RoleManagementRepository>();
            services.AddScoped<IUserDetailsService, UserDetailsService>();
            services.AddScoped<IRoleManagementService, RoleManagementService>(); 
            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddScoped<IApplicationConfigurationService, ApplicationConfigurationService>();
            services.AddScoped<IApplicationConfigurationRepository, ApplicationConfigurationRepository>();
            services.AddScoped<IProjectDetailsService, ProjectDetailsService>();
            services.AddScoped<IProjectDetailsRepository, ProjectDetailsRepository>();
            services.AddScoped<ISprintService, SprintService>();
            services.AddScoped<ISprintRepository, SprintRepository>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CompanyAdmin.API", Version = "v1" });
            });

            services.AddHealthChecks()
                    .AddMongoDb(Configuration["DatabaseSettings:ConnectionString"], "MongoDb Health", HealthStatus.Degraded);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, ILoggerFactory loggerFactory)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "CompanyAdmin.API v1"));
            //}

            app.UseRouting();
            app.UseAuthorization();
            app.ConfigureExceptionHandler(logger);
            //seeder.HydrateApplicationConfigurations();
            //seeder.HydrateEmailTemplateIds();

            app.UseCors(options =>
            {
                options.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
            var serilogConfig = new LoggerConfiguration()
         .MinimumLevel.Verbose()
         .Enrich.FromLogContext()
          .WriteTo.File(@"CompanyAdminApi_log_" + DateTime.Now.ToString("MM-dd-yyyy") + ".txt");

            loggerFactory
            .AddSerilog(serilogConfig.CreateLogger());

        }
    }
}
