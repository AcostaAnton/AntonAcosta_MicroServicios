var builder = WebApplication.CreateBuilder(args);

// ── Registrar YARP leyendo configuración desde appsettings.json ──
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// ── Endpoint informativo en la raíz ─────────────────────────────
app.MapGet("/", () => new
{
    service  = "API Gateway",
    version  = "1.0",
    routes   = new[]
    {
        "/api/products  → ProductService",
        "/api/customers → CustomerService",
        "/api/orders    → OrderService",
    }
});

// ── Activar el proxy inverso ─────────────────────────────────────
app.MapReverseProxy();

app.Run();
