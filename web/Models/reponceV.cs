namespace web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using domain.entities;

   
    public  class reponceV
    {
        public int id { get; set; }

        [Required(ErrorMessage = "obligatoire")]
        [Column(TypeName = "bit")]
        public bool? isValid { get; set; }

        [Required(ErrorMessage = "libelle est obligatoire")]
        [StringLength(255)]
        public string libelle { get; set; }
        

        public virtual question question { get; set; }
    }
}
