using Company.Data.Models;
using Company.Data.MySql.Repositories.Base;
using Company.Data.Repositories;
using NHibernate;

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
}
