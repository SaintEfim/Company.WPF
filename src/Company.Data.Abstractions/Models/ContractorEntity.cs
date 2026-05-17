using Company.Data.Models.Base;

namespace Company.Data.Models;

public class ContractorEntity : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;

    public virtual string INN { get; set; }  = string.Empty;

    public virtual EmployeeEntity Curator { get; set; } = null!;
}
