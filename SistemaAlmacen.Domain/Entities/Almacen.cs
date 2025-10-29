using SistemaAlmacen.Domain.Aggregates.UbicacionAggregate;
using SistemaAlmacen.Domain.Exceptions;
using SistemaAlmacen.Domain.ValueObjects;

namespace SistemaAlmacen.Domain.Entities
{
    public class Almacen:BaseEntity
    {
        //Propiedades        
        public CodigoVO Codigo { get; private set; } = null!;
        public string Nombre { get; private set; } = null!;
        public string Direccion { get; private set; } = null!;
        public TelefonoVO Telefono { get; private set; } = null!;


        //Navigation properties
        public Guid LocalidadId { get; private set; }
        //public Localidad Localidad { get; private set; } = null!;

        //public ICollection<Ubicacion> Ubicaciones { get; private set; } = new List<Ubicacion>();


        //Constructores
        private Almacen() { }

        public Almacen(Guid id, CodigoVO codigo, string nombre,
                        string direccion, TelefonoVO telefono, Guid localidadId)
        {           

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionReglaNegocio("El nombre del almacén no puede estar vacío.", 1003);
            }

            if (string.IsNullOrWhiteSpace(direccion))
            {
                throw new ExcepcionReglaNegocio("La dirección del almacén no puede estar vacía.", 1004);
            }

            if (codigo == null)
            {
                throw new ExcepcionReglaNegocio("El código del almacén no puede ser nulo.", 1005);
            }

            if (telefono == null)
            {
                throw new ExcepcionReglaNegocio("El teléfono del almacén no puede ser nulo.", 1006);
            }

            if (id == Guid.Empty)
            {
                throw new ExcepcionReglaNegocio("El ID del almacén no puede ser vacío.", 1007);
            }

            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            LocalidadId = localidadId;
        }
    }
}
