namespace domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.projet")]
    public partial class projet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public projet()
        {
            tache = new HashSet<tache>();
        }

        public int id { get; set; }

        public DateTime? date_debut { get; set; }

        public DateTime? date_fin { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public int dureeEstimee { get; set; }

        public int dureeReelle { get; set; }

        [Column(TypeName = "bit")]
        public bool? isFinished { get; set; }

        [StringLength(255)]
        public string nom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tache> tache { get; set; }
    }
}
