using AutoMapper;
using Company.Data.Models;
using Company.Data.Repositories;
using Company.Domain.Models;
using Company.Domain.Services.Base;

namespace Company.Domain.Services.Employee;

public class EmployeeManager
    : DataManagerBase<EmployeeModel, EmployeeEntity, IEmployeeRepository>,
        IEmployeeManager
{
    public EmployeeManager(
        IMapper mapper,
        IEmployeeRepository repository)
        : base(mapper, repository)
    {
    }
}
