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
    public class FormateurService : Service<formateur> , IFormateurService
    {   
         static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork UOW = new UnitOfWork(factory);
    public FormateurService():base(UOW)
    {

    }

}
}
