namespace Store.Bussines.DTOs;

public class ArticuloDto
{
    public int ArticuloId { get; set; }
    public string Descripcion { get; set; }
    public string Imagen { get; set; }
    public int Stock { get; set; }
    public string ImagenBase64 { get; set; }
    public string Tienda { get; set; }
}
