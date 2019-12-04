using domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class FormationView
    {

        public int id { get; set; }

        public string titre { get; set; }

        public string description { get; set; }

        public DateTime date_debut { get; set; }

        public DateTime date_fin { get; set; }
        public string img { get; set; }

        public string type { get; set; }

        public formateur formateur { get; set; }

        public IEnumerable<skills> skillss { get; set; }
    }
}