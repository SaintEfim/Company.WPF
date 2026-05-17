using AutoMapper;
using Company.Data.Models;
using Company.Domain.Models;

namespace Company.Domain;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ContractorEntity, ContractorModel>()
            .ReverseMap();

        CreateMap<EmployeeEntity, EmployeeModel>()
            .ReverseMap();

        CreateMap<OrderEntity, OrderModel>()
            .ReverseMap();
    }
}
