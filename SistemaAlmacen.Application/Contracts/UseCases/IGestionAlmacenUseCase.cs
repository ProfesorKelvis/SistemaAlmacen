using SistemaAlmacen.Application.Models;
using SistemaAlmacen.Application.Result;

namespace SistemaAlmacen.Application.Contracts.UseCases
{
    public interface IGestionAlmacenUseCase
    {
        Task<Result<AlmacenModel>> GuardarAlmacenAsync(AlmacenModel model, CancellationToken ct = default);
        Task<Result<AlmacenModel>> CrearAlmacenAsync(AlmacenModel model, CancellationToken ct = default);
        Task<Result<AlmacenModel>> ActualizarAlmacenAsync(Guid id, AlmacenModel almacen, CancellationToken ct = default);
        Task<Result<bool>> EliminarAlmacenAsync(Guid id, CancellationToken ct = default);
        Task<Result<IReadOnlyList<AlmacenModel>>> ObtenerAlmacenesAsync(CancellationToken ct = default);
        Task<Result<AlmacenModel>> ObtenerAlmacenPorIdAsync(Guid id, CancellationToken ct = default);
        Task<Result<bool>> ExisteAlmacenAsync(Guid id, CancellationToken ct = default);
        Task<Result<int>> ContarAlmacenesAsync(CancellationToken ct = default);
    }
}