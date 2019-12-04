namespace domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.evaluation")]
    public partial class evaluation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public evaluation()
        {
            avis = new HashSet<avis>();
            critere = new HashSet<critere>();
        }

        public int id { get; set; }

        [Column(TypeName = "bit")]
        public bool Launch { get; set; }

        [Column(TypeName = "bit")]
        public bool Traiter { get; set; }

        public DateTime? datedebut { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string nom { get; set; }

        public int? evaluationEmploye_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<avis> avis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<critere> critere { get; set; }

        public virtual employe employe { get; set; }
    }
}
