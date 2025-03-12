using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Bussines.DTOs;
using Store.Bussines.Interfaces;


namespace Store.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CarritoController : ControllerBase
{
    private readonly ICarritoService _carritoService;

    public CarritoController(ICarritoService carritoService)
    {
        _carritoService = carritoService;
    }
    [HttpPost("agregar-al-carrito")]
    public async Task<IActionResult> AgregarAlCarrito([FromBody] ClienteArticuloDto clienteArticulo)
    {
        try
        {
            clienteArticulo.ClienteId = (int)ObtenerClienteIdDesdeToken();
            var result = await _carritoService.AgregarAlCarrito(clienteArticulo);
            if (!result)
            {
                return BadRequest(new { mensaje = "No hay stock disponible" });
            }
            return Ok(new { mensaje = "Artículo agregado al carrito" });

        }
        catch (ArgumentException ex) // Captura la excepción específica
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }       
    }

    [HttpGet()]
    public async Task<IActionResult> ObtenerCarrito()
    {
        try
        {
            int clienteId = (int)ObtenerClienteIdDesdeToken();
            var carrito = await _carritoService.ObtenerCarrito(clienteId);
            return Ok(carrito);

        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }        
    }
    
    [HttpDelete("eliminar/{articuloId}")]
    public async Task<IActionResult> EliminarArticulo( int articuloId)
    {
        try
        {
            int clienteId = (int)ObtenerClienteIdDesdeToken();
            await _carritoService.EliminarArticulo(clienteId, articuloId);
            return Ok(new { mensaje = "Artículo eliminado del carrito" });

        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }       
    }

    [HttpDelete("eliminar/todos/{articuloId}")]
    public async Task<IActionResult> EliminarArticuloTodos(int articuloId)
    {
        try
        {
            int clienteId = (int)ObtenerClienteIdDesdeToken();
            await _carritoService.EliminarArticuloTodos(clienteId, articuloId);
            return Ok(new { mensaje = "Artículo eliminado del carrito" });

        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }
    }

    private int? ObtenerClienteIdDesdeToken()
    {
        var userClaims = HttpContext.User;
        var clienteIdClaim = userClaims.FindFirst("ClienteId")?.Value;

        return clienteIdClaim != null && int.TryParse(clienteIdClaim, out int clienteId)
            ? clienteId
            : throw new ArgumentException("Token no valido");
    }
}