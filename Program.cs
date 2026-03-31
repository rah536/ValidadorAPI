using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Lee el archivo secrets.json si existe
builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

// 1. AGREGADO: Le decimos al motor que vamos a usar la arquitectura de Controladores
builder.Services.AddControllers();

// Configuración de Swagger (lo dejamos porque es súper útil para probar)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Validador Logístico API", Version = "v1" });

    // 1. Definimos el esquema de seguridad (El botón "Authorize")
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "La API Key necesaria para acceder a los endpoints. Ejemplo: Logistica_SuperSecreta_2026",
        Type = SecuritySchemeType.ApiKey,
        Name = "x-api-key", // Así se llamará el Header HTTP
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    // 2. Le decimos a Swagger que aplique este candado a todas las rutas
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        { scheme, new List<string>() }
    };
    c.AddSecurityRequirement(requirement);
});

var app = builder.Build();

// Configura el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. AGREGADO: Mapea las rutas (endpoints) hacia tus controladores
app.MapControllers();

app.Run();