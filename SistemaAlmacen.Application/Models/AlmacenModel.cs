namespace SistemaAlmacen.Application.Models
{
    public class AlmacenModel
    {
        public Guid? Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public Guid LocalidadId { get; set; }

    }
}
