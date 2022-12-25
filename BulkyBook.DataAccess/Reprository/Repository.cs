using BulkyBook.DataAccess.Reprository.IReprository;
using BulkyBook.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Reprository
{
    //includeProp-"Category,CoverType"
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataProduct _db;
        internal DbSet<T> dbSet;
        public Repository(DataProduct db) { 
            _db= db;
            _db.Products.Include(u => u.Category).Include(u=> u.CoverType);
            this.dbSet = _db.Set<T>();
        }
        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null,string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
				query = query.Where(filter);
			}
			if (includeProperties != null)
            {
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
            
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
			if (includeProperties != null)
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
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
