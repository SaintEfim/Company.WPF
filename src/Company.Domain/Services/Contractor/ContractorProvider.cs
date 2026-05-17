using AutoMapper;
using Company.Data.Models;
using Company.Data.Repositories;
using Company.Domain.Models;
using Company.Domain.Services.Base;

namespace Company.Domain.Services.Contractor;

public class ContractorProvider
    : DataProviderBase<ContractorModel, ContractorEntity, IContractorRepository>,
        IContractorProvider
{
    public ContractorProvider(
        IMapper mapper,
        IContractorRepository repository)
        : base(mapper, repository)
    {
    }
}
