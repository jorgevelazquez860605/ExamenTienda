namespace Store.Bussines.DTOs;

public class CarritoDetalleDto
{
    public int ClienteId { get; set; }
    public int ArticuloId { get; set; }
    public string NombreArticulo { get; set; }
    public decimal Precio { get; set; }         
    public int Cantidad { get; set; }           
    public string ImagenBase64 { get; set; }
}
