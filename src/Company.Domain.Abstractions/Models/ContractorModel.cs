using Company.Domain.Models.Base;

namespace Company.Domain.Models;

public abstract class ContractorModel : ModelBase
{
    public string Name { get; set; } = string.Empty;

    public string INN { get; set; }  = string.Empty;

    public EmployeeModel? Curator { get; set; } = null!;
}
