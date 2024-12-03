using Autofac;
using Food.App.Core.Interfaces;
using Food.App.Repository;

namespace Food.App.API.Config;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Repositories
        builder.RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>)).InstancePerLifetimeScope();

        // Services
        //builder.RegisterAssemblyTypes(typeof(CourseService).Assembly)
        //    .Where(c => c.Name.EndsWith("Service"))
        //    .AsImplementedInterfaces().InstancePerLifetimeScope();

        // Validators
        //builder.RegisterAssemblyTypes(typeof(CourseValidator).Assembly)
        //    .AsClosedTypesOf(typeof(IValidator<>)).InstancePerDependency();
        //builder.RegisterType<FakeDataService>().SingleInstance();
        //builder.RegisterType<AppDbContext>().InstancePerDependency(); // Transient
        //builder.RegisterGeneric(typeof(Repository<>)).AsImplementedInterfaces;
    }
}