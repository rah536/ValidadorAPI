namespace ValidadorAPI.Models
{
    public class Vehiculos
    {
        public string Dominio { get; set; } = string.Empty;
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public bool? TieneMulta { get; set; }
        public DateTime? Fecha_Ultima_Consulta { get; set; }
        public bool? Estado { get; set; }
    }
}