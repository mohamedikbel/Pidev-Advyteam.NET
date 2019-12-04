namespace domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.commentaire")]
    public partial class commentaire
    {
        [Required]
        public String comment { get; set; }
        public int id { get; set; }

        public int? commentaireEvzl360_id { get; set; }

        public int? employecommentaire_id { get; set; }

        public virtual employe employe { get; set; }

        public virtual evaluation360 evaluation360 { get; set; }
    }
}
