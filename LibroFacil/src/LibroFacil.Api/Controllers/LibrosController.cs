using LibroFacil.Api.DTOs;
using LibroFacil.Application.DTOs;
using LibroFacil.Application.Exceptions;
using LibroFacil.Application.Services;
using LibroFacil.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibroFacil.Api.Controllers;

[ApiController]
[Route("api/libros")]
public class LibrosController : ControllerBase
{
    private readonly LibroService _servicio;

    public LibrosController(LibroService servicio)
    {
        _servicio = servicio;
    }

    // GET /api/libros
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LibroResponseDto>>> Listar()
    {
        var libros = await _servicio.ListarAsync();
        return Ok(libros);
    }

    // GET /api/libros/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LibroResponseDto>> ObtenerPorId(int id)
    {
        try
        {
            var libro = await _servicio.ObtenerPorIdAsync(id);
            return Ok(libro);
        }
        catch (AppException ex)
        {
            return NotFound(new ErrorResponseDto(ex.Message));
        }
    }

    // POST /api/libros
    [HttpPost]
    public async Task<ActionResult<LibroResponseDto>> Registrar([FromBody] LibroRequestDto dto)
    {
        try
        {
            var creado = await _servicio.RegistrarAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }
        catch (DomainException ex)
        {
            return BadRequest(new ErrorResponseDto(ex.Message));
        }
        catch (AppException ex)
        {
            return Conflict(new ErrorResponseDto(ex.Message));
        }
    }

    // PUT /api/libros/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<LibroResponseDto>> Actualizar(int id, [FromBody] LibroRequestDto dto)
    {
        try
        {
            var actualizado = await _servicio.ActualizarAsync(id, dto);
            return Ok(actualizado);
        }
        catch (DomainException ex)
        {
            return BadRequest(new ErrorResponseDto(ex.Message));
        }
        catch (AppException ex) when (ex.Message.Contains("No existe"))
        {
            return NotFound(new ErrorResponseDto(ex.Message));
        }
        catch (AppException ex)
        {
            return Conflict(new ErrorResponseDto(ex.Message));
        }
    }

    // DELETE /api/libros/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        try
        {
            await _servicio.EliminarAsync(id);
            return NoContent();
        }
        catch (AppException ex)
        {
            return NotFound(new ErrorResponseDto(ex.Message));
        }
    }
}