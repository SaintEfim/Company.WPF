using Company.Data.Models;
using Company.Data.MySql.Repositories.Base;
using Company.Data.Repositories;
using NHibernate;

namespace Company.Data.MySql.Repositories;

public class EmployeeRepository
    : RepositoryBase<EmployeeModel>,
        IEmployeeRepository
{
    public EmployeeRepository(
        ISession session)
        : base(session)
    {
    }
}
