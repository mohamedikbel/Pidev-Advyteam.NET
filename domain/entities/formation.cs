namespace domain.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advyteam.formation")]
    public partial class formation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public formation()
        {
            invetation = new HashSet<invetation>();
            test = new HashSet<test>();
            skills = new HashSet<skills>();
            employe = new HashSet<employe>();
        }

        public int id { get; set; }
        [Display(Name ="Date Debut")]
        [Required(ErrorMessage = "Invalid Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? date_debut { get; set; }

        [Display(Name = "Date Fin")]
        [Required(ErrorMessage = "Invalid Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? date_fin { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description Obligatoitre")]
        [StringLength(255)]
        public string description { get; set; }

        [Display(Name = "Image ")]
        [Required(ErrorMessage = "Image Obligatoitre")]
        [StringLength(255)]
        public string img { get; set; }
        [Display(Name = "Titre")]
        [Required(ErrorMessage = "Titre Obligatoitre")]
        [StringLength(255)]
        public string titre { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type Obligatoitre")]
        [StringLength(255)]
        public string type { get; set; }

        [Display(Name = "Formateur")]
        public int? formateur_id { get; set; }

        public virtual formateur formateur { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<invetation> invetation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<test> test { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<skills> skills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<employe> employe { get; set; }
    }
}
