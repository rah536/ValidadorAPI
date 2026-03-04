namespace ValidadorAPI.Models
{
    public class Conductores
    {
        public int DNI { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string? Domicilio { get; set; }
        public bool? TieneMulta { get; set; }
        public DateTime? Fecha_Ultima_Consulta { get; set; }
        public bool? Estado { get; set; }
    }
}