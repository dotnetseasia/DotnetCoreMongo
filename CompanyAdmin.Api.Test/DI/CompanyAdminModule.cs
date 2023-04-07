using Autofac;
using CompanyAdmin.API.Common.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAdmin.Api.Test.DI
{
    public class CompanyAdminModule : Module
    {
        /// <inheritdoc />
        /// <summary>
        /// Registers types with the container.
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            var companyAdminssemblies = AppDomain.CurrentDomain
                                           .GetAssemblies()
                                           .Where(p => p.FullName!.ToUpperInvariant().StartsWith("COMPANYADMIN.", StringComparison.InvariantCulture)).ToList();

            var types = companyAdminssemblies.SelectMany(p => p.GetTypes())
                .Where(p => !p.IsInterface && !p.IsAbstract && typeof(IInjectable).IsAssignableFrom(p)).ToList();

            foreach (var type in types)
            {
                var registered = builder.RegisterType(type);
                if (typeof(IAsImplementedInterfaces).IsAssignableFrom(type)) registered.AsImplementedInterfaces();
                if (typeof(IAsSelf).IsAssignableFrom(type)) registered.AsSelf();
            }
        }
    }
}
