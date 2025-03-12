namespace Store.Data.Entities;

public class ArticuloTienda
{
    // Clave compuesta: ArticuloId y TiendaId
    public int ArticuloId { get; set; }
    public Articulo Articulo { get; set; }

    public int TiendaId { get; set; }
    public Tienda Tienda { get; set; }

    public DateTime Fecha { get; set; }
}
