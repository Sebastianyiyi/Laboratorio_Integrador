using LibroFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibroFacil.Infrastructure.Persistence;

public class LibroFacilDbContext : DbContext
{
    public LibroFacilDbContext(DbContextOptions<LibroFacilDbContext> options)
        : base(options) { }

    public DbSet<Libro> Libros => Set<Libro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Libro>(entidad =>
        {
            entidad.ToTable("Libros");
            entidad.HasKey(l => l.Id);
            entidad.Property(l => l.Isbn).IsRequired().HasMaxLength(20);
            entidad.HasIndex(l => l.Isbn).IsUnique();
            entidad.Property(l => l.Titulo).IsRequired().HasMaxLength(300);
            entidad.Property(l => l.Autor).IsRequired().HasMaxLength(200);
            entidad.Property(l => l.AnioPublicacion).IsRequired();
            entidad.Property(l => l.Stock).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}