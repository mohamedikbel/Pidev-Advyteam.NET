namespace domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.question")]
    public partial class question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public question()
        {
            reponce = new HashSet<reponce>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string img { get; set; }

        [StringLength(255)]
        public string libelle { get; set; }

        [StringLength(255)]
        public string type { get; set; }

        public int? test_id { get; set; }

        public virtual test test { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reponce> reponce { get; set; }
    }
}
