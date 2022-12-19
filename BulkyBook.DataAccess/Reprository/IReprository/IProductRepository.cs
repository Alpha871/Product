
using BulkyBook.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Reprository.IReprository
{
    public interface IProductRepository: IRepository<Product>
    {
        void Update(Product obj);
        void Save();
    }
}
