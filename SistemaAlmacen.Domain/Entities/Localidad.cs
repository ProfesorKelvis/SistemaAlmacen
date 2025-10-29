using SistemaAlmacen.Domain.Exceptions;

namespace SistemaAlmacen.Domain.Entities
{
    public class Localidad
    {
        //Properties
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;

        //Navigation properties
        
        public ICollection<Almacen> Almacenes { get; private set; } = new List<Almacen>();

        //Constructor
        public Localidad(Guid? id, string nombre)
        {
            Id = id ?? Guid.CreateVersion7();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionReglaNegocio("El nombre de la localidad no puede estar vacío.");
            }

            Nombre = nombre;
        }
    }
}
