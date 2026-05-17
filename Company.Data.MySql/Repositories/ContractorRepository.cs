using Company.Data.Models;
using Company.Data.MySql.Repositories.Base;
using Company.Data.Repositories;
using NHibernate;

namespace Company.Data.MySql.Repositories;

public class ContractorRepository
    : Repository<ContractorModel>,
        IContractorRepository
{
    public ContractorRepository(
        ISession session)
        : base(session)
    {
    }
}
