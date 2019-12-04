using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class questionView
    {
        public int id { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "image est obligatoire")]
        public string img { get; set; }

        [Required(ErrorMessage = "libelle est obligatoire")]
        public string libelle { get; set; }

        [Required(ErrorMessage = "type est obligatoire")]
        public string type { get; set; }

       

        public testview test { get; set; }
    }
}