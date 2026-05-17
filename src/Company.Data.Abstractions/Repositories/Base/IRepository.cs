using System.Linq.Expressions;
using Company.Data.Models.Base;

namespace Company.Data.Repositories.Base;

public interface IRepository<T>
    where T : class, IEntity
{
    Task<T> GetOneById(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> Get(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> Find(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<T> Create(
        T entity,
        CancellationToken cancellationToken = default);

    Task<T> Update(
        T entity,
        CancellationToken cancellationToken = default);

    Task<T> Delete(
        Guid id,
        CancellationToken cancellationToken = default);
}
