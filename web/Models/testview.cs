using domain.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class testview
    {
        public int id { get; set; }

        [Required(ErrorMessage = "description est obligatoire")]
        public string description { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "image est obligatoire")]
        public string img { get; set; }

        public FormationView formation { get; set; }
    }
}