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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private DataProduct _db;

        public ApplicationUserRepository(DataProduct db):base(db) 
        {
			_db = db;
        }
       
    }
}
