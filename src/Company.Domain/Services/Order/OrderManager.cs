using AutoMapper;
using Company.Data.Models;
using Company.Data.Repositories;
using Company.Domain.Models;
using Company.Domain.Services.Base;
using Company.Domain.Services.Employee;

namespace Company.Domain.Services.Order;

public class OrderManager
    : DataManagerBase<EmployeeModel, EmployeeEntity, IEmployeeRepository>,
        IEmployeeManager
{
    public OrderManager(
        IMapper mapper,
        IEmployeeRepository repository)
        : base(mapper, repository)
    {
    }
}
