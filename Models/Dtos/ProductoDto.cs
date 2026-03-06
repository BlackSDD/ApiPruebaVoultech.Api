using System.ComponentModel.DataAnnotations;

namespace ApiPruebaVoultech.Api.Models.Dtos
{
    public class ProductoGetDto
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }
        [Range(10.00,99999999999999999.99)]
        public decimal Precio { get; set; }
    }

    public class ProductoGetIdDto
    {
        public int ProductId { get; set; }

        public required string Nombre { get; set; }
        public decimal Precio { get; set; }

        public List<OrdenCompraProductoDto> OrdenProductos { get; set; } = new();
    }



}
