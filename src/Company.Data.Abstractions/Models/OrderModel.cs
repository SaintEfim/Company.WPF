using Company.Data.Models.Base;

namespace Company.Data.Models;

public class OrderModel : ModelBase
{
    public virtual DateTime Date { get; set; }

    public virtual decimal Amount { get; set; }

    public virtual EmployeeModel? EmployeeModel { get; set; } = null!;

    public virtual ContractorModel? ContractorModel { get; set; } = null!;
}
