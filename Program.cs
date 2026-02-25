var builder = WebApplication.CreateBuilder(args);

// Lee el archivo secrets.json si existe
builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

// 1. AGREGADO: Le decimos al motor que vamos a usar la arquitectura de Controladores
builder.Services.AddControllers();

// Configuración de Swagger (lo dejamos porque es súper útil para probar)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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