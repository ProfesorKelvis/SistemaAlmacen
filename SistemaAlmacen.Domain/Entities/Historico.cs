using SistemaAlmacen.Domain.Aggregates.UbicacionAggregate;
using SistemaAlmacen.Domain.Enums;

namespace SistemaAlmacen.Domain.Entities
{
    public class Historico
    {
        //Propiedades
        public Guid Id { get; private set; }
        public int Cantidad { get; private set; }
        public DateTime Fecha { get; private set; }
        public EnumTipoTransaccion TipoTransaccion { get; private set; }

        //Propiedades de navegación
        public Guid ProductoId { get; private set; }
        public Producto Producto { get; private set; }

        public Guid UbicacionId { get; private set; }
        public Ubicacion Ubicacion { get; private set; }

        //Constructor
        public Historico(Guid? id, int cantidad, DateTime fecha, EnumTipoTransaccion tipoTransaccion, Guid productoId, Guid ubicacionId)
        {
            Id = id ?? Guid.CreateVersion7();
            Cantidad = cantidad;
            Fecha = fecha;
            TipoTransaccion = tipoTransaccion;
            ProductoId = productoId;
            UbicacionId = ubicacionId;
        }

    }
}
