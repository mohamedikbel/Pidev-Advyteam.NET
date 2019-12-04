namespace domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.evaluation360")]
    public partial class evaluation360
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public evaluation360()
        {
            commentaire = new HashSet<commentaire>();
        }

        public int id { get; set; }
        [Required(ErrorMessage = "Invalid Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? datedebut { get; set; }

        public int? evaluationEmploye360_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<commentaire> commentaire { get; set; }

        public virtual employe employe { get; set; }
    }
}
