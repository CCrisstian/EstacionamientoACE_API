namespace EstacionamientoACE_API.Models.DTO
{
    public class Estacionamiento_DTO
    {
        public required string EstNombre { get; set; }
        public double? EstLatitud { get; set; }
        public double? EstLongitud { get; set; }
    }
}
