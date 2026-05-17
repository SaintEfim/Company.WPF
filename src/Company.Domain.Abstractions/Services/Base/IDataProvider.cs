using Company.Domain.Models.Base;

namespace Company.Domain.Services.Base;

public interface IDataProvider<TModel>
    where TModel : class, IModel
{
    Task<IEnumerable<TModel>> Get(
        CancellationToken cancellationToken = default);

    Task<TModel> GetOneById(
        Guid id,
        CancellationToken cancellationToken = default);
}
