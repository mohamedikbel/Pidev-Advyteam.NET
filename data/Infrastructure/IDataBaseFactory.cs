
using domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Infrastructure
{
    public interface IDataBaseFactory:IDisposable
    {
        Context DataContext { get; }
        // void Dispose(); methode cachée qui exite deja dans l'interface IDesposable
    }
}
