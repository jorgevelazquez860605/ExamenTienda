using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Data.Migrations;

/// <inheritdoc />
public partial class SeedData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Insertar Tiendas
        migrationBuilder.InsertData(
            table: "Tiendas",
            columns: new[] { "Sucursal", "Direccion" },
            values: new object[,]
            {
        { "Tienda Centro", "Av. Principal #123, Ciudad" },
        { "Tienda Norte", "Calle 5 Norte #456, Ciudad" },
        { "Tienda Sur", "Av. Sur #789, Ciudad" }
            });


        // Insertar Artículos (relacionados con Tiendas)
        migrationBuilder.Sql(@"
            INSERT INTO Articulos (Codigo, Descripcion, Precio, Imagen, Stock)
            VALUES
            ('A001', 'Laptop Gamer', 15000, 'laptop.jpg', 10),
            ('A002', 'Mouse Inalámbrico', 500, 'mouse.jpg', 30),
            ('A003', 'Teclado Mecánico', 1200, 'teclado.jpg', 20),
            ('A004', 'Monitor 24 pulgadas', 4500, 'monitor.jpg', 15),
            ('A005', 'Audífonos Bluetooth', 1800, 'audifonos.jpg', 25),
        
            ('B001', 'Smartphone Pro', 20000, 'smartphone.jpg', 12),
            ('B002', 'Cargador Rápido', 800, 'cargador.jpg', 40),
            ('B003', 'Tablet 10""', 8500, 'tablet.jpg', 18),
            ('B004', 'Bocina Inteligente', 2200, 'bocina.jpg', 22),
            ('B005', 'Smartwatch', 3200, 'smartwatch.jpg', 17),
        
            ('C001', 'Cámara Fotográfica', 12500, 'camara.jpg', 8),
            ('C002', 'Trípode Profesional', 2100, 'tripode.jpg', 15),
            ('C003', 'Lámpara LED', 750, 'lampara.jpg', 35),
            ('C004', 'Dron Profesional', 25000, 'dron.jpg', 5),
            ('C005', 'Micrófono USB', 3200, 'microfono.jpg', 20);
        ");


        migrationBuilder.Sql(@"
            INSERT INTO ArticuloTiendas (ArticuloId, TiendaId, Fecha)
            VALUES
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'A001'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Centro'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'A002'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Centro'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'A003'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Centro'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'A004'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Centro'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'A005'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Centro'), GETDATE()),
                                             
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'B001'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Norte'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'B002'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Norte'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'B003'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Norte'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'B004'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Norte'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'B005'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Norte'), GETDATE()),
                                             
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'C001'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Sur'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'C002'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Sur'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'C003'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Sur'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'C004'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Sur'), GETDATE()),
            ((SELECT ArticuloId FROM Articulos WHERE Codigo = 'C005'), (SELECT TiendaId FROM Tiendas WHERE Sucursal = 'Tienda Sur'), GETDATE());
        ");
    }



    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Eliminar los datos en caso de revertir la migración
        migrationBuilder.Sql("DELETE FROM Articulos WHERE TiendaId IN (SELECT TiendaId FROM Tiendas);");
        migrationBuilder.Sql("DELETE FROM Tiendas;");
    }

}
