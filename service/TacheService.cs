using domain.entities;
using servicepattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data.Infrastructure;
using data;

namespace service
{
    public class TacheService : Service<tache>, ITacheService
    {
        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork UOW = new UnitOfWork(factory);
        Context db = new Context();
        public TacheService() : base(UOW)
        {
        }

        public List<tache> getByEmp(int id)
        {
            List<tache> ts = new List<tache>();
            foreach(var t in GetAll())
            {
                if( t.employe_EM_Id == id)
                {
                    ts.Add(t);
                }
            }

            return ts;
        }
    }
}
