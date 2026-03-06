namespace ApiPruebaVoultech.Api.Models
{
    public class OrdenCompra
    {
        public int Id { get; set; }
        public required string Cliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrdenProducto> OrdenProductos { get; set; } = new List<OrdenProducto>();
    }
}
