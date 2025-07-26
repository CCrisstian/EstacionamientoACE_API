using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EstacionamientoACE_API.Models;

[Table("Estacionamiento")]
public partial class Estacionamiento
{
    [Key]
    [Column("Est_id")]
    public int EstId { get; set; }

    [Column("Dueño_Legajo")]
    public int? DueñoLegajo { get; set; }

    [Column("Est_nombre")]
    [StringLength(500)]
    [Unicode(false)]
    public string? EstNombre { get; set; }

    [Column("Est_provincia")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EstProvincia { get; set; }

    [Column("Est_localidad")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EstLocalidad { get; set; }

    [Column("Est_direccion")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EstDireccion { get; set; }

    [Column("Est_puntaje")]
    public double EstPuntaje { get; set; }

    [Column("Est_Dias_Atencion")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EstDiasAtencion { get; set; }

    [Column("Est_Hra_Atencion")]
    [StringLength(50)]
    [Unicode(false)]
    public string? EstHraAtencion { get; set; }

    [Column("Est_Dias_Feriado_Atencion")]
    public bool? EstDiasFeriadoAtencion { get; set; }

    [Column("Est_Fin_de_semana_Atencion")]
    public bool? EstFinDeSemanaAtencion { get; set; }

    [Column("Est_Hora_Fin_de_semana")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EstHoraFinDeSemana { get; set; }

    [Column("Est_Disponibilidad")]
    public bool EstDisponibilidad { get; set; }

    [Column("Est_Latitud")]
    public double? EstLatitud { get; set; }

    [Column("Est_Longitud")]
    public double? EstLongitud { get; set; }

    [InverseProperty("Est")]
    public virtual ICollection<Plaza> Plazas { get; set; } = new List<Plaza>();
}
