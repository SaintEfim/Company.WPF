using Company.Data.MySql.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Company.Data.MySql;

public class NhibernateHelper
{
    private string ConnectionString { get; }

    private static ISessionFactory? _sessionFactory;

    public NhibernateHelper(
        IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("CompanyDb") ??
                           throw new InvalidOperationException("No connection string configured");
    }

    public ISessionFactory GetSessionFactory()
    {
        return _sessionFactory ??= Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(ConnectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EmployeeMap>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
    }
}
