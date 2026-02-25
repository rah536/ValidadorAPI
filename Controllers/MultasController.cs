using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient; 
using ValidadorAPI.Models;

namespace ValidadorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MultasController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MultasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Ruta para consultar por DNI: GET /api/multas/conductor/12345678
        [HttpGet("conductor/{dni}")]
        public IActionResult ConsultarConductor(int dni)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrEmpty(connectionString)) 
            {
                return StatusCode(500, "Error interno: No se encontró la cadena de conexión.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DNI, Genero, Nombre, Apellido, Estado_LibreDeuda, Fecha_Ultima_Consulta FROM Conductor WHERE DNI = @dni";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@dni", dni);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var conductor = new Conductores
                            {
                                DNI = (int)reader["DNI"],
                                Genero = reader["Genero"].ToString()!,
                                Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() : null,
                                Apellido = reader["Apellido"] != DBNull.Value ? reader["Apellido"].ToString() : null,
                                // Si Estado_LibreDeuda es 1 (True), Tiene_Multas es False
                                Tiene_Multas = reader["Estado_LibreDeuda"] != DBNull.Value ? !(bool)reader["Estado_LibreDeuda"] : null,
                                Fecha_Ultima_Consulta = reader["Fecha_Ultima_Consulta"] != DBNull.Value ? (DateTime)reader["Fecha_Ultima_Consulta"] : null
                            };
                            return Ok(conductor);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Esto atrapará errores como "No se pudo conectar al servidor"
                    return StatusCode(500, $"Error conectando a la base de datos: {ex.Message}");
                }
            }
            return NotFound("Conductor no encontrado en la base de datos.");
        }

        // Ruta para consultar por Dominio: GET /api/multas/vehiculo/AB123CD
        [HttpGet("vehiculo/{dominio}")]
        public IActionResult ConsultarVehiculo(string dominio)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrEmpty(connectionString)) 
            {
                return StatusCode(500, "Error interno: No se encontró la cadena de conexión.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Dominio, Marca, Estado_LibreDeuda, Fecha_Ultima_Consulta FROM Vehiculo WHERE Dominio = @dominio";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@dominio", dominio);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var vehiculo = new Vehiculos
                            {
                                Dominio = reader["Dominio"].ToString()!,
                                Marca = reader["Marca"] != DBNull.Value ? reader["Marca"].ToString() : null,
                                Tiene_Multas = reader["Estado_LibreDeuda"] != DBNull.Value ? !(bool)reader["Estado_LibreDeuda"] : null,
                                Fecha_Ultima_Consulta = reader["Fecha_Ultima_Consulta"] != DBNull.Value ? (DateTime)reader["Fecha_Ultima_Consulta"] : null
                            };
                            return Ok(vehiculo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error conectando a la base de datos: {ex.Message}");
                }
            }
            return NotFound("Vehículo no encontrado en la base de datos.");
        }
    }
}