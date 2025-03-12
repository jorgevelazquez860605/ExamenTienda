
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities;

public class ClienteArticulo
{
    public int Id { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    public int ArticuloId { get; set; }
    public Articulo Articulo { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow; 
}


