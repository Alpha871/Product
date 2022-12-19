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
    public class ProductRepository : Repository<Product>, IProductRepository
	{
        private DataProduct _db;

        public ProductRepository(DataProduct db):base(db) 
        {
			_db = db;
        }
        public void Save()
        {
           _db.SaveChanges();
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(p => p.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title= obj.Title;
                objFromDb.Description= obj.Description;
                objFromDb.Price= obj.Price;
                objFromDb.Price50= obj.Price50 ;
                objFromDb.Price100= obj.Price100 ;
                objFromDb.ISBN= obj.ISBN;
                objFromDb.CategoryId= obj.CategoryId;
                objFromDb.CoverTypeId= obj.CoverTypeId;
                objFromDb.Author = obj.Author;
                objFromDb.ListPrice= obj.ListPrice;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl; 
                }
                    
            }
  
        }
    }
}
