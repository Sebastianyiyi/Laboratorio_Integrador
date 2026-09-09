using LibroFacil.Application.Interfaces;
using LibroFacil.Domain.Entities;
using LibroFacil.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibroFacil.Infrastructure.Repositories;

public class LibroRepositoryEf : ILibroRepository
{
    private readonly LibroFacilDbContext _contexto;

    public LibroRepositoryEf(LibroFacilDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<Libro>> ObtenerTodosAsync()
        => await _contexto.Libros.AsNoTracking().ToListAsync();

    public async Task<Libro?> ObtenerPorIdAsync(int id)
        => await _contexto.Libros.FirstOrDefaultAsync(l => l.Id == id);

    public async Task<Libro?> ObtenerPorIsbnAsync(string isbn)
        => await _contexto.Libros.FirstOrDefaultAsync(l => l.Isbn == isbn);

    public async Task AgregarAsync(Libro libro)
    {
        await _contexto.Libros.AddAsync(libro);
        await _contexto.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Libro libro)
    {
        _contexto.Libros.Update(libro);
        await _contexto.SaveChangesAsync();
    }

    public async Task EliminarAsync(Libro libro)
    {
        _contexto.Libros.Remove(libro);
        await _contexto.SaveChangesAsync();
    }
}