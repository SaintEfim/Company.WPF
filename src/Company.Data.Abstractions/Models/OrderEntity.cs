using Company.Data.Models.Base;

namespace Company.Data.Models;

public class OrderEntity : EntityBase
{
    public virtual DateTime Date { get; set; }

    public virtual decimal Amount { get; set; }

    public virtual EmployeeEntity? EmployeeModel { get; set; } = null!;

    public virtual ContractorEntity? ContractorModel { get; set; } = null!;
}
