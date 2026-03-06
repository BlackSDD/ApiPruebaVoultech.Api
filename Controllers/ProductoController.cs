using ApiPruebaVoultech.Api.BD;
using ApiPruebaVoultech.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiPruebaVoultech.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductoController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoGetDto>>> Get()
        {
            var productos = await _db.Productos.Select(p => new ProductoGetDto
            {
                ProductId = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio
            }).ToListAsync();

            return Ok(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoGetIdDto>> GetID(int id)
        {
            var producto = await _db.Productos.Where(p => p.Id == id).Select(p => new ProductoGetIdDto
            {
                ProductId = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                OrdenProductos = p.OrdenProductos.Select(op => new OrdenCompraProductoDto
                {
                    Id = op.OrdenCompra.Id,
                    Cliente = op.OrdenCompra.Cliente,
                    Total = op.OrdenCompra.Total,
                    FechaCreacion = op.OrdenCompra.FechaCreacion
                }).ToList()
            }).FirstOrDefaultAsync();

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductoGetDto productoDto)
        {
            var nombre = productoDto.Nombre.ToLower().Trim();

            var existe = await _db.Productos.AnyAsync(p => p.Nombre.ToLower() == nombre);

            if (existe)
            {
                return BadRequest("Ya existe un producto con ese nombre.");
            }

            if (productoDto.Precio < 10)
            {
                return BadRequest("El precio no puede ser menor a 10.");
            }

            var producto = new Models.Producto
            {
                Nombre = productoDto.Nombre,
                Precio = productoDto.Precio
            };
            _db.Productos.Add(producto);
            await _db.SaveChangesAsync();
            return Ok(producto);
        }

    }
}
