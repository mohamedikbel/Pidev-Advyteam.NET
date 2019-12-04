namespace data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.conge")]
    public partial class conge
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Invalid Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date_deb { get; set; }

        public DateTime? date_demande { get; set; }
        [Required(ErrorMessage = "Invalid Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date_fin { get; set; }

        [StringLength(255)]
        public string etat { get; set; }

        [Column(TypeName = "bit")]
        public bool paye { get; set; }

        public int? employe_id { get; set; }

        public virtual employe employe { get; set; }
    }
}
