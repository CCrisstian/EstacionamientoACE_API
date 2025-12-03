using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstacionamientoACE_API.Models
{
    [Table("Tipos_Tarifa")]
    public class Tipos_Tarifa
    {
        [Key]
        [Column("Tipos_tarifa_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TiposTarifaId { get; set; } // Corregido

        [Column("Tipos_tarifa_descripcion")]
        [StringLength(200)]
        public string? TiposTarifaDescripcion { get; set; } // Corregido

        // Propiedad de navegación
        public virtual ICollection<Tarifa>? Tarifas { get; set; }
    }
}