﻿
using BulkyBook.Model;
using BulkyBook.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Reprository.IReprository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int IncrementCount(ShoppingCart ShoppingCart, int count);
        int DecrementCount(ShoppingCart ShoppingCart, int count);
    }
}
