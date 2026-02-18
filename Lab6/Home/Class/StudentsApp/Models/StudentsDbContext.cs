using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentsApp.Models;

public partial class StudentsDbContext : DbContext
{
    public StudentsDbContext()
    {
    }

    public StudentsDbContext(DbContextOptions<StudentsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<tblStudents> tblStudents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=StudentsDB;User Id=sa;Password=Somethingstupid1@;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblStudents>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__tblStude__32C52A79B91C3388");

            entity.ToTable("tblStudents");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
