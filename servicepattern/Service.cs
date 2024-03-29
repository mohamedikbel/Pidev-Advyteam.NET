﻿using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MyFinance.Data.Infrastructure;

namespace Service.Pattern
{
    public class Service<T> : IService<T> where T : class
    {
        IUnitOfWork utk;
        public Service(IUnitOfWork utk)//faible couplage heka aalh staamlna interface 7ata kn saret ghalta fl class tb9a interface fonctionnel
        {
            this.utk = utk;
        }
        public void Add(T Entity)
        {
            utk.GetRepositoryBase<T>().Add(Entity);
        }

        public void Commit()
        {
            utk.Commit();
        }

        public void Delete(Expression<Func<T, bool>> Condition)
        {
            utk.GetRepositoryBase<T>().Delete(Condition);
        }

        public void Delete(T Entity)
        {

            utk.GetRepositoryBase<T>().Delete(Entity);
        }

        public void dispose()
        {
            utk.Dispose();
        }

        public T GetById(string id)
        {
            return utk.GetRepositoryBase<T>().GetById(id);
        }

        public T GetById(int id)
        {
            return utk.GetRepositoryBase<T>().GetById(id);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> Condition = null, Expression<Func<T, bool>> orderBy = null)
        {

            return utk.GetRepositoryBase<T>().GetMany(Condition = null, orderBy = null);
            ;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return utk.GetRepositoryBase<T>().GetAll();
            //return _repository.GetById(id);
            //  return utwk.getRepository<TEntity>().GetById(id);
        }

        public virtual void Update(T Entity)
        {
            utk.GetRepositoryBase<T>().Update(Entity);
        }

      
    }
}

