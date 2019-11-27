
using domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Infrastructure
{
    public class DataBaseFactory :Disposable,IDataBaseFactory
    {
        private Model1 ctx;
        public DataBaseFactory()
        {
            ctx = new Model1();
        }
        public Model1 DataContext
        {
            get
            {
                return ctx;
            }
        }
        public override void DisposeCore()
        {
            if (DataContext != null)
                DataContext.Dispose();
        }
    }
}
