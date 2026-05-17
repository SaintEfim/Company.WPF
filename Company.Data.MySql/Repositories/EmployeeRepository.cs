using Company.Data.Models;
using Company.Data.MySql.Repositories.Base;
using Company.Data.Repositories;
using NHibernate;

namespace Company.Data.MySql.Repositories;

public class EmployeeRepository
    : Repository<EmployeeModel>,
        IEmployeeRepository
{
    public EmployeeRepository(
        ISession session)
        : base(session)
    {
    }
}
