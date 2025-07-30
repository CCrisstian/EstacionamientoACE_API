namespace EstacionamientoACE_API.Models.DTO
{
    public class Estacionamiento_DTO
    {
        public required string EstNombre { get; set; }
        public required string Est_direccion { get; set; }
        public required string Est_Dias_Atencion { get; set; }
        public required string Est_Hra_Atencion { get; set; }
        public bool? EstDiasFeriadoAtencion { get; set; }
        public bool? EstFinDeSemanaAtencion { get; set; }
        public string? EstHoraFinDeSemana { get; set; }
        public bool EstDisponibilidad { get; set; }

        public double? EstLatitud { get; set; }
        public double? EstLongitud { get; set; }

        // Cantidad de plazas disponibles
        public int CantidadPlazasDisponibles { get; set; }
    }
}
