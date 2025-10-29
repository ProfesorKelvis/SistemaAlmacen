using SistemaAlmacen.Domain.Exceptions;

namespace SistemaAlmacen.Domain.Entities
{
    public class Municipio
    {

        //Properties
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;

        //Navigation properties
        public ICollection<Localidad> Localidades { get; private set; } = new List<Localidad>();

        //Constructor
        public Municipio(Guid? id, string nombre)
        {
            Id = id ?? Guid.CreateVersion7();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionReglaNegocio("El nombre del municipio no puede estar vacío.");
            }

            Nombre = nombre;
        }

    }
}
