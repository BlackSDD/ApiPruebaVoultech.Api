namespace ApiPruebaVoultech.Api.Models
{
    public class OrdenProducto
    {
        public int Id { get; set; }
        public int OrdenCompraId { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
        public OrdenCompra OrdenCompra { get; set; } = null!;
    }
}
