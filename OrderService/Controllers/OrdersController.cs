using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers;

public record OrderItem(int ProductId, string ProductName, int Quantity, decimal UnitPrice);
public record Order(int Id, int CustomerId, string CustomerName, DateTime Date, string Status, List<OrderItem> Items, decimal Total);

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private static readonly List<Order> _orders = new()
    {
        new(1, 1, "Ana García",
            new DateTime(2025, 3, 10), "Entregado",
            new List<OrderItem>
            {
                new(1, "Laptop Pro 15",    1, 1499.99m),
                new(2, "Mouse Inalámbrico", 2,   29.99m),
            },
            1559.97m),

        new(2, 2, "Carlos López",
            new DateTime(2025, 3, 15), "En proceso",
            new List<OrderItem>
            {
                new(3, "Teclado Mecánico", 1, 89.99m),
                new(5, "Audífonos BT",     1, 199.99m),
            },
            289.98m),

        new(3, 3, "María Rodríguez",
            new DateTime(2025, 3, 20), "Pendiente",
            new List<OrderItem>
            {
                new(4, "Monitor 4K 27\"", 1, 499.99m),
            },
            499.99m),

        new(4, 1, "Ana García",
            new DateTime(2025, 4, 1), "Pendiente",
            new List<OrderItem>
            {
                new(2, "Mouse Inalámbrico", 1, 29.99m),
                new(3, "Teclado Mecánico",  1, 89.99m),
            },
            119.98m),
    };

    /// <summary>Retorna la lista completa de órdenes.</summary>
    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetAll() => Ok(_orders);

    /// <summary>Retorna una orden por su ID.</summary>
    [HttpGet("{id:int}")]
    public ActionResult<Order> GetById(int id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        return order is null ? NotFound(new { message = $"Orden {id} no encontrada." }) : Ok(order);
    }
}
