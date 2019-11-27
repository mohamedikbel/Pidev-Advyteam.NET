
using domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Infrastructure
{
    public interface IDataBaseFactory:IDisposable
    {
        Model1 DataContext { get; }
        // void Dispose(); methode cachée qui exite deja dans l'interface IDesposable
    }
}
