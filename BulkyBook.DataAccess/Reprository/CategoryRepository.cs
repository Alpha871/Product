using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Reprository.IReprository;
using BulkyBook.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Reprository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
        private DataProduct _db;

        public CategoryRepository(DataProduct db):base(db) 
        {
			_db = db;
        }
        public void Save()
        {
           _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
