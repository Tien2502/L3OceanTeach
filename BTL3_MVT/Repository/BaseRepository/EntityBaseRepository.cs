using BTL3_MVT.Data;
using BTL3_MVT.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BTL3_MVT.Repository.BaseRepository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class
    {
        private readonly PersonContext _context;
        private readonly DbSet<T> _dbSet;

        public EntityBaseRepository(PersonContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            query = includeProperties.Aggregate(query, (current, includeProperties) => current.Include(includeProperties));
            return query.Where(predicate).ToList();
        }

        public T GetById(int id) => _dbSet.Find(id);

        public void RemoveRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
