using System.Linq.Expressions;
using Company.Data.Models.Base;
using Company.Data.Repositories.Base;
using NHibernate;
using NHibernate.Linq;

namespace Company.Data.MySql.Repositories.Base;

public abstract class RepositoryBase<T> : IRepository<T>
    where T : EntityBase
{
    private readonly ISession _session;

    protected RepositoryBase(
        ISession session)
    {
        _session = session;
    }

    public async Task<T> GetOneById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _session.GetAsync<T>(id, cancellationToken);

        if (entity == null)
        {
            throw new Exception($"Entity with id {id} was not found");
        }

        return entity;
    }

    public async Task<IEnumerable<T>> Get(
        CancellationToken cancellationToken = default)
    {
        return await _session.Query<T>()
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<T>> Find(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _session.Query<T>()
            .Where(predicate)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<T> Create(
        T entity,
        CancellationToken cancellationToken = default)
    {
        using var transaction = _session.BeginTransaction();
        try
        {
            var createdItem = (T) await _session.SaveAsync(entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return createdItem;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<T> Update(
        T entity,
        CancellationToken cancellationToken = default)
    {
        using var transaction = _session.BeginTransaction();
        try
        {
            await _session.UpdateAsync(entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return await _session.GetAsync<T>(entity.Id, cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<T> Delete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        using var transaction = _session.BeginTransaction();
        try
        {
            var entity = await _session.GetAsync<T>(id, cancellationToken);

            await _session.DeleteAsync(entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return entity;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
