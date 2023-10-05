using Atawiz.UnitTestDemo.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Atawiz.UnitTestDemo.EF.Context;

public partial class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.Property(e => e.Assignee)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}