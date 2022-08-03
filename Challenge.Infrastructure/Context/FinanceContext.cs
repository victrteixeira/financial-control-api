using Challenge.Domain;
using Challenge.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Context;

public class FinanceContext : DbContext
{
    public FinanceContext()
    {
    }

    public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) 
    {
    }
    
    public DbSet<Receitas> TReceitas { get; set; }
    public DbSet<Despesas> TDespesas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DespesasMap());
        modelBuilder.ApplyConfiguration(new ReceitasMap());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}