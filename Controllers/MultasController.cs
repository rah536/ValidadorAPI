using Microsoft.AspNetCore.Mvc;
using ValidadorAPI.Models;

namespace ValidadorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Esto hace que la URL base sea: /api/multas
    public class MultasController : ControllerBase
    {
        // Ruta para consultar por DNI: GET /api/multas/conductor/12345678
        [HttpGet("conductor/{dni}")]
        public IActionResult ConsultarConductor(int dni)
        {
            // Simulamos que fuimos a la base de datos y trajimos esto:
            var resultadoSimulado = new Conductores
            {
                DNI = dni,
                Genero = "M", // Dato fijo por ahora
                Tiene_Multas = false,
                Fecha_Ultima_Consulta = DateTime.Now
            };

            // Devuelve un código HTTP 200 (OK) junto con el objeto en formato JSON
            return Ok(resultadoSimulado); 
        }

        // Ruta para consultar por Dominio: GET /api/multas/vehiculo/AB123CD
        [HttpGet("vehiculo/{dominio}")]
        public IActionResult ConsultarVehiculo(string dominio)
        {
            var resultadoSimulado = new Vehiculos
            {
                Dominio = dominio,
                Tiene_Multas = true,
                Fecha_Ultima_Consulta = DateTime.Now
            };

            return Ok(resultadoSimulado);
        }
    }
}