using BulkyBook.DataAccess.Reprository.IReprository;
using BulkyBook.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Reprository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataProduct _db;
        internal DbSet<T> dbSet;
        public Repository(DataProduct db) { 
            _db= db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> item)
        {
           dbSet.RemoveRange(item);
        }
    }
}
