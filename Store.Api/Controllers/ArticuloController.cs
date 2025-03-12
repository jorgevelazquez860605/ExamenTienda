using Microsoft.AspNetCore.Mvc;
using Store.Bussines.DTOs;
using Store.Bussines.Interfaces;
using Store.Data.Entities;

namespace Store.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ArticuloController : ControllerBase
{
    private readonly IArticuloService _articuloService;

    public ArticuloController(IArticuloService articuloService)
    {
        _articuloService = articuloService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ArticuloDto>>> GetAllArticulos()
    {
        var articulos = await _articuloService.GetAllArticulosAsync();
        if (articulos == null || articulos.Count == 0)
        {
            return NotFound("No hay artículos disponibles.");
        }

        return Ok(articulos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Articulo>> GetArticulo(int id)
    {
        var articulo = await _articuloService.GetArticuloByIdAsync(id);
        if (articulo == null) return NotFound();
        return Ok(articulo);
    }

    [HttpPost]
    public async Task<IActionResult> AddArticulo(Articulo articulo)
    {
        await _articuloService.AddArticuloAsync(articulo);
        return CreatedAtAction(nameof(GetArticulo), new { id = articulo.ArticuloId }, articulo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArticulo(int id, Articulo articulo)
    {
        if (id != articulo.ArticuloId) return BadRequest();
        await _articuloService.UpdateArticuloAsync(articulo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticulo(int id)
    {
        await _articuloService.DeleteArticuloAsync(id);
        return NoContent();
    }
}
