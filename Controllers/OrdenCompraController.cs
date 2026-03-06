using ApiPruebaVoultech.Api.BD;
using ApiPruebaVoultech.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPruebaVoultech.Api.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ApiPruebaVoultech.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdenCompraController : ControllerBase
{
    private readonly AppDbContext _db;

    public OrdenCompraController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/OrdenCompra
    [HttpGet]
    public async Task<ActionResult<List<OrdenCompraGetDto>>> Get()
    {
        var ordenes = await _db.OrdenCompras.Select(oc => new OrdenCompraGetDto
        {
            Id = oc.Id,
            Cliente = oc.Cliente,
            FechaCreacion = oc.FechaCreacion,
            Total = oc.Total,
            Productos = oc.OrdenProductos.Select(op => new ProductoGetDto
            {
                ProductId = op.Producto.Id,
                Nombre = op.Producto.Nombre,
                Precio = op.Producto.Precio,
            }).ToList()
        }).ToListAsync();

        return Ok(ordenes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrdenCompra>> GetId(int id)
    {
        var orden = await _db.OrdenCompras.Where(oc => oc.Id == id).Select(oc => new OrdenCompraGetDto
        {
            Id = oc.Id,
            Cliente = oc.Cliente,
            FechaCreacion = oc.FechaCreacion,
            Total = oc.Total,
            Productos = oc.OrdenProductos.Select(op => new ProductoGetDto
            {
                ProductId = op.Producto.Id,
                Nombre = op.Producto.Nombre,
                Precio = op.Producto.Precio
            }).ToList()
        }).FirstOrDefaultAsync();
        if (orden == null)
        {
            return NotFound(); 
        }

        return Ok(orden);
    }

    [HttpPost]
    public async Task<ActionResult<OrdenCompra>> Create([FromBody] OrdenCompraPostDto ordenCompra)
    {
        decimal totalPrecio = 0;

        var productos = await _db.Productos.Where(p => ordenCompra.ProductoIds.Contains(p.Id)).ToListAsync();

        if (productos.Count != ordenCompra.ProductoIds.Count)
        {
            return BadRequest("Uno o más productos no existen.");
        }
        if (productos.Count == 0)
        {
            return BadRequest("No se han proporcionado productos.");
        }
        if (ordenCompra.ProductoIds.Count != ordenCompra.ProductoIds.Distinct().Count())
        {
            return BadRequest("No se pueden agregar productos duplicados a la orden.");
        }

        totalPrecio = productos.Sum(p => p.Precio);

        if (totalPrecio > 500)
        {
            totalPrecio *= 0.90m;
        }
        if (productos.Count >5)
        {
            totalPrecio *= 0.95m;
        }

        var compra = new OrdenCompra
        {
            Cliente = ordenCompra.Cliente,
            Total = totalPrecio,
            OrdenProductos = productos.Select(p => new OrdenProducto
            {
                ProductoId = p.Id
            }).ToList()
        };




        _db.OrdenCompras.Add(compra);
        await _db.SaveChangesAsync();

        return Ok(CreatedAtAction(nameof(GetId), new { id = compra.Id }, compra));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] OrdenCompraPostDto ordenCompra)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (ordenCompra.ProductoIds.Count != ordenCompra.ProductoIds.Distinct().Count())
            return BadRequest("No se permiten productos repetidos.");

        var orden = await _db.OrdenCompras
            .Include(o => o.OrdenProductos)
            .ThenInclude(op => op.Producto)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (orden is null)
            return NotFound();

        var productos = await _db.Productos
            .Where(p => ordenCompra.ProductoIds.Contains(p.Id))
            .ToListAsync();

        if (productos.Count != ordenCompra.ProductoIds.Count)
            return BadRequest("Uno o más productos no existen.");

        orden.Cliente = ordenCompra.Cliente;

        var ids = ordenCompra.ProductoIds.ToHashSet();

        var aEliminar = orden.OrdenProductos
            .Where(op => !ids.Contains(op.ProductoId))
            .ToList();

        _db.OrdenProductos.RemoveRange(aEliminar);

        var actuales = orden.OrdenProductos.Select(op => op.ProductoId).ToHashSet();

        var nuevos = ids
            .Where(idp => !actuales.Contains(idp))
            .Select(idp => new OrdenProducto
            {
                ProductoId = idp,
                OrdenCompraId = orden.Id
            });

        _db.OrdenProductos.AddRange(nuevos);

        decimal total = productos.Sum(p => p.Precio);

        if (total > 500)
            total *= 0.90m;

        if (productos.Count > 5)
            total *= 0.95m;

        orden.Total = total;

        await _db.SaveChangesAsync();

        var resultado = new OrdenCompraGetDto
        {
            Cliente = orden.Cliente,
            FechaCreacion = orden.FechaCreacion,
            Total = orden.Total,
            Productos = orden.OrdenProductos.Select(op => new ProductoGetDto
            {
                ProductId = op.Producto.Id,
                Nombre = op.Producto.Nombre,
                Precio = op.Producto.Precio
            }).ToList()
        };

        return Ok(resultado);

    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var orden = await _db.OrdenCompras.Include(o => o.OrdenProductos).FirstOrDefaultAsync(o => o.Id == id);

        if (orden is null)
            return NotFound();

        _db.OrdenProductos.RemoveRange(orden.OrdenProductos);

        _db.OrdenCompras.Remove(orden);

        await _db.SaveChangesAsync();

        return Ok();
    }
}