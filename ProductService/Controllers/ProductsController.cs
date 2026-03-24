using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers;

public record Product(int Id, string Name, string Description, decimal Price, int Stock);

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> _products = new()
    {
        new(1, "Laptop Pro 15",   "Laptop de alto rendimiento con procesador i9",  1499.99m, 10),
        new(2, "Mouse Inalámbrico","Mouse ergonómico con DPI ajustable",              29.99m, 150),
        new(3, "Teclado Mecánico", "Teclado mecánico RGB con switches Cherry MX",    89.99m,  75),
        new(4, "Monitor 4K 27\"",  "Monitor UHD con panel IPS y 144Hz",             499.99m,  20),
        new(5, "Audífonos BT",    "Audífonos inalámbricos con cancelación de ruido", 199.99m,  40),
    };

    /// <summary>Retorna la lista completa de productos.</summary>
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll() => Ok(_products);

    /// <summary>Retorna un producto por su ID.</summary>
    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return product is null ? NotFound(new { message = $"Producto {id} no encontrado." }) : Ok(product);
    }
}
