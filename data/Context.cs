namespace data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using domain.entities;

    public partial class Context : DbContext
    {
        public Context():base("name=Context")
        {
        }

        public virtual DbSet<document> document { get; set; }
        public virtual DbSet<employe> employe { get; set; }
        public virtual DbSet<formateur> formateur { get; set; }
        public virtual DbSet<formation> formation { get; set; }
        public virtual DbSet<invetation> invetation { get; set; }
        public virtual DbSet<post> post { get; set; }
        public virtual DbSet<projet> projet { get; set; }
        public virtual DbSet<question> question { get; set; }
        public virtual DbSet<reponce> reponce { get; set; }
        public virtual DbSet<skills> skills { get; set; }
        public virtual DbSet<tache> tache { get; set; }
        public virtual DbSet<test> test { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<document>()
                .Property(e => e.nomdoc)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .Property(e => e.EM_Password)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .Property(e => e.codeqr)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .Property(e => e.nom)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .Property(e => e.prenom)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .HasMany(e => e.invetation)
                .WithRequired(e => e.employe)
                .HasForeignKey(e => e.idEmploye)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<employe>()
                .HasMany(e => e.tache)
                .WithOptional(e => e.employe)
                .HasForeignKey(e => e.employe_EM_Id);

            modelBuilder.Entity<employe>()
                .HasMany(e => e.formation)
                .WithMany(e => e.employe)
                .Map(m => m.ToTable("participation", "advyteam").MapLeftKey("idEmploye").MapRightKey("idFormation"));

            modelBuilder.Entity<employe>()
                .HasMany(e => e.document)
                .WithMany(e => e.employe)
                .Map(m => m.ToTable("document_employe").MapLeftKey("employes_EM_Id").MapRightKey("documents_id"));

            modelBuilder.Entity<formateur>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<formateur>()
                .Property(e => e.nomPrenom)
                .IsUnicode(false);

            modelBuilder.Entity<formateur>()
                .Property(e => e.specialiste)
                .IsUnicode(false);

            modelBuilder.Entity<formateur>()
                .HasMany(e => e.formation)
                .WithOptional(e => e.formateur)
                .HasForeignKey(e => e.formateur_id);

            modelBuilder.Entity<formateur>()
                .HasMany(e => e.skills)
                .WithMany(e => e.formateur)
                .Map(m => m.ToTable("formateur_skills").MapLeftKey("Formateur_id").MapRightKey("skillss_id"));

            modelBuilder.Entity<formation>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<formation>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<formation>()
                .Property(e => e.titre)
                .IsUnicode(false);

            modelBuilder.Entity<formation>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<formation>()
                .HasMany(e => e.invetation)
                .WithRequired(e => e.formation)
                .HasForeignKey(e => e.idFormation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<formation>()
                .HasMany(e => e.test)
                .WithOptional(e => e.formation)
                .HasForeignKey(e => e.formation_id);

            modelBuilder.Entity<invetation>()
                .Property(e => e.etat_Invitation)
                .IsUnicode(false);

            modelBuilder.Entity<post>()
                .Property(e => e.body)
                .IsUnicode(false);

            modelBuilder.Entity<projet>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<projet>()
                .Property(e => e.nom)
                .IsUnicode(false);

            modelBuilder.Entity<projet>()
                .HasMany(e => e.tache)
                .WithOptional(e => e.projet)
                .HasForeignKey(e => e.projet_id);

            modelBuilder.Entity<question>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.libelle)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .HasMany(e => e.reponce)
                .WithOptional(e => e.question)
                .HasForeignKey(e => e.question_id);

            modelBuilder.Entity<reponce>()
                .Property(e => e.libelle)
                .IsUnicode(false);

            modelBuilder.Entity<skills>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<skills>()
                .Property(e => e.level)
                .IsUnicode(false);

            modelBuilder.Entity<skills>()
                .HasMany(e => e.formation)
                .WithMany(e => e.skills)
                .Map(m => m.ToTable("formation_skills", "advyteam").MapLeftKey("skillss_id").MapRightKey("Formation_id"));

            modelBuilder.Entity<tache>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<tache>()
                .Property(e => e.nom)
                .IsUnicode(false);

            modelBuilder.Entity<tache>()
                .HasMany(e => e.skills)
                .WithMany(e => e.tache)
                .Map(m => m.ToTable("tache_skills", "advyteam").MapLeftKey("Tache_id").MapRightKey("skillss_id"));

            modelBuilder.Entity<test>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<test>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<test>()
                .HasMany(e => e.question)
                .WithOptional(e => e.test)
                .HasForeignKey(e => e.test_id);
        }
    }
}
