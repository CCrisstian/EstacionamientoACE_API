using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstacionamientoACE_API.Models
{
    [Table("Acepta_Metodo_De_Pago")]
    public class Acepta_Metodo_De_Pago
    {
        [Key, Column("Est_id", Order = 0)]
        public int EstId { get; set; }

        [Key, Column("Metodo_Pago_id", Order = 1)]
        public int MetodoPagoId { get; set; }

        [Column("AMP_Desde", TypeName = "date")]
        public DateTime? AmpDesde { get; set; }

        [Column("AMP_Hasta", TypeName = "date")]
        public DateTime? AmpHasta { get; set; }

        // 🔹 Propiedades de navegación

        [ForeignKey(nameof(EstId))]
        [InverseProperty(nameof(Estacionamiento.AceptaMetodosDePagos))]
        public virtual Estacionamiento? Estacionamiento { get; set; }

        [ForeignKey(nameof(MetodoPagoId))]
        [InverseProperty(nameof(Metodos_De_Pago.AceptaMetodosDePagos))]
        public virtual Metodos_De_Pago? MetodoDePago { get; set; } // ✅ nombre correcto
    }
}
