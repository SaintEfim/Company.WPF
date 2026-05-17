using Company.Data.Models;
using Company.Data.MySql.Repositories.Base;
using Company.Data.Repositories;
using NHibernate;

namespace Company.Data.MySql.Repositories;

public class EmployeeRepositoryBase
    : RepositoryBase<EmployeeEntity>,
        IEmployeeRepository
{
    public EmployeeRepositoryBase(
        ISession session)
        : base(session)
    {
    }
}
