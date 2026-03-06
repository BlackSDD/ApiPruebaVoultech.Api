namespace ApiPruebaVoultech.Api.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public decimal Precio { get; set; }

        public ICollection<OrdenProducto> OrdenProductos { get; set; } = new List<OrdenProducto>();

    }
}
