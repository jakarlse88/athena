using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Athena.Models;

namespace Athena.Data
{
    public partial class AthenaDbContext : DbContext
    {
        #pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public AthenaDbContext()
        {
        }

        public AthenaDbContext(DbContextOptions<AthenaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Form> Form { get; set; }
        public virtual DbSet<FormFamily> FormFamily { get; set; }
        public virtual DbSet<FormFamilyName> FormFamilyName { get; set; }
        public virtual DbSet<FormMovement> FormMovement { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Movement> Movement { get; set; }
        public virtual DbSet<Name> Name { get; set; }
        public virtual DbSet<NameCategory> NameCategory { get; set; }
        public virtual DbSet<RelativeDirection> RelativeDirection { get; set; }
        public virtual DbSet<RotationCategory> RotationCategory { get; set; }
        public virtual DbSet<Stance> Stance { get; set; }
        public virtual DbSet<StanceCategory> StanceCategory { get; set; }
        public virtual DbSet<StanceType> StanceType { get; set; }
        public virtual DbSet<StanceTypeName> StanceTypeName { get; set; }
        public virtual DbSet<Technique> Technique { get; set; }
        public virtual DbSet<TechniqueCategory> TechniqueCategory { get; set; }
        public virtual DbSet<TechniqueName> TechniqueName { get; set; }
        public virtual DbSet<TechniqueType> TechniqueType { get; set; }
        public virtual DbSet<Transition> Transition { get; set; }
        public virtual DbSet<TransitionCategory> TransitionCategory { get; set; }
        public virtual DbSet<TransitionCategoryTransition> TransitionCategoryTransition { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasOne(d => d.FormFamily)
                    .WithMany(p => p.Form)
                    .HasForeignKey(d => d.FormFamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Form_FormFamily");
            });

            modelBuilder.Entity<FormFamilyName>(entity =>
            {
                entity.HasKey(e => new { e.FormFamilyId, e.NameId });

                entity.HasOne(d => d.FormFamily)
                    .WithMany(p => p.FormFamilyName)
                    .HasForeignKey(d => d.FormFamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormFamilyName_FormFamily");

                entity.HasOne(d => d.Name)
                    .WithMany(p => p.FormFamilyName)
                    .HasForeignKey(d => d.NameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormFamilyName_Name");
            });

            modelBuilder.Entity<FormMovement>(entity =>
            {
                entity.HasKey(e => new { e.FormId, e.MovementId });

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.FormMovement)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormMovement_Form");

                entity.HasOne(d => d.Movement)
                    .WithMany(p => p.FormMovement)
                    .HasForeignKey(d => d.MovementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormMovement_Movement");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Movement>(entity =>
            {
                entity.HasOne(d => d.Stance)
                    .WithMany(p => p.Movement)
                    .HasForeignKey(d => d.StanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Stance");

                entity.HasOne(d => d.Technique)
                    .WithMany(p => p.Movement)
                    .HasForeignKey(d => d.TechniqueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Technique");

                entity.HasOne(d => d.Transition)
                    .WithMany(p => p.Movement)
                    .HasForeignKey(d => d.TransitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Transition");
            });

            modelBuilder.Entity<Name>(entity =>
            {
                entity.Property(e => e.Name1)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(200);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.NameNavigation)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Name_Language");

                entity.HasOne(d => d.NameCategory)
                    .WithMany(p => p.Name)
                    .HasForeignKey(d => d.NameCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Name_NameCategory");
            });

            modelBuilder.Entity<NameCategory>(entity =>
            {
                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RelativeDirection>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RotationCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Stance>(entity =>
            {
                entity.HasOne(d => d.StanceCategory)
                    .WithMany(p => p.Stance)
                    .HasForeignKey(d => d.StanceCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stance_StanceCategory");

                entity.HasOne(d => d.StanceType)
                    .WithMany(p => p.Stance)
                    .HasForeignKey(d => d.StanceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stance_StanceType");
            });

            modelBuilder.Entity<StanceCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StanceType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StanceTypeName>(entity =>
            {
                entity.HasKey(e => new { e.StanceTypeId, e.NameId });

                entity.HasOne(d => d.Name)
                    .WithMany(p => p.StanceTypeName)
                    .HasForeignKey(d => d.NameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StanceTypeName_Name");

                entity.HasOne(d => d.StanceType)
                    .WithMany(p => p.StanceTypeName)
                    .HasForeignKey(d => d.StanceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StanceTypeName_StanceType");
            });

            modelBuilder.Entity<Technique>(entity =>
            {
                entity.HasOne(d => d.TechniqueCategory)
                    .WithMany(p => p.Technique)
                    .HasForeignKey(d => d.TechniqueCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Technique_TechniqueCategory");

                entity.HasOne(d => d.TechniqueType)
                    .WithMany(p => p.Technique)
                    .HasForeignKey(d => d.TechniqueTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Technique_TechniqueType");
            });

            modelBuilder.Entity<TechniqueCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TechniqueName>(entity =>
            {
                entity.HasKey(e => new { e.TechniqueId, e.NameId });

                entity.HasOne(d => d.Name)
                    .WithMany(p => p.TechniqueName)
                    .HasForeignKey(d => d.NameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TechniqueName_Name");

                entity.HasOne(d => d.Technique)
                    .WithMany(p => p.TechniqueName)
                    .HasForeignKey(d => d.TechniqueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TechniqueName_Technique");
            });

            modelBuilder.Entity<TechniqueType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transition>(entity =>
            {
                entity.HasOne(d => d.RelationDirection)
                    .WithMany(p => p.Transition)
                    .HasForeignKey(d => d.RelationDirectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transition_RelativeDirection");

                entity.HasOne(d => d.RotationCategory)
                    .WithMany(p => p.Transition)
                    .HasForeignKey(d => d.RotationCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transition_RotationCategory");
            });

            modelBuilder.Entity<TransitionCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TransitionCategoryTransition>(entity =>
            {
                entity.HasKey(e => new { e.TransitionId, e.TransitionCategoryId });

                entity.HasOne(d => d.TransitionCategory)
                    .WithMany(p => p.TransitionCategoryTransition)
                    .HasForeignKey(d => d.TransitionCategoryId)
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
