using Company.Data.Models;
using Company.Data.MySql.Repositories.Base;
using Company.Data.Repositories;
using NHibernate;

namespace Company.Data.MySql.Repositories;

public class OrderRepository
    : RepositoryBase<OrderModel>,
        IOrderRepository
{
    public OrderRepository(
        ISession session)
        : base(session)
    {
    }
}
