using domain.entities;
using servicepattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data.Infrastructure;

namespace service
{
    public class FormationService : Service<formation>, IFormationService
    {   
        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork UOW = new UnitOfWork(factory);
        public FormationService() : base(UOW)
        {
        }

        
    }
}
