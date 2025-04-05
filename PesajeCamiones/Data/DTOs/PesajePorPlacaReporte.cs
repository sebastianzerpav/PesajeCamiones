namespace PesajeCamiones.Data.DTOs
{
    public class PesajePorPlacaReporte
    {
        public string? Placa { get; set; }
        public string? Marca { get; set; }
        public int NumeroEjes { get; set; }
        public float Peso { get; set; }
        public string? Estacion { get; set; }
        public DateOnly FechaPesaje { get; set; }
    }
}
