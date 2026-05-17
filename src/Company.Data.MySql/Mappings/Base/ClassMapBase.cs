using Company.Data.Models.Base;
using FluentNHibernate.Mapping;

namespace Company.Data.MySql.Mappings.Base;

public abstract class ClassMapBase<T> : ClassMap<T>
    where T : EntityBase
{
    private static string GetTableName()
    {
        const string endsWitchConst = "Entity";

        var name = typeof(T).Name;
        return name.EndsWith(endsWitchConst) ? name[..^endsWitchConst.Length] + "s" : name + "s";
    }

    protected ClassMapBase()
    {
        Table(GetTableName());

        Id(x => x.Id)
            .GeneratedBy
            .GuidComb();

        Not.LazyLoad();
    }
}
