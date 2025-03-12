﻿namespace Store.Data.Entities;

public class Articulo
{
    public int ArticuloId { get; set; }
    public string Codigo { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string Imagen { get; set; }
    public int Stock { get; set; }

    public ICollection<ArticuloTienda> ArticuloTiendas { get; set; }
    public ICollection<ClienteArticulo> ClienteArticulos { get; set; }
}
