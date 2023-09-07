using System.Linq.Expressions;

namespace BTL3_MVT.Repository.BaseRepository
{
    public interface IEntityBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void RemoveRange(IEnumerable<T> entity);
    }
}
