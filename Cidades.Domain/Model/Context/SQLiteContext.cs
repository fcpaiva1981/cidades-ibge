using Microsoft.EntityFrameworkCore;

namespace Cidades.Domain.Model.Context;

public class SQLiteContext : DbContext
{
    public SQLiteContext() { }

    public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) {  }

    public DbSet<Cidades> Cidades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map table names
        modelBuilder.Entity<Cidades>().ToTable("cidades", "test");
        modelBuilder.Entity<Cidades>(entity =>
        {
            entity.HasIndex(e => e.IBGE).IsUnique();
            
        });
        base.OnModelCreating(modelBuilder);
    }
}