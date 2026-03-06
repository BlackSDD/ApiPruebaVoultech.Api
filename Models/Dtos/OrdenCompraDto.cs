using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace ApiPruebaVoultech.Api.Models.Dtos
{
    public class OrdenCompraPostDto
    {
        [Required(ErrorMessage = "El Cliente es obligatorio")]
        [StringLength(50, ErrorMessage = "El Cliente no puede tener más de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El cliente solo puede contener letras y espacios")]
        public required string Cliente { get; set; }
        [Required(ErrorMessage = "Tiene que agregar minimo 1 producto")]
        [MinLength(1, ErrorMessage = "Tiene que agregar minimo 1 producto")]
        public List<int> ProductoIds { get; set; } = new List<int>();
    }

    public class OrdenCompraGetDto
    {
        public int Id { get; set; }
        public required string Cliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
        public List<ProductoGetDto> Productos { get; set; } = new();

    }

    public class OrdenCompraProductoDto
    {
        public int Id { get; set; }
        public required string Cliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }

    }
}
