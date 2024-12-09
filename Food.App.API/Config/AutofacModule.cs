using Autofac;
using Food.App.Core.Interfaces;
using Food.App.Repository;
using Food.App.Service;

namespace Food.App.API.Config;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(authenticationService).Assembly)
            .Where(a => a.Name.EndsWith("Service"))
            .AsImplementedInterfaces().InstancePerLifetimeScope();
    }
}