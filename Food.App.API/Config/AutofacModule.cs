using Autofac;
using Food.App.Core.Interfaces;
using Food.App.Repository;

namespace Food.App.API.Config;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }
}