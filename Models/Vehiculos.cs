namespace ValidadorAPI.Models
{
    public class Vehiculos
    {
        public string Dominio { get; set; } = string.Empty;
        public string Marca { get; set; }
        public bool Tiene_Multa { get; set; }
        public DateTime Fecha_Ultima_Consulta { get; set; }
    }
}