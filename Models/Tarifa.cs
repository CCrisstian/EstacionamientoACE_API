using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstacionamientoACE_API.Models
{
    [Table("Tarifa")]
    public class Tarifa
    {
        [Key]
        [Column("Tarifa_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TarifaId { get; set; }

        [Column("Est_id")]
        public int? EstId { get; set; }

        [Column("Tipos_Tarifa_Id")]
        public int? TiposTarifaId { get; set; }

        [Column("Categoria_id")]
        public int? CategoriaId { get; set; }

        [Column("Tarifa_Monto")]
        public double TarifaMonto { get; set; }

        [Column("Tarifa_Desde")]
        public DateTime TarifaDesde { get; set; }

        // 🔹 Propiedades de navegación
        [ForeignKey("EstId")]
        public virtual Estacionamiento? Estacionamiento { get; set; }

        [ForeignKey("TiposTarifaId")]
        public virtual Tipos_Tarifa? TipoTarifa { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria_Vehiculo? CategoriaVehiculo { get; set; }
    }
}
