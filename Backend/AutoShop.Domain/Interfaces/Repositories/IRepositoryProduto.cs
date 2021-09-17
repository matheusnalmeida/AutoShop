﻿using AutoShop.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Domain.Interfaces.Repositories
{
    public interface IRepositoryProduto : IRepositoryBase<Produto>
    {
        public IQueryable<Produto> GetByIds(IEnumerable<string> ids);
    }
}
