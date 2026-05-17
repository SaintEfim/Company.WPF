using Company.Domain.Models.Base;

namespace Company.Domain.Models;

public class OrderModel : ModelBase
{
    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public EmployeeModel? EmployeeModel { get; set; } = null!;

    public ContractorModel? ContractorModel { get; set; } = null!;
}
