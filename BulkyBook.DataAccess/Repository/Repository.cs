using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    //HERE MY GENERIC REPOSITORY
    //Of course, as the requirement changes, we will be making modifications here
    //and when we make modification in the repository, all the classes that implements
    //this repository will get the update as well so that.

    //So that way, we don't have to go to all the places to update the change
    public class Repository<T> : IRepository<T> where T : class
    {
        //From our DbContext, we can get DbSet instance and work on that directly
        internal DbSet<T> dbSet;
        private readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_db.ShoppingCarts.AsNoTracking()
            //Include is used to load the navigation properties that are used inside the product model

            //_db.ShoppingCarts.Include(x => x.Product).Include(x => x.CoverType);
            dbSet = _db.Set<T>(); //That is calling our repository
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        //includeProp - "Category,CoverType"
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            //We will return an inumerable of T like we might want to query our data before we return
            IQueryable<T> query = dbSet;
            if(filter != null)
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

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked=true)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
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

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
