using System.Linq.Expressions;
using Company.Data.Models.Base;

namespace Company.Data.Repositories.Base;

public interface IRepository<T>
    where T : ModelBase
{
    T Get(
        int id);

    IEnumerable<T> GetAll();

    IEnumerable<T> Find(
        Expression<Func<T, bool>> predicate);

    void Create(
        T entity);

    void Update(
        T entity);

    void Delete(
        T entity);
}
