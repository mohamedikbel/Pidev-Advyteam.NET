namespace data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.tache")]
    public partial class tache
    {
        public int id { get; set; }

        public DateTime? date_debut { get; set; }

        public DateTime? date_fin { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public int dureeEtimee { get; set; }

        public int dureeReelle { get; set; }

        [Column(TypeName = "bit")]
        public bool? isFinished { get; set; }

        [StringLength(255)]
        public string nom { get; set; }

        [StringLength(255)]
        public string phase { get; set; }

        public int? employe_id { get; set; }

        public int? projet_id { get; set; }

        public virtual employe employe { get; set; }

        public virtual projet projet { get; set; }
    }
}
