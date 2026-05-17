using System.Linq.Expressions;
using Company.Data.Models.Base;
using Company.Data.Repositories.Base;
using NHibernate;

namespace Company.Data.MySql.Repositories.Base;

public abstract class Repository<T> : IRepository<T>
    where T : ModelBase
{
    private readonly ISession _session;

    protected Repository(
        ISession session)
    {
        _session = session;
    }

    public T Get(
        int id) =>
        _session.Get<T>(id);

    public IEnumerable<T> GetAll() =>
        _session.Query<T>()
            .ToList();

    public IEnumerable<T> Find(
        Expression<Func<T, bool>> predicate) =>
        _session.Query<T>()
            .Where(predicate)
            .ToList();

    public void Create(
        T entity)
    {
        using var transaction = _session.BeginTransaction();
        _session.Save(entity);
        transaction.Commit();
    }

    public void Update(
        T entity)
    {
        using var transaction = _session.BeginTransaction();
        _session.Update(entity);
        transaction.Commit();
    }

    public void Delete(
        T entity)
    {
        using var transaction = _session.BeginTransaction();
        _session.Delete(entity);
        transaction.Commit();
    }
}
