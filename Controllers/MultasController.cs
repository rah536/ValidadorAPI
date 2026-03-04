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
                // Actualizamos la consulta con los campos exactos de tu tabla
                string query = "SELECT DNI, Nombre, Apellido, Genero, Domicilio, TieneMulta, Fecha_Ultima_Consulta, Estado FROM Conductor WHERE DNI = @dni";
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
                                Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() : null,
                                Apellido = reader["Apellido"] != DBNull.Value ? reader["Apellido"].ToString() : null,
                                Genero = reader["Genero"].ToString()!,
                                Domicilio = reader["Domicilio"] != DBNull.Value ? reader["Domicilio"].ToString() : null,
                                TieneMulta = reader["TieneMulta"] != DBNull.Value ? (bool)reader["TieneMulta"] : null,
                                Fecha_Ultima_Consulta = reader["Fecha_Ultima_Consulta"] != DBNull.Value ? (DateTime)reader["Fecha_Ultima_Consulta"] : null,
                                Estado = reader["Estado"] != DBNull.Value ? (bool)reader["Estado"] : null
                            };
                            return Ok(conductor);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error conectando a la base de datos: {ex.Message}");
                }
            }
            return NotFound("Conductor no encontrado en la base de datos.");
        }

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
                // Actualizamos la consulta para Vehiculo
                string query = "SELECT Dominio, Marca, Modelo, TieneMulta, Fecha_Ultima_Consulta, Estado FROM Vehiculo WHERE Dominio = @dominio";
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
                                Modelo = reader["Modelo"] != DBNull.Value ? reader["Modelo"].ToString() : null,
                                TieneMulta = reader["TieneMulta"] != DBNull.Value ? (bool)reader["TieneMulta"] : null,
                                Fecha_Ultima_Consulta = reader["Fecha_Ultima_Consulta"] != DBNull.Value ? (DateTime)reader["Fecha_Ultima_Consulta"] : null,
                                Estado = reader["Estado"] != DBNull.Value ? (bool)reader["Estado"] : null
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