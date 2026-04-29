using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PDIEntities.Models;

namespace PDIDatabase.Data;

public partial class PDItDbContext : DbContext
{
    public PDItDbContext()
    {
    }

    public PDItDbContext(DbContextOptions<PDItDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-L4MMBQG;Initial Catalog=PDI;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Connect Timeout=60");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Users__AF2DBB99329C5BAD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
