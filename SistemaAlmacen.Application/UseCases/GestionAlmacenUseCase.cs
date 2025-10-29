using SistemaAlmacen.Application.Contracts.Persistence;
using SistemaAlmacen.Application.Contracts.UseCases;
using SistemaAlmacen.Application.Models;
using SistemaAlmacen.Application.Result;
using SistemaAlmacen.Application.Utilities.Mappers;
using SistemaAlmacen.Domain.Exceptions;

namespace SistemaAlmacen.Application.UseCases
{
    public class GestionAlmacenUseCase : IGestionAlmacenUseCase
    {
        private readonly IRepositorioAlmacen repositorioAlmacen;

        public GestionAlmacenUseCase(IRepositorioAlmacen repositorioAlmacen)
        {
            this.repositorioAlmacen = repositorioAlmacen;
        }

        public async Task<Result<AlmacenModel>> GuardarAlmacenAsync(AlmacenModel model, CancellationToken ct = default)
        {
            return model.Id == null || model.Id == Guid.Empty
                ? await CrearAlmacenAsync(model,  ct)
                : await ActualizarAlmacenAsync(model.Id.Value, model, ct);
        }

        public async Task<Result<AlmacenModel>> CrearAlmacenAsync(AlmacenModel model, CancellationToken ct = default)
        {
            try
            {
                // Generar un nuevo ID
                model.Id = Guid.CreateVersion7();

                var entity = model.ToEntity();

                var entityCreada = await repositorioAlmacen.AgregarAsync(entity, ct);

                return Result<AlmacenModel>.Success(entityCreada.ToModel());
            }
            catch (ExcepcionReglaNegocio ex)
            {
                return Result<AlmacenModel>.Failure($"Error al crear el almacén: {ex.Message}");
            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Result<AlmacenModel>.Failure($"Error al crear el almacén");
            }
        }

        public async Task<Result<AlmacenModel>> ActualizarAlmacenAsync(Guid id, AlmacenModel model, CancellationToken ct = default)
        {
            try
            {
                //Verificamos si el almacén existe

                var existe = await repositorioAlmacen.ExisteAsync(id);

                if (!existe) return Result<AlmacenModel>.Failure("Almacén no encontrado.");


                //Establecemos el Id a actualizar

                model.Id = id;

                var entidadActualizar = model.ToEntity();

                var entityActualizada = await repositorioAlmacen.ActualizarAsync(entidadActualizar, ct);

                return Result<AlmacenModel>.Success(entityActualizada.ToModel());

            }
            catch (ExcepcionReglaNegocio ex)
            {
                return Result<AlmacenModel>.Failure($"Error al actualizar el almacén: {ex.Message}");
            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Result<AlmacenModel>.Failure($"Error al actualizar el almacén. {ex.Message}");
            }
        }

        public async Task<Result<bool>> EliminarAlmacenAsync(Guid id, CancellationToken ct)
        {
            try
            {

                await repositorioAlmacen.EliminarAsync(id, ct);

                return Result<bool>.Success(true);

            }            
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Result<bool>.Failure($"Error al eliminar el almacén.");
            }
        }

        public async Task<Result<IReadOnlyList<AlmacenModel>>> ObtenerAlmacenesAsync(CancellationToken ct = default)
        {
            try
            {

                var almacenes = await repositorioAlmacen.ObtenerTodosAsync(ct);

                return Result<IReadOnlyList<AlmacenModel>>.Success(almacenes.ToModels());

            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Result<IReadOnlyList<AlmacenModel>>.Failure($"Error al obtener los almacenes");
            }
        }

        public async Task<Result<AlmacenModel>> ObtenerAlmacenPorIdAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var almacenEncontrado = await repositorioAlmacen.ObtenerPorIdAsync(id);

                if (almacenEncontrado == null) return Result<AlmacenModel>.Failure("Almacén no encontrado.");

                return Result<AlmacenModel>.Success(almacenEncontrado.ToModel());
            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Result<AlmacenModel>.Failure($"Error al obtener el almacen.");
            }
        }

        public async Task<Result<bool>> ExisteAlmacenAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var existe = await repositorioAlmacen.ExisteAsync(id);

                return existe ? Result<bool>.Success(true) : Result<bool>.Failure("Almacén no encontrado.");
            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Result<bool>.Failure("Almacén no encontrado.");
            }
        }

        public async Task<Result<int>> ContarAlmacenesAsync(CancellationToken ct = default)
        {
            try
            {
                var cantidad= await repositorioAlmacen.ContarAsync();

                return Result<int>.Success(cantidad);
            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Result<int>.Failure("Falla al contar los almacenes.");
            }
        }
    }
}
