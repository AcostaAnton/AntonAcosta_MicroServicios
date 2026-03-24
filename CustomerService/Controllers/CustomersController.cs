using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers;

public record Customer(int Id, string FullName, string Email, string Phone, string City);

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private static readonly List<Customer> _customers = new()
    {
        new(1, "Ana García",     "ana.garcia@email.com",   "+504-9999-1111", "Tegucigalpa"),
        new(2, "Carlos López",   "carlos.lopez@email.com", "+504-9999-2222", "San Pedro Sula"),
        new(3, "María Rodríguez","maria.rod@email.com",    "+504-9999-3333", "La Ceiba"),
        new(4, "Luis Hernández", "luis.hdz@email.com",     "+504-9999-4444", "Comayagua"),
        new(5, "Sofia Martínez", "sofia.mtz@email.com",    "+504-9999-5555", "Tegucigalpa"),
    };

    /// <summary>Retorna la lista completa de clientes.</summary>
    [HttpGet]
    public ActionResult<IEnumerable<Customer>> GetAll() => Ok(_customers);

    /// <summary>Retorna un cliente por su ID.</summary>
    [HttpGet("{id:int}")]
    public ActionResult<Customer> GetById(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        return customer is null ? NotFound(new { message = $"Cliente {id} no encontrado." }) : Ok(customer);
    }
}
