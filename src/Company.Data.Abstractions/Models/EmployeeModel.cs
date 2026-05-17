using Company.Data.Models.Base;

namespace Company.Data.Models;

public class EmployeeModel : ModelBase
{
    public virtual string FullName { get; set; } = string.Empty;

    public virtual Position Position { get; set; }

    public virtual DateTime DateOfBirth { get; set; }
}
