using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EstacionamientoACE_API.Models;

[PrimaryKey("EstId", "PlazaId")]
[Table("Plaza")]
public partial class Plaza
{
    [Key]
    [Column("Est_id")]
    public int EstId { get; set; }

    [Key]
    [Column("Plaza_id")]
    public int PlazaId { get; set; }

    [Column("Categoria_id")]
    public int? CategoriaId { get; set; }

    [Column("Plaza_Nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PlazaNombre { get; set; }

    [Column("Plaza_Tipo")]
    [StringLength(50)]
    [Unicode(false)]
    public string PlazaTipo { get; set; } = null!;

    [Column("Plaza_Disponibilidad")]
    public bool PlazaDisponibilidad { get; set; }

    [ForeignKey("EstId")]
    [InverseProperty("Plazas")]
    public virtual Estacionamiento Est { get; set; } = null!;
}
