using Microsoft.EntityFrameworkCore;
using Athena.Models.Entities;

#nullable disable

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

        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<FormFamily> FormFamilies { get; set; }
        public virtual DbSet<Movement> Movements { get; set; }
        public virtual DbSet<NumberInSequence> NumberInSequences { get; set; }
        public virtual DbSet<RelativeDirection> RelativeDirections { get; set; }
        public virtual DbSet<RotationCategory> RotationCategories { get; set; }
        public virtual DbSet<Stance> Stances { get; set; }
        public virtual DbSet<StanceCategory> StanceCategories { get; set; }
        public virtual DbSet<StanceType> StanceTypes { get; set; }
        public virtual DbSet<Technique> Techniques { get; set; }
        public virtual DbSet<TechniqueCategory> TechniqueCategories { get; set; }
        public virtual DbSet<TechniqueType> TechniqueTypes { get; set; }
        public virtual DbSet<Transition> Transitions { get; set; }
        public virtual DbSet<TransitionCategory> TransitionCategories { get; set; }
        public virtual DbSet<TransitionCategoryTransition> TransitionCategoryTransitions { get; set; }

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

                entity.ToTable("Form");

                entity.HasIndex(e => e.FormFamilyName, "IX_Form_FormFamilyName");

                entity.HasIndex(e => e.NameHangeul, "UK_Form_NameHangeul")
                    .IsUnique()
                    .HasFilter("([NameHangeul] IS NOT NULL)");

                entity.HasIndex(e => e.NameHanja, "UK_Form_NameHanja")
                    .IsUnique()
                    .HasFilter("([NameHanja] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FormFamilyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.HasOne(d => d.FormFamilyNameNavigation)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.FormFamilyName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Form_FormFamily");
            });

            modelBuilder.Entity<FormFamily>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("FormFamily");

                entity.HasIndex(e => e.NameHangeul, "UK_FormFamily_NameHangeul")
                    .IsUnique()
                    .HasFilter("([NameHangeul] IS NOT NULL)");

                entity.HasIndex(e => e.NameHanja, "UK_FormFamily_NameHanja")
                    .IsUnique()
                    .HasFilter("([NameHanja] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);
            });

            modelBuilder.Entity<Movement>(entity =>
            {
                entity.ToTable("Movement");

                entity.HasIndex(e => e.TechniqueName, "IX_Movement_TechniqueName");

                entity.HasIndex(e => e.TransitionId, "IX_Movement_TransitionId");

                entity.HasIndex(e => new { e.StanceName, e.TechniqueName, e.TransitionId }, "UK_Movement")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.StanceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TechniqueName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.StanceNameNavigation)
                    .WithMany(p => p.Movements)
                    .HasForeignKey(d => d.StanceName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Stance");

                entity.HasOne(d => d.TechniqueNameNavigation)
                    .WithMany(p => p.Movements)
                    .HasForeignKey(d => d.TechniqueName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Technique");

                entity.HasOne(d => d.Transition)
                    .WithMany(p => p.Movements)
                    .HasForeignKey(d => d.TransitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Transition");
            });

            modelBuilder.Entity<NumberInSequence>(entity =>
            {
                entity.HasKey(e => new { e.FormName, e.MovementId, e.OrdinalNumber });

                entity.ToTable("NumberInSequence");

                entity.HasIndex(e => e.MovementId, "IX_NumberInSequence_MovementId");

                entity.Property(e => e.FormName).HasMaxLength(50);

                entity.HasOne(d => d.FormNameNavigation)
                    .WithMany(p => p.NumberInSequences)
                    .HasForeignKey(d => d.FormName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NumberInSequence_Form");

                entity.HasOne(d => d.Movement)
                    .WithMany(p => p.NumberInSequences)
                    .HasForeignKey(d => d.MovementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NumberInSequence_Movement");
            });

            modelBuilder.Entity<RelativeDirection>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("RelativeDirection");

                entity.HasIndex(e => e.NameHangeul, "UK_RelativeDirection_NameHangeul")
                    .IsUnique()
                    .HasFilter("([NameHangeul] IS NOT NULL)");

                entity.HasIndex(e => e.NameHanja, "UK_RelativeDirection_NameHanja")
                    .IsUnique()
                    .HasFilter("([NameHanja] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);
            });

            modelBuilder.Entity<RotationCategory>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("RotationCategory");

                entity.HasIndex(e => e.NameHangeul, "UK_RotationCategory_NameHangeul")
                    .IsUnique()
                    .HasFilter("([NameHangeul] IS NOT NULL)");

                entity.HasIndex(e => e.NameHanja, "UK_RotationCategory_NameHanja")
                    .IsUnique()
                    .HasFilter("([NameHanja] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);
            });

            modelBuilder.Entity<Stance>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("Stance");

                entity.HasIndex(e => e.StanceCategoryName, "IX_Stance_StanceCategoryName");

                entity.HasIndex(e => e.StanceTypeName, "IX_Stance_StanceTypeName");

                entity.HasIndex(e => new { e.Name, e.StanceCategoryName, e.StanceTypeName }, "UK_Stance")
                    .IsUnique();

                entity.HasIndex(e => e.NameHangeul, "UK_Stance_NameHangeul")
                    .IsUnique()
                    .HasFilter("([NameHangeul] IS NOT NULL)");

                entity.HasIndex(e => e.NameHanja, "UK_Stance_NameHanja")
                    .IsUnique()
                    .HasFilter("([NameHanja] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.Property(e => e.StanceCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StanceTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.StanceCategoryNameNavigation)
                    .WithMany(p => p.Stances)
                    .HasForeignKey(d => d.StanceCategoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stance_StanceCategory");

                entity.HasOne(d => d.StanceTypeNameNavigation)
                    .WithMany(p => p.Stances)
                    .HasForeignKey(d => d.StanceTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stance_StanceType");
            });

            modelBuilder.Entity<StanceCategory>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("StanceCategory");

                entity.HasIndex(e => e.NameHangeul, "UK_StanceCategory_NameHangeul")
                    .IsUnique()
                    .HasFilter("([NameHangeul] IS NOT NULL)");

                entity.HasIndex(e => e.NameHanja, "UK_StanceCategory_NameHanja")
                    .IsUnique()
                    .HasFilter("([NameHanja] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);
            });

            modelBuilder.Entity<StanceType>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("StanceType");

                entity.HasIndex(e => e.NameHangeul, "UK_StanceType_NameHangeul")
                    .IsUnique()
                    .HasFilter("([NameHangeul] IS NOT NULL)");

                entity.HasIndex(e => e.NameHanja, "UK_StanceType_NameHanja")
                    .IsUnique()
                    .HasFilter("([NameHanja] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);
            });

            modelBuilder.Entity<Technique>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK_Technique_1");

                entity.ToTable("Technique");

                entity.HasIndex(e => e.TechniqueCategoryName, "IX_Technique_TechniqueCategoryName");

                entity.HasIndex(e => e.TechniqueTypeName, "IX_Technique_TechniqueTypeName");

                entity.HasIndex(e => new { e.Name, e.TechniqueCategoryName, e.TechniqueTypeName }, "UK_Technique")
                    .IsUnique();

                entity.HasIndex(e => e.NameHangeul, "UK_Technique_NameHangeul")
                    .IsUnique()
                    .HasFilter("([NameHangeul] IS NOT NULL)");

                entity.HasIndex(e => e.NameHanja, "UK_Technique_NameHanja")
                    .IsUnique()
                    .HasFilter("([NameHanja] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);

                entity.Property(e => e.TechniqueCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TechniqueTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.TechniqueCategoryNameNavigation)
                    .WithMany(p => p.Techniques)
                    .HasForeignKey(d => d.TechniqueCategoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Technique_TechniqueCategory");

                entity.HasOne(d => d.TechniqueTypeNameNavigation)
                    .WithMany(p => p.Techniques)
                    .HasForeignKey(d => d.TechniqueTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Technique_TechniqueType");
            });

            modelBuilder.Entity<TechniqueCategory>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("TechniqueCategory");

                entity.HasIndex(e => e.NameHangeul, "UK_TechniqueCategory_NameHangeul")
                    .IsUnique()
                    .HasFilter("([NameHangeul] IS NOT NULL)");

                entity.HasIndex(e => e.NameHanja, "UK_TechniqueCategory_NameHanja")
                    .IsUnique()
                    .HasFilter("([NameHanja] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);
            });

            modelBuilder.Entity<TechniqueType>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("TechniqueType");

                entity.HasIndex(e => e.NameHangeul, "UK_TechniqueType_NameHangeul")
                    .IsUnique()
                    .HasFilter("([NameHangeul] IS NOT NULL)");

                entity.HasIndex(e => e.NameHanja, "UK_TechniqueType_NameHanja")
                    .IsUnique()
                    .HasFilter("([NameHanja] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.NameHangeul).HasMaxLength(50);

                entity.Property(e => e.NameHanja).HasMaxLength(50);
            });

            modelBuilder.Entity<Transition>(entity =>
            {
                entity.ToTable("Transition");

                entity.HasIndex(e => e.RotationCategoryName, "IX_Transition_RotationCategoryName");

                entity.HasIndex(e => e.TechniqueName, "IX_Transition_TechniqueName");

                entity.HasIndex(e => new { e.RelativeDirectionName, e.RotationCategoryName, e.StanceName, e.TechniqueName }, "UK_Transition")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(1000);

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
                    .WithMany(p => p.Transitions)
                    .HasForeignKey(d => d.RelativeDirectionName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transition_RelativeDirection");

                entity.HasOne(d => d.RotationCategoryNameNavigation)
                    .WithMany(p => p.Transitions)
                    .HasForeignKey(d => d.RotationCategoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transition_RotationCategory");

                entity.HasOne(d => d.TechniqueNameNavigation)
                    .WithMany(p => p.Transitions)
                    .HasForeignKey(d => d.TechniqueName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transition_Technique");
            });

            modelBuilder.Entity<TransitionCategory>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("TransitionCategory");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);
            });

            modelBuilder.Entity<TransitionCategoryTransition>(entity =>
            {
                entity.HasKey(e => new { e.TransitionCategoryName, e.TransitionId });

                entity.ToTable("TransitionCategoryTransition");

                entity.HasIndex(e => e.TransitionId, "IX_TransitionCategoryTransition_TransitionId");

                entity.Property(e => e.TransitionCategoryName).HasMaxLength(50);

                entity.HasOne(d => d.TransitionCategoryNameNavigation)
                    .WithMany(p => p.TransitionCategoryTransitions)
                    .HasForeignKey(d => d.TransitionCategoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransitionCategoryTransition_TransitionCategory");

                entity.HasOne(d => d.Transition)
                    .WithMany(p => p.TransitionCategoryTransitions)
                    .HasForeignKey(d => d.TransitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransitionCategoryTransition_Transition");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
