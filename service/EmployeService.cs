using domain.entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFinance.Data.Infrastructure;
using Service.Pattern;

namespace service
{
  public  class EmployeService : Service<employe>, IEmployeService
    {
      

        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork UTK = new UnitOfWork(Factory);
        public EmployeService() : base(UTK)
        {

        }
    }
}
