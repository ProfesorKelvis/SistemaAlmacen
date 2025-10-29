using SistemaAlmacen.Domain.Aggregates.UbicacionAggregate;
using SistemaAlmacen.Domain.Exceptions;
using SistemaAlmacen.Domain.ValueObjects;

namespace SistemaAlmacen.Domain.Entities
{
    public class Producto
    {
        public Guid Id { get; private set; }
        public CodigoVO Codigo { get; private set; } = null!;
        public string Nombre { get; private set; } = null!;
        public string Descripcion { get; private set; } = null!;


        //Propiedades de navegación
        public Guid CategoriaId { get; private set; }
        public Categoria Categoria { get; private set; } = null!;

        public ICollection<Ubicacion> Ubicaciones { get; private set; } = new List<Ubicacion>();
        public ICollection<Historico> Historicos { get; private set; } = new List<Historico>();


        //Constructor
        public Producto(Guid? id, CodigoVO codigo, string nombre, string descripcion, Guid categoriaId)
        {
            Id = id ?? Guid.CreateVersion7();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionReglaNegocio("El nombre del producto no puede estar vacío.");
            }

            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            CategoriaId = categoriaId;
        }

    }
}
