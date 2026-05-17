using AutoMapper;
using Company.Data.Models;
using Company.Data.Repositories;
using Company.Domain.Models;
using Company.Domain.Services.Base;

namespace Company.Domain.Services.Order;

public class OrderProvider
    : DataProviderBase<OrderModel, OrderEntity, IOrderRepository>,
        IOrderProvider
{
    public OrderProvider(
        IMapper mapper,
        IOrderRepository repository)
        : base(mapper, repository)
    {
    }
}
