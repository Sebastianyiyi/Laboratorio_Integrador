namespace LibroFacil.Application.DTOs;

public record LibroRequestDto(
    string Isbn,
    string Titulo,
    string Autor,
    int AnioPublicacion,
    int Stock
);

public record LibroResponseDto(
    int Id,
    string Isbn,
    string Titulo,
    string Autor,
    int AnioPublicacion,
    int Stock
);