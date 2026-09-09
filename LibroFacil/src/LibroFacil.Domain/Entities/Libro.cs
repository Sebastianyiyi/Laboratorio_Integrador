using LibroFacil.Domain.Exceptions;

namespace LibroFacil.Domain.Entities;

public class Libro
{
    public int Id { get; private set; }
    public string Isbn { get; private set; } = string.Empty;
    public string Titulo { get; private set; } = string.Empty;
    public string Autor { get; private set; } = string.Empty;
    public int AnioPublicacion { get; private set; }
    public int Stock { get; private set; }

    // Constructor requerido por EF Core
    protected Libro() { }

    public Libro(string isbn, string titulo, string autor, int anioPublicacion, int stock)
    {
        SetIsbn(isbn);
        SetTitulo(titulo);
        SetAutor(autor);
        SetAnioPublicacion(anioPublicacion);
        SetStock(stock);
    }

    public void Actualizar(string isbn, string titulo, string autor, int anioPublicacion, int stock)
    {
        SetIsbn(isbn);
        SetTitulo(titulo);
        SetAutor(autor);
        SetAnioPublicacion(anioPublicacion);
        SetStock(stock);
    }

    private void SetIsbn(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            throw new DomainException("El ISBN es obligatorio.");

        Isbn = isbn.Trim();
    }

    private void SetTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new DomainException("El título es obligatorio.");

        Titulo = titulo.Trim();
    }

    private void SetAutor(string autor)
    {
        if (string.IsNullOrWhiteSpace(autor))
            throw new DomainException("El autor es obligatorio.");

        Autor = autor.Trim();
    }

    private void SetAnioPublicacion(int anioPublicacion)
    {
        if (anioPublicacion <= 0 || anioPublicacion > DateTime.UtcNow.Year)
            throw new DomainException("El año de publicación debe ser mayor que 0 y no puede ser mayor al año actual.");

        AnioPublicacion = anioPublicacion;
    }

    private void SetStock(int stock)
    {
        if (stock < 0)
            throw new DomainException("El stock no puede ser negativo.");

        Stock = stock;
    }
}