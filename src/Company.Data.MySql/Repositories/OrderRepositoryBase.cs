using Company.Data.Models;
using Company.Data.MySql.Repositories.Base;
using Company.Data.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Company.Data.MySql.Repositories;

public class OrderRepositoryBase
    : RepositoryBase<OrderEntity>,
        IOrderRepository
{
    public OrderRepositoryBase(
        ISession session)
        : base(session)
    {
    }

    protected override IQueryable<OrderEntity>? FillRelatedRecords(
        IQueryable<OrderEntity>? query)
    {
        return query.Fetch(x => x.Contractor)
            .Fetch(x => x.Employee);
    }
}
