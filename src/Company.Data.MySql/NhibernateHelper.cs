using Company.Data.MySql.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Company.Data.MySql;

public class NhibernateHelper
{
    private static ISessionFactory? _sessionFactory;

    public ISessionFactory GetSessionFactory()
    {
        return _sessionFactory ??= Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(cs => cs.Server("localhost")
                .Database("companydb")
                .Username("root")
                .Password("123456")))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EmployeeMap>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
    }
}
