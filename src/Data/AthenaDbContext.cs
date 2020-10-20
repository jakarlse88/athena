using Athena.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Athena.Data
{
    public partial class AthenaDbContext : DbContext
    {
        public AthenaDbContext()
        {
        }

        public AthenaDbContext(DbContextOptions<AthenaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Form> Form { get; set; }
        public virtual DbSet<FormFamily> FormFamily { get; set; }
        public virtual DbSet<Movement> Movement { get; set; }
        public virtual DbSet<NumberInSequence> NumberInSequence { get; set; }
        public virtual DbSet<RelativeDirection> RelativeDirection { get; set; }
        public virtual DbSet<RotationCategory> RotationCategory { get; set; }
        public virtual DbSet<Stance> Stance { get; set; }
        public virtual DbSet<StanceCategory> StanceCategory { get; set; }
        public virtual DbSet<StanceType> StanceType { get; set; }
        public virtual DbSet<Technique> Technique { get; set; }
        public virtual DbSet<TechniqueCategory> TechniqueCategory { get; set; }
        public virtual DbSet<TechniqueType> TechniqueType { get; set; }
        public virtual DbSet<Transition> Transition { get; set; }
        public virtual DbSet<TransitionCategory> TransitionCategory { get; set; }
        public virtual DbSet<TransitionCategoryTransition> TransitionCategoryTransition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.HasIndex(e => e.NameHangeul)
                    .HasDatabaseName("UK_Form_NameHangeul")
                    .IsUnique();

                entity.HasIndex(e => e.NameHanja)
                    .HasDatabaseName("UK_Form_NameHanja")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.FormFamilyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.HasOne(d => d.FormFamilyNameNavigation)
                    .WithMany(p => p.Form)
                    .HasForeignKey(d => d.FormFamilyName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Form_FormFamily");
            });

            modelBuilder.Entity<FormFamily>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.HasIndex(e => e.NameHangeul)
                    .HasDatabaseName("UK_FormFamily_NameHangeul")
                    .IsUnique();

                entity.HasIndex(e => e.NameHanja)
                    .HasDatabaseName("UK_FormFamily_NameHanja")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);
            });

            modelBuilder.Entity<Movement>(entity =>
            {
                entity.HasIndex(e => new { e.StanceName, e.TechniqueName, e.TransitionId })
                    .HasDatabaseName("UK_Movement")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.StanceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TechniqueName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.StanceNameNavigation)
                    .WithMany(p => p.Movement)
                    .HasForeignKey(d => d.StanceName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Stance");

                entity.HasOne(d => d.TechniqueNameNavigation)
                    .WithMany(p => p.Movement)
                    .HasForeignKey(d => d.TechniqueName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Technique");

                entity.HasOne(d => d.Transition)
                    .WithMany(p => p.Movement)
                    .HasForeignKey(d => d.TransitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Transition");
            });

            modelBuilder.Entity<NumberInSequence>(entity =>
            {
                entity.HasKey(e => new { e.FormName, e.MovementId, e.OrdinalNumber });

                entity.Property(e => e.FormName).HasMaxLength(50);

                entity.HasOne(d => d.FormNameNavigation)
                    .WithMany(p => p.NumberInSequence)
                    .HasForeignKey(d => d.FormName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NumberInSequence_Form");

                entity.HasOne(d => d.Movement)
                    .WithMany(p => p.NumberInSequence)
                    .HasForeignKey(d => d.MovementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NumberInSequence_Movement");
            });

            modelBuilder.Entity<RelativeDirection>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name).HasMaxLength(50);
                
                entity.HasIndex(e => e.NameHangeul)
                    .HasDatabaseName("UK_RelativeDirection_NameHangeul")
                    .IsUnique();

                entity.HasIndex(e => e.NameHanja)
                    .HasDatabaseName("UK_RelativeDirection_NameHanja")
                    .IsUnique();

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);
            });

            modelBuilder.Entity<RotationCategory>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name).HasMaxLength(50);
                
                entity.HasIndex(e => e.NameHangeul)
                    .HasDatabaseName("UK_RotationCategory_NameHangeul")
                    .IsUnique();

                entity.HasIndex(e => e.NameHanja)
                    .HasDatabaseName("UK_RotationCategory_NameHanja")
                    .IsUnique();

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);
            });

            modelBuilder.Entity<Stance>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.HasIndex(e => e.NameHangeul)
                    .HasDatabaseName("UK_Stance_NameHangeul")
                    .IsUnique();

                entity.HasIndex(e => e.NameHanja)
                    .HasDatabaseName("UK_Stance_NameHanja")
                    .IsUnique();

                entity.HasIndex(e => new { e.Name, e.StanceCategoryName, e.StanceTypeName })
                    .HasDatabaseName("UK_Stance")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.Property(e => e.StanceCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StanceTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.StanceCategoryNameNavigation)
                    .WithMany(p => p.Stance)
                    .HasForeignKey(d => d.StanceCategoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stance_StanceCategory");

                entity.HasOne(d => d.StanceTypeNameNavigation)
                    .WithMany(p => p.Stance)
                    .HasForeignKey(d => d.StanceTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stance_StanceType");
            });

            modelBuilder.Entity<StanceCategory>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name).HasMaxLength(50);
                
                entity.HasIndex(e => e.NameHangeul)
                    .HasDatabaseName("UK_StanceCategory_NameHangeul")
                    .IsUnique();

                entity.HasIndex(e => e.NameHanja)
                    .HasDatabaseName("UK_StanceCategory_NameHanja")
                    .IsUnique();

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);
            });

            modelBuilder.Entity<StanceType>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name).HasMaxLength(50);
                
                entity.HasIndex(e => e.NameHangeul)
                    .HasDatabaseName("UK_StanceType_NameHangeul")
                    .IsUnique();

                entity.HasIndex(e => e.NameHanja)
                    .HasDatabaseName("UK_StanceType_NameHanja")
                    .IsUnique();

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);
            });

            modelBuilder.Entity<Technique>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK_Technique_1");

                entity.HasIndex(e => e.NameHangeul)
                    .HasDatabaseName("UK_Technique_NameHangeul")
                    .IsUnique();

                entity.HasIndex(e => e.NameHanja)
                    .HasDatabaseName("UK_Technique_NameHanja")
                    .IsUnique();

                entity.HasIndex(e => new { e.Name, e.TechniqueCategoryName, e.TechniqueTypeName })
                    .HasDatabaseName("UK_Technique")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.Property(e => e.TechniqueCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TechniqueTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.TechniqueCategoryNameNavigation)
                    .WithMany(p => p.Technique)
                    .HasForeignKey(d => d.TechniqueCategoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Technique_TechniqueCategory");

                entity.HasOne(d => d.TechniqueTypeNameNavigation)
                    .WithMany(p => p.Technique)
                    .HasForeignKey(d => d.TechniqueTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Technique_TechniqueType");
            });

            modelBuilder.Entity<TechniqueCategory>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasIndex(e => e.NameHangeul)
                    .HasDatabaseName("UK_TechniqueCategory_NameHangeul")
                    .IsUnique();

                entity.HasIndex(e => e.NameHanja)
                    .HasDatabaseName("UK_TechniqueCategory_NameHanja")
                    .IsUnique();

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);
            });

            modelBuilder.Entity<TechniqueType>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasIndex(e => e.NameHangeul)
                    .HasDatabaseName("UK_TechniqueType_NameHangeul")
                    .IsUnique();

                entity.HasIndex(e => e.NameHanja)
                    .HasDatabaseName("UK_TechniqueType_NameHanja")
                    .IsUnique();

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);
            });

            modelBuilder.Entity<Transition>(entity =>
            {
                entity.HasIndex(e => new
                        { e.RelativeDirectionName, e.RotationCategoryName, e.StanceName, e.TechniqueName })
                    .HasDatabaseName("UK_Transition")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RelativeDirectionName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RotationCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StanceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TechniqueName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.RelativeDirectionNameNavigation)
                    .WithMany(p => p.Transition)
                    .HasForeignKey(d => d.RelativeDirectionName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transition_RelativeDirection");

                entity.HasOne(d => d.RotationCategoryNameNavigation)
                    .WithMany(p => p.Transition)
                    .HasForeignKey(d => d.RotationCategoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transition_RotationCategory");

                entity.HasOne(d => d.TechniqueNameNavigation)
                    .WithMany(p => p.Transition)
                    .HasForeignKey(d => d.TechniqueName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transition_Technique");
            });

            modelBuilder.Entity<TransitionCategory>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TransitionCategoryTransition>(entity =>
            {
                entity.HasKey(e => new { e.TransitionCategoryName, e.TransitionId });

                entity.Property(e => e.TransitionCategoryName).HasMaxLength(50);

                entity.HasOne(d => d.TransitionCategoryNameNavigation)
                    .WithMany(p => p.TransitionCategoryTransition)
                    .HasForeignKey(d => d.TransitionCategoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransitionCategoryTransition_TransitionCategory");

                entity.HasOne(d => d.Transition)
                    .WithMany(p => p.TransitionCategoryTransition)
                    .HasForeignKey(d => d.TransitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransitionCategoryTransition_Transition");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}