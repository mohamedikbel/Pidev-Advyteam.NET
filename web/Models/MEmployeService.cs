using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFinance.Data.Infrastructure;

namespace web.Models
{
    public class MEmployeService : Service<employe>, IMEmployeService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork UTK = new UnitOfWork(Factory);
        public MEmployeService() : base(UTK)
        {

        }
    }
}