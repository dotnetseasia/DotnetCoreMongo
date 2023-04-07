using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using CompanyAdmin.Api.Test.DI;
using CompanyAdmin.API.Data;
using CompanyAdmin.API.Data.Interfaces;
using CompanyAdmin.API.Repositories;
using CompanyAdmin.API.Repositories.Interfaces;
using CompanyAdmin.API.Services;
using CompanyAdmin.API.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAdmin.Api.Test.Data
{
    public abstract class TestBase
    {
        /// <summary>
        /// </summary>
        private static ServiceCollection? _serviceCollection;

        /// <summary>
        /// </summary>
        private protected static IConfigurationRoot _configuration = null!;

        /// <summary>
        /// </summary>
        protected IContainer Container { get; private set; } = null!;

        /// <summary>
        /// Lifetime scope for tests
        /// </summary>
        protected ILifetimeScope Scope { get; set; } = null!;

        /// <summary>
        /// Mapper
        /// </summary>
        protected IMapper Mapper { get; set; } = null!;

        /// <summary>
        /// </summary>
        public static TestOptions? TestOptions { get; private set; }

        [SetUp]
        public void TestBaseInitialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<CompanyAdminModule>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            var env = Substitute.For<IWebHostEnvironment>();
            builder.RegisterInstance(env).As<IWebHostEnvironment>();

            // If you assign to any field, such as `TestOptions` and `Configuration, in this block, make sure those fields are static. Or else. 
            if (_serviceCollection == null)
            {
                _serviceCollection = new ServiceCollection();
                _configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.test.json", true)
                   
                    .Build();

                


                TestOptions = _configuration.GetSection("TestOptions").Get<TestOptions>();

                _serviceCollection.AddAutoMapper(GetCompanyApiAssemblies());
                _serviceCollection.AddLogging(loggingBuilder => { loggingBuilder.AddSerilog(); });
                _serviceCollection.AddHttpClient();

                // This next line does some magic
                _serviceCollection
                    .Configure<TestOptions>(_configuration.GetSection("TestOptions"))
                    // Adds a bunch of MVC related stuff to the service collection
                    // `AddMvc` must be called in order to have access to call `AddApplicationPart` and `AddControllersAsServices`. 
                    .AddMvc()
                    // Tell MVC that it can find controllers and such in the same assembly as the `SettingsController`. 
                    // Since we're calling `AddMvc` from a unit test, MVC won't look in the API assembly by default.
                    // https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/app-parts?view=aspnetcore-3.0
                    // Controllers are typically not actually loaded from the DI framework. MS supposedly has their
                    // own mechanism for firing up controllers. This next line makes it so we can resolve
                    // controllers in tests with the DI container. 
                    // https://andrewlock.net/controller-activation-and-dependency-injection-in-asp-net-core-mvc/
                    .AddControllersAsServices();

                //ServiceCollection.AddRazorPages();
                _serviceCollection.AddSignalR();
                //ServiceCollection.AddHttpClient();
                _serviceCollection.AddHealthChecks()
                    .AddMongoDb(_configuration["DatabaseSettings:ConnectionString"], "MongoDb Health", HealthStatus.Degraded);

                _serviceCollection.AddTransient<IConfiguration>(sp =>
                {
                    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                    configurationBuilder.AddJsonFile("appsettings.test.json");
                    return configurationBuilder.Build();
                });

            }
            builder.RegisterType<UserDetailsService>().As<IUserDetailsService>().InstancePerLifetimeScope();
            builder.RegisterType<UserDetailsRepository>().As<IUserDetailsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CompanyAdminContext>().As<ICompanyAdminContext>().InstancePerLifetimeScope();
            builder.RegisterType<RoleManagementService>().As<IRoleManagementService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleManagementRepository>().As<IRoleManagementRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserDetailsService>().As<IUserDetailsService>().InstancePerLifetimeScope();
            builder.RegisterType<UserDetailsRepository>().As<IUserDetailsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MasterService>().As<IMasterService>().InstancePerLifetimeScope();
            builder.RegisterType<MasterRepository>().As<IMasterRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SprintService>().As<ISprintService>().InstancePerLifetimeScope();
            builder.RegisterType<SprintRepository>().As<ISprintRepository>().InstancePerLifetimeScope();

            builder.RegisterType<CompanyAdminContext>().As<ICompanyAdminContext>().InstancePerLifetimeScope();
            builder.Populate(_serviceCollection);

            Build(builder);

            Container = builder.Build();
            Assert.That(Container, Is.Not.Null);

            Scope = Container.BeginLifetimeScope();

            Mapper = Scope.Resolve<IMapper>();
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Assembly> GetCompanyApiAssemblies()
        {
            return AppDomain.CurrentDomain
                            .GetAssemblies()
                            .Where(p => p.FullName!.ToUpperInvariant().StartsWith("COMPANYADMIN.", StringComparison.InvariantCulture))
                            .ToList();
        }

        /// <summary>
        /// Hook to allow additional builder registrations specific for a test class.
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void Build(ContainerBuilder builder) { }

        [TearDown]
        public void TestBaseTeardown()
        {
            Container?.Dispose();
            Scope?.Dispose();
        }
    }
}
