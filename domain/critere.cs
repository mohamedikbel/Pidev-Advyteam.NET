namespace domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.critere")]
    public partial class critere
    {
        public critere()
        {
                
        }
        public int id { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string niveau { get; set; }

        public int? emlployeid { get; set; }

        public virtual evaluation evaluation { get; set; }
    }
}
