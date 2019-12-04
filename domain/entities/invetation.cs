namespace domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.invetation")]
    public partial class invetation
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage ="merci de selectioner employer")]
        public int idEmploye { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idFormation { get; set; }

        [StringLength(255)]
        public string etat_Invitation { get; set; }

        [Column(TypeName = "bit")]
        public bool? vu { get; set; }

        public virtual employe employe { get; set; }

        public virtual formation formation { get; set; }
    }
}
