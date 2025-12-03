using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // <- necesario para JsonIgnore

namespace EstacionamientoACE_API.Models
{
    [Table("Plaza")]
    public class Plaza
    {
        [Column("Est_id")]
        public int EstId { get; set; }

        [Column("Plaza_id")]
        public int PlazaId { get; set; }

        [Column("Categoria_id")]
        public int? CategoriaId { get; set; }

        [Column("Plaza_Nombre")]
        [StringLength(50)]
        public string? PlazaNombre { get; set; }

        [Column("Plaza_Tipo")]
        [StringLength(50)]
        public string? PlazaTipo { get; set; }

        [Column("Plaza_Disponibilidad")]
        public bool PlazaDisponibilidad { get; set; }

        // 🔹 Navegaciones
        [ForeignKey(nameof(EstId))]
        [JsonIgnore] // <- evita el ciclo de serialización
        public virtual Estacionamiento? Estacionamiento { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        public virtual Categoria_Vehiculo? CategoriaVehiculo { get; set; }
    }
}
