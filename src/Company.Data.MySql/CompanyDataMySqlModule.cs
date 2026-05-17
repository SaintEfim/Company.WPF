using Autofac;
using Company.Data.Repositories.Base;

namespace Company.Data.MySql;

public class CompanyDataMySqlModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IRepository<>))
            .AsImplementedInterfaces();

        builder.RegisterType<NhibernateHelper>()
            .AsSelf()
            .SingleInstance();

        builder.Register(c => c.Resolve<NhibernateHelper>()
                .GetSessionFactory()
                .OpenSession())
            .InstancePerLifetimeScope();
    }
}
