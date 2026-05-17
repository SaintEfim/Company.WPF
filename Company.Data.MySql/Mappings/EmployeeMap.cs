using Company.Data.Models;
using Company.Data.MySql.Mappings.Base;

namespace Company.Data.MySql.Mappings;

public class EmployeeMap : ClassMapBase<EmployeeModel>
{
    public EmployeeMap()
    {
        Map(x => x.FullName)
            .Not
            .Nullable()
            .Length(200);

        Map(x => x.Position)
            .CustomType<int>()
            .Not
            .Nullable();

        Map(x => x.DateOfBirth)
            .Not
            .Nullable();
    }
}
