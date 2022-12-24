using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Reprository.IReprository;
using BulkyBook.Model;
using BulkyBook.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Reprository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private DataProduct _db;

        public ShoppingCartRepository(DataProduct db):base(db) 
        {
			_db = db;
        }

		public int DecrementCount(ShoppingCart ShoppingCart, int count)
		{
			ShoppingCart.Count -= count;
			return ShoppingCart.Count;
		}

		public int IncrementCount(ShoppingCart ShoppingCart, int count)
		{
			ShoppingCart.Count += count;
			return ShoppingCart.Count;
		}
	}
}
