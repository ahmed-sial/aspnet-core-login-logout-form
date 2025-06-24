using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoginForm.Models;

public partial class LoginFormDbContext : DbContext
{
    public LoginFormDbContext()
    {
    }

    public LoginFormDbContext(DbContextOptions<LoginFormDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UsersCredential> UsersCredentials { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsersCredential>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__UsersCre__A9D105352C499828");

            entity.ToTable("UsersCredential");

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
