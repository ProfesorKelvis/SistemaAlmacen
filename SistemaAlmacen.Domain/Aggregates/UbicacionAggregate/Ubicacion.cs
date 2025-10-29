using SistemaAlmacen.Domain.Entities;
using SistemaAlmacen.Domain.Enums;
using SistemaAlmacen.Domain.Exceptions;
using SistemaAlmacen.Domain.ValueObjects;

namespace SistemaAlmacen.Domain.Aggregates.UbicacionAggregate
{

    public sealed class Ubicacion
    {
        // Propiedades
        public Guid Id { get; private set; }
        public CodigoVO Codigo { get; private set; } = null!;
        public string Nombre { get; private set; } = null!;
        public string Descripcion { get; private set; } = null!;
        public CapacidadUbicacionVO Capacidad { get; private set; }

        // Propiedades de navegación
        public Guid? ProductoPermitidoId { get; private set; } = null;
        public Producto ProductoPermitido { get; private set; } = null!;
        public ICollection<Historico> Historicos { get; private set; } = new List<Historico>();

        // Constructor
        public Ubicacion(Guid? id, CodigoVO codigo, string nombre, string descripcion,
                         CapacidadUbicacionVO capacidad, Guid? productoPermitidoId = null)
        {
            Id = id ?? Guid.CreateVersion7();
           
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ExcepcionReglaNegocio("El nombre de la ubicación no puede estar vacío.");

            Nombre = nombre;            
            Codigo = codigo;            
            Descripcion = descripcion;
            Capacidad = capacidad;
            ProductoPermitidoId = productoPermitidoId;
        }

        // Métodos

        /// <summary>
        /// Implementación de Reglas: RB02 y RB03
        /// </summary>
        public void RegistrarEntrada(Producto producto, int cantidad)
        {
            //Si no hay producto permitido asignado, se asigna el del primer ingreso
            if (ProductoPermitidoId is null)
                ProductoPermitidoId = producto.Id;

            if (producto.Id != ProductoPermitidoId)
                throw new ExcepcionReglaNegocio($"La ubicación solo permite productos: '{ProductoPermitido.Codigo} {ProductoPermitido.Nombre}'");

            var nuevaCantidad = Capacidad.CantidadActual + cantidad;
            if (nuevaCantidad > Capacidad.CapacidadMaxima)
                throw new ExcepcionReglaNegocio("Excede la capacidad máxima de esta ubicación");

            Capacidad = new CapacidadUbicacionVO(Capacidad.CapacidadMaxima, nuevaCantidad);

            var historico = new Historico(
                id: null,
                cantidad: cantidad,
                fecha: DateTime.UtcNow,
                tipoTransaccion: EnumTipoTransaccion.Entrada,
                productoId: producto.Id,
                ubicacionId: this.Id
            );
            
            Historicos.Add(historico);

        }

        public void RegistrarSalida(Producto producto, int cantidad)
        {
            var nuevaCantidad = Capacidad.CantidadActual - cantidad;
            if (nuevaCantidad < 0)
                throw new ExcepcionReglaNegocio("No hay suficiente stock");

            Capacidad = new CapacidadUbicacionVO(Capacidad.CapacidadMaxima, nuevaCantidad);
        }

        public void CambiarProductoPermitido(Guid nuevoProductoId)
        {
            if (Capacidad.CantidadActual > 0)
                throw new ExcepcionReglaNegocio("No se puede cambiar el producto con stock existente");

            ProductoPermitidoId = nuevoProductoId;
        }

        public void CambiarCapacidadMaxima(int nuevaCapacidad)
        {
            if (nuevaCapacidad <= 0)
                throw new ExcepcionReglaNegocio("La capacidad máxima debe ser mayor a 0");

            if (Capacidad.CantidadActual > nuevaCapacidad)
                throw new ExcepcionReglaNegocio("La nueva capacidad es menor que la cantidad actual");

            Capacidad = new CapacidadUbicacionVO(nuevaCapacidad, Capacidad.CantidadActual);
        }
    }

}
