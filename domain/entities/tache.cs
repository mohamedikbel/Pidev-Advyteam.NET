namespace domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.tache")]
    public partial class tache
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tache()
        {
            skills = new HashSet<skills>();
        }

        public int id { get; set; }

        public TimeSpan? date_debut { get; set; }

        public TimeSpan? date_fin { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public int dureeEtimee { get; set; }

        public int dureeReelle { get; set; }

        [Column(TypeName = "bit")]
        public bool? isFinished { get; set; }

        [StringLength(255)]
        public string nom { get; set; }

        public int? employe_EM_Id { get; set; }

        public int? projet_id { get; set; }

        public virtual employe employe { get; set; }

        public virtual projet projet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<skills> skills { get; set; }
    }
}
