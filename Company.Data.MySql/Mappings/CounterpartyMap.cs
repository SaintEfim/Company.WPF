using Company.Data.Models;
using Company.Data.MySql.Mappings.Base;

namespace Company.Data.MySql.Mappings;

public class ContractorMap : ClassMapBase<ContractorModel>
{
    public ContractorMap()
    {
        Map(x => x.Name)
            .Not
            .Nullable()
            .Length(300);

        Map(x => x.INN)
            .Not
            .Nullable()
            .Length(12);

        References(x => x.Curator)
            .Column("CuratorId")
            .Not
            .Nullable();
    }
}
