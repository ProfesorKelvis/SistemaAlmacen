using System.ComponentModel.DataAnnotations;

namespace SistemaAlmacen.API.DTOs.Almacen
{
    public class AlmacenDTO
    {
        public string? Id { get; set; }=null!;

        [Required(ErrorMessage = "{0} es obligatorio")]
        //[StringLength(5,ErrorMessage ="{0} debe ser de 5 caracteres")]
        public string Codigo { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        [Required]
        [StringLength(300)]
        public string Direccion { get; set; } = null!;
        [Required]
        [StringLength(10)]
        public string Telefono { get; set; } = null!;
        public string LocalidadId { get; set; } = null!;

    }
}
