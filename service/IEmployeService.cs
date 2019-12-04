using domain.entities;
using servicepattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public interface IEmployeService : IService<tache>
    {
        List<tache> MaxEmp();
    }
}
