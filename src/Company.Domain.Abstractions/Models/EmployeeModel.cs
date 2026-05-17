using Company.Domain.Models.Base;

namespace Company.Domain.Models;

public class EmployeeModel : ModelBase
{
    public string FullName { get; set; } = string.Empty;

    public Position Position { get; set; }

    public DateTime DateOfBirth { get; set; }
}
