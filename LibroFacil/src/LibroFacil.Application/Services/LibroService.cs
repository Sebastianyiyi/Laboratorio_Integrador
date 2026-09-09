using LibroFacil.Application.DTOs;
using LibroFacil.Application.Exceptions;
using LibroFacil.Application.Interfaces;
using LibroFacil.Domain.Entities;

namespace LibroFacil.Application.Services;

public class LibroService
{
    private readonly ILibroRepository _repositorio;

    public LibroService(ILibroRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<LibroResponseDto>> ListarAsync()
    {
        var libros = await _repositorio.ObtenerTodosAsync();
        return libros.Select(MapearAResponse);
    }

    public async Task<LibroResponseDto> ObtenerPorIdAsync(int id)
    {
        var libro = await _repositorio.ObtenerPorIdAsync(id)
            ?? throw new AppException($"No existe un libro con Id {id}.");

        return MapearAResponse(libro);
    }

    public async Task<LibroResponseDto> RegistrarAsync(LibroRequestDto dto)
    {
        var existente = await _repositorio.ObtenerPorIsbnAsync(dto.Isbn);
        if (existente is not null)
            throw new AppException($"Ya existe un libro con el ISBN {dto.Isbn}.");

        var libro = new Libro(dto.Isbn, dto.Titulo, dto.Autor, dto.AnioPublicacion, dto.Stock);
        await _repositorio.AgregarAsync(libro);
        return MapearAResponse(libro);
    }

    public async Task<LibroResponseDto> ActualizarAsync(int id, LibroRequestDto dto)
    {
        var libro = await _repositorio.ObtenerPorIdAsync(id)
            ?? throw new AppException($"No existe un libro con Id {id}.");

        var libroConMismoIsbn = await _repositorio.ObtenerPorIsbnAsync(dto.Isbn);
        if (libroConMismoIsbn is not null && libroConMismoIsbn.Id != id)
            throw new AppException($"Ya existe otro libro con el ISBN {dto.Isbn}.");

        libro.Actualizar(dto.Isbn, dto.Titulo, dto.Autor, dto.AnioPublicacion, dto.Stock);
        await _repositorio.ActualizarAsync(libro);
        return MapearAResponse(libro);
    }

    public async Task EliminarAsync(int id)
    {
        var libro = await _repositorio.ObtenerPorIdAsync(id)
            ?? throw new AppException($"No existe un libro con Id {id}.");

        await _repositorio.EliminarAsync(libro);
    }

    private static LibroResponseDto MapearAResponse(Libro libro) =>
        new(
            libro.Id,
            libro.Isbn,
            libro.Titulo,
            libro.Autor,
            libro.AnioPublicacion,
            libro.Stock
        );
}