﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepositoryBase<T> GetRepositoryBase<T>() where T:class;
        void Commit();
        void Dispose();
        object GetRepository<T>();
    }
}
