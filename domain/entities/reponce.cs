namespace domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.reponce")]
    public partial class reponce
    {
        public int id { get; set; }

        [Column(TypeName = "bit")]
        public bool? isValid { get; set; }

        [StringLength(255)]
        public string libelle { get; set; }

        public int? question_id { get; set; }

        public virtual question question { get; set; }
    }
}
