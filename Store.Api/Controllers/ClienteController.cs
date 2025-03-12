using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Bussines.DTOs;
using Store.Bussines.Interfaces;
using Store.Data.Entities;

namespace Store.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
        
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        return Ok(await _clienteService.GetAllClientesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetCliente(int id)
    {
        var cliente = await _clienteService.GetClienteByIdAsync(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> AddCliente(ClienteDto cliente)
    {
        try
        {
            await _clienteService.AddClienteAsync(cliente);
            return Ok();

        }
        catch (ArgumentException ex) // Captura la excepción específica
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex) {
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }       
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCliente(int id, Cliente cliente)
    {
        if (id != cliente.ClienteId) return BadRequest();
        await _clienteService.UpdateClienteAsync(cliente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        await _clienteService.DeleteClienteAsync(id);
        return NoContent();
    }
}
