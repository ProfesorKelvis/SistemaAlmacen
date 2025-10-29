using SistemaAlmacen.Domain.Exceptions;

namespace SistemaAlmacen.Domain.Aggregates.UbicacionAggregate
{
    public sealed record CapacidadUbicacionVO
    {
        public int CapacidadMaxima { get; init; }
        public int CantidadActual { get; init; }

        public CapacidadUbicacionVO(int capacidadMaxima, int cantidadActual = 0)
        {
            if (capacidadMaxima <= 0)
                throw new ExcepcionReglaNegocio("La capacidad máxima debe ser mayor a 0");

            if (cantidadActual < 0 || cantidadActual > capacidadMaxima)
                throw new ExcepcionReglaNegocio("La cantidad actual debe estar entre 0 y la capacidad máxima");

            CapacidadMaxima = capacidadMaxima;
            CantidadActual = cantidadActual;
        }

        public int EspacioDisponible => CapacidadMaxima - CantidadActual;
    }
}
