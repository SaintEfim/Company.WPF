using Company.Domain.Models.Base;

namespace Company.Domain.Models;

public class OrderModel : ModelBase
{
    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public EmployeeModel Employee { get; set; } = null!;

    public ContractorModel Contractor { get; set; } = null!;
}
