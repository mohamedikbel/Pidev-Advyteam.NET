using data.Infrastructure;
using domain.entities;
using servicepattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
   public  class EmployeeService : Service<employe> ,IEmployeeService
    {
        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork UOW = new UnitOfWork(factory);

        public EmployeeService():base(UOW)
        {
        }
    }
}
