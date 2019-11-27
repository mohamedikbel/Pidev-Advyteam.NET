namespace domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.post")]
    public partial class post
    {
        public int id { get; set; }

        [StringLength(255)]
        public string body { get; set; }
    }
}
