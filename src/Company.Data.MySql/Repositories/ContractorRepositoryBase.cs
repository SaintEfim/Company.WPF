using Company.Data.Models;
using Company.Data.MySql.Repositories.Base;
using Company.Data.Repositories;
using NHibernate;

namespace Company.Data.MySql.Repositories;

public class ContractorRepositoryBase
    : RepositoryBase<ContractorEntity>,
        IContractorRepository
{
    public ContractorRepositoryBase(
        ISession session)
        : base(session)
    {
    }
}
