using Company.Data.Models.Base;

namespace Company.Data.Models;

public class OrderEntity : EntityBase
{
    public virtual DateTime Date { get; set; }

    public virtual decimal Amount { get; set; }

    public virtual EmployeeEntity Employee { get; set; } = null!;

    public virtual ContractorEntity Contractor { get; set; } = null!;
}
