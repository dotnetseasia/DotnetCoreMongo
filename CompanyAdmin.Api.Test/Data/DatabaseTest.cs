using Autofac;
using CompanyAdmin.API.Data.Interfaces;
using CompanyAdmin.API.Services.Interfaces;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CompanyAdmin.Api.Test.Data
{
    public class DatabaseTest : TestBase
    {
        public IUserDetailsService UserDetailsService { get; set; } = null!;
        public IMasterService MasterService { get; set; } = null!;

        public ISprintService SprintService { get; set; } = null!;
        public IRoleManagementService RoleManagementService { get; set; } = null!;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [SetUp]
        public async Task DataBaseTestSetUpAsync()
        {
            if (TestOptions != null && TestOptions.DatabaseType == DatabaseType.SqlServer)
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var context = scope.Resolve<ICompanyAdminContext>();
                }
            }

            GetAllFreshServices();
        }

        /// <summary>
        /// </summary>
        /// <param name="builder"></param>
        protected override void Build(ContainerBuilder Builder)
        {
            
        }

        

        /// <summary>
        /// </summary>
        [TearDown]
        public void DatabaseTeardown()
        {
            if (TestOptions == null || TestOptions.DatabaseType == DatabaseType.InMemory)
            {
               
                //Need to check code for clear context
                
            }
        }

        protected void GetAllFreshServices()
        {
            Scope.Dispose();
            Scope = Container.BeginLifetimeScope();
            UserDetailsService = Scope.Resolve<IUserDetailsService>();
            MasterService = Scope.Resolve<IMasterService>();
            RoleManagementService = Scope.Resolve<IRoleManagementService>();
            SprintService = Scope.Resolve<ISprintService>();
        }
    }
}
