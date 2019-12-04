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
    public class FormateurService : Service<formateur>,IformateurService
    {
        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork UOW = new UnitOfWork(factory);

        public FormateurService():base(UOW)
        {
        }



        public IEnumerable<formateur> GetformateurByTitle(string title)
        {
            return GetMany(f => f.nomPrenom.Contains(title));
        }


        public Context db = new Context();

        public List<formateur> MaxNote()

        {

            var formateurs = new List<formateur>();
            var Array = db.formateur.SqlQuery("SELECT * FROM formateur");


            foreach (var a in Array.OrderByDescending(s => s.note))
            {

                formateurs.Add(a);

            }
            return formateurs;
        }

    }
}
