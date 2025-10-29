using SistemaAlmacen.Domain.Exceptions;

namespace SistemaAlmacen.Domain.Entities
{
    public class Categoria
    {
        //Properties
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;

        
        //Navigation properties
        public ICollection<Producto> Productos { get; private set; } = new List<Producto>();


        //Constructor
        public Categoria(Guid? id, string nombre)
        {
            Id = id ?? Guid.CreateVersion7();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionReglaNegocio("El nombre de la categoría no puede estar vacío.");
            }
            
            Nombre = nombre;
        }

        //Methods


    }
}
