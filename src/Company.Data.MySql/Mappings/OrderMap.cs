using Company.Data.Models;
using Company.Data.MySql.Mappings.Base;

namespace Company.Data.MySql.Mappings;

public class OrderMap : ClassMapBase<OrderEntity>
{
    public OrderMap()
    {
        Map(x => x.Date)
            .Not
            .Nullable();

        Map(x => x.Amount)
            .Not
            .Nullable();

        References(x => x.EmployeeModel)
            .Column("EmployeeId")
            .Not
            .Nullable();

        References(x => x.ContractorModel)
            .Column("ContractorId")
            .Not
            .Nullable();
    }
}
