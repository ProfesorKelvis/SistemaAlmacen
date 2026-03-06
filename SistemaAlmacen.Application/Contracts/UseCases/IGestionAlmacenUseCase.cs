using SistemaAlmacen.Application.Models;
using SistemaAlmacen.Application.Result;

namespace SistemaAlmacen.Application.Contracts.UseCases
{
    public interface IGestionAlmacenUseCase
    {
        Task<Resultado<AlmacenModel>> GuardarAlmacenAsync(AlmacenModel model, CancellationToken ct = default);
        Task<Resultado<AlmacenModel>> CrearAlmacenAsync(AlmacenModel model, CancellationToken ct = default);
        Task<Resultado<AlmacenModel>> ActualizarAlmacenAsync(Guid id, AlmacenModel almacen, CancellationToken ct = default);
        Task<Resultado<bool>> EliminarAlmacenAsync(Guid id, CancellationToken ct = default);
        Task<Resultado<IReadOnlyList<AlmacenModel>>> ObtenerAlmacenesAsync(CancellationToken ct = default);
        Task<Resultado<AlmacenModel>> ObtenerAlmacenPorIdAsync(Guid id, CancellationToken ct = default);
        Task<Resultado<bool>> ExisteAlmacenAsync(Guid id, CancellationToken ct = default);
        Task<Resultado<int>> ContarAlmacenesAsync(CancellationToken ct = default);
    }
}