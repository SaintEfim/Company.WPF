using Autofac;

namespace Company.Data.MySql;

public class CompanyDataMySqlModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.Register(c => c.Resolve<NhibernateHelper>()
                .GetSessionFactory()
                .OpenSession())
            .InstancePerLifetimeScope();
    }
}
