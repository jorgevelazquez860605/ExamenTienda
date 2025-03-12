using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities;

public class Cliente
{
    public int ClienteId { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string Direccion { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public ICollection<ClienteArticulo> ClienteArticulos { get; set; }
}
