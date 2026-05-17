using Company.Data.Models.Base;

namespace Company.Data.Models;

public class ContractorModel : ModelBase
{
    public virtual string Name { get; set; } = string.Empty;

    public virtual string INN { get; set; }  = string.Empty;

    public virtual EmployeeModel? Curator { get; set; } = null!;
}
