namespace Infrastructure.EntityFramework.DataContext;

using backend.Domain.Entities;

using Microsoft.EntityFrameworkCore;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Transacao> Transacoes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Descricao).IsRequired();
            entity.Property(e => e.Valor).HasPrecision(18, 2).IsRequired();
            entity.HasOne(e => e.Categoria)
                  .WithMany(c => c.Transacoes)
                  .HasForeignKey(e => e.CategoriaId);
            entity.HasOne(e => e.Pessoa)
                  .WithMany(p => p.Transacoes)
                  .HasForeignKey(e => e.PessoaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Descricao).IsRequired();
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Nome).IsRequired();
        });
    }
}