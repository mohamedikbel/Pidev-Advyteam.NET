using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class TForm
    {
        public int idT { get; set; }

        [Required(ErrorMessage = "Formation est obligatoire")]
        public int idf { get; set; }
    }
}