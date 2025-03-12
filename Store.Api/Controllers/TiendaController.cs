using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Bussines.Interfaces;
using Store.Data.Entities;

namespace Store.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TiendaController : ControllerBase
{
    private readonly ITiendaService _tiendaService;

    public TiendaController(ITiendaService tiendaService)
    {
        _tiendaService = tiendaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tienda>>> GetTiendas()
    {
        return Ok(await _tiendaService.GetAllTiendasAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tienda>> GetTienda(int id)
    {
        var tienda = await _tiendaService.GetTiendaByIdAsync(id);
        if (tienda == null)
            return NotFound();
        return Ok(tienda);
    }

    [HttpPost]
    public async Task<IActionResult> AddTienda(Tienda tienda)
    {
        await _tiendaService.AddTiendaAsync(tienda);
        return CreatedAtAction(nameof(GetTienda), new { id = tienda.TiendaId }, tienda);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTienda(int id, Tienda tienda)
    {
        if (id != tienda.TiendaId)
            return BadRequest();

        await _tiendaService.UpdateTiendaAsync(tienda);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTienda(int id)
    {
        await _tiendaService.DeleteTiendaAsync(id);
        return NoContent();
    }
}
