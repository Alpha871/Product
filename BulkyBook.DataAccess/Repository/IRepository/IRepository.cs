using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    /*
     * This interface should be generic.
     * We have category,, but as we move forward, we will have cover type company,
     * user, order, order details and much more
     * 
     * This repository should be handle all of the classes.
     */
    public interface IRepository<T> where T : class
    {
        //T - Category (For NOW)
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties=null, bool tracked=true);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity); //We can remove more than one entity
    }
}
