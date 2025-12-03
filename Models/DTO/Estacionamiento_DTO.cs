using System;
using System.Collections.Generic;

namespace EstacionamientoACE_API.Models.DTO
{
    public class Estacionamiento_DTO
    {
        // --- Campos base del estacionamiento ---
        public int EstId { get; set; }
        public int? DuenoLegajo { get; set; }
        public string? EstNombre { get; set; }
        public string? EstProvincia { get; set; }
        public string? EstLocalidad { get; set; }
        public string? EstDireccion { get; set; }
        public double EstPuntaje { get; set; }
        public string? EstDiasAtencion { get; set; }
        public string? EstHraAtencion { get; set; }
        public bool? EstDiasFeriadoAtencion { get; set; }
        public bool? EstFinDeSemanaAtencion { get; set; }
        public string? EstHoraFinDeSemana { get; set; }
        public bool EstDisponibilidad { get; set; }
        public double? EstLatitud { get; set; }
        public double? EstLongitud { get; set; }
        public double EstPuntajeAcumulado { get; set; }
        public int EstCantidadVotos { get; set; }

        // --- Relaciones (DTOs relacionados) ---
        public List<Plaza_DTO>? Plazas { get; set; }
        public List<Tarifa_DTO>? Tarifas { get; set; }
        public List<AceptaMetodoDePagoDTO>? MetodosDePagoAceptados { get; set; }
    }

    public class Plaza_DTO
    {
        public int PlazaId { get; set; }
        public string? PlazaNombre { get; set; }
        public string? PlazaTipo { get; set; }
        public bool PlazaDisponibilidad { get; set; }
        public string? CategoriaDescripcion { get; set; }
    }

    public class Tarifa_DTO
    {
        public int TarifaId { get; set; }
        public double TarifaMonto { get; set; }
        public DateTime TarifaDesde { get; set; }

        public int? CategoriaId { get; set; }
        public string? CategoriaDescripcion { get; set; }

        public int? TiposTarifaId { get; set; }
        public string? TiposTarifaDescripcion { get; set; }
    }

    public class AceptaMetodoDePagoDTO
    {
        public int EstId { get; set; }
        public int MetodoPagoId { get; set; }
        public string? MetodoPagoDescripcion { get; set; }
        public DateTime? AMPDesde { get; set; }
        public DateTime? AMPHasta { get; set; }
    }

    public class CategoriaVehiculoDTO
    {
        public int CategoriaId { get; set; }
        public string? CategoriaDescripcion { get; set; }
    }

    public class TiposTarifaDTO
    {
        public int TiposTarifaId { get; set; }
        public string? TiposTarifaDescripcion { get; set; }
    }

    public class VehiculoAbonadoDTO
    {
        public int VAId { get; set; }
        public string? VehiculoPatente { get; set; }
        public int TarifaId { get; set; }
        public double? TarifaMonto { get; set; }

        public int EstId { get; set; }
        public int PlazaId { get; set; }

        public long NumeroIdentificacion { get; set; }
        public string? TipoIdentificacion { get; set; }
        public DateTime TABFechaDesde { get; set; }
    }
}
