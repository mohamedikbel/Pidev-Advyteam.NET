
using domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Infrastructure
{
    public class DataBaseFactory :Disposable,IDataBaseFactory
    {
        private Context ctx;
        public DataBaseFactory()
        {
            ctx = new Context();
        }
        public Context DataContext
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
