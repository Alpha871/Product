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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
	{
        private DataProduct _db;

        public CompanyRepository(DataProduct db):base(db) 
        {
			_db = db;
        }
        public void Save()
        {
           _db.SaveChanges();
        }

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
  
        }
    }
}
