using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EstacionamientoACE_API.Models
{
    [Table("Metodos_De_Pago")]
    public class Metodos_De_Pago
    {
        [Key]
        [Column("Metodo_pago_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MetodoPagoId { get; set; }

        [Column("Metodo_pago_descripcion")]
        [StringLength(1000)]
        [Unicode(false)]
        public string? MetodoPagoDescripcion { get; set; }

        // 🔹 Relación inversa con Acepta_Metodo_De_Pago
        [InverseProperty(nameof(Acepta_Metodo_De_Pago.MetodoDePago))]
        public virtual ICollection<Acepta_Metodo_De_Pago> AceptaMetodosDePagos { get; set; } = new List<Acepta_Metodo_De_Pago>();
    }
}
