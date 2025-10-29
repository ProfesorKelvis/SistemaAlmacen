namespace SistemaAlmacen.Application.Contracts.Persistence
{
    public interface IRepositorio<T> where T : class
    {
        //Operaciones de escritura
        Task<T> AgregarAsync(T entidad, CancellationToken ct = default);
        Task<T> ActualizarAsync(T entidad, CancellationToken ct = default);

        //Operaciones de lectura
        Task<T?> ObtenerPorIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<T>> ObtenerTodosAsync(CancellationToken ct = default);
        Task EliminarAsync(Guid id, CancellationToken ct = default);
        Task<int> ContarAsync(CancellationToken ct = default);
        Task<bool> ExisteAsync(Guid id, CancellationToken ct = default);
       

    }
}
