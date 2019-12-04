using data;
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
  public  class EmployeService :Service<tache>,IEmployeService
    {


        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork UOW = new UnitOfWork(factory);

        public EmployeService():base(UOW)
        {
        }
        public Context db = new Context();
        public List<tache> MaxEmp()

        {

            var taches = new List<tache>();
            var Array = db.tache.SqlQuery("SELECT * FROM tache");


            foreach (var a in Array.OrderByDescending(x => x.dureeReelle - x.dureeEtimee))
            {

                taches.Add(a);

            }
            return taches;
        }
    }
}
