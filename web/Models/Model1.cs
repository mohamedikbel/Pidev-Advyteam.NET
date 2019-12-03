namespace web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<commentaire> commentaires { get; set; }
        public virtual DbSet<document> documents { get; set; }
        public virtual DbSet<employe> employes { get; set; }
        public virtual DbSet<post> posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<commentaire>()
                .Property(e => e.com)
                .IsUnicode(false);

            modelBuilder.Entity<commentaire>()
                .HasMany(e => e.employes)
                .WithMany(e => e.commentaires)
                .Map(m => m.ToTable("employe_commentaire").MapLeftKey("commentaires_id").MapRightKey("employes_id"));

            modelBuilder.Entity<commentaire>()
                .HasMany(e => e.posts)
                .WithMany(e => e.commentaires)
                .Map(m => m.ToTable("post_commentaire").MapLeftKey("commentaires_id").MapRightKey("posts_id"));

            modelBuilder.Entity<document>()
                .Property(e => e.nomdoc)
                .IsUnicode(false);

            modelBuilder.Entity<document>()
                .HasMany(e => e.employes)
                .WithMany(e => e.documents)
                .Map(m => m.ToTable("employe_document").MapLeftKey("documents_id").MapRightKey("employes_id"));

            modelBuilder.Entity<employe>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .Property(e => e.codeqr)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .Property(e => e.image)
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
                .Property(e => e.numtel)
                .IsUnicode(false);

            modelBuilder.Entity<employe>()
                .Property(e => e.qrlogin)
                .IsUnicode(false);

            modelBuilder.Entity<post>()
                .Property(e => e.body)
                .IsUnicode(false);
        }
    }
}
