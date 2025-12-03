using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstacionamientoACE_API.Models
{
    [Table("Categoria_Vehiculo")]
    public class Categoria_Vehiculo
    {
        [Key]
        [Column("Categoria_id")]
        public int CategoriaId { get; set; }

        [Column("Categoria_descripcion")]
        public string? CategoriaDescripcion { get; set; }

        // 🔹 Relación inversa
        public ICollection<Plaza>? Plazas { get; set; }
    }
}
