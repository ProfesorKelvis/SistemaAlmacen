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

        public async Task<Resultado<AlmacenModel>> GuardarAlmacenAsync(AlmacenModel model, CancellationToken ct = default)
        {
            return model.Id == null || model.Id == Guid.Empty
                ? await CrearAlmacenAsync(model,  ct)
                : await ActualizarAlmacenAsync(model.Id.Value, model, ct);
        }

        public async Task<Resultado<AlmacenModel>> CrearAlmacenAsync(AlmacenModel model, CancellationToken ct = default)
        {
            try
            {
                // Generar un nuevo ID
                model.Id = Guid.CreateVersion7();

                var entity = model.ToEntity();

                var entityCreada = await repositorioAlmacen.AgregarAsync(entity, ct);

                return Resultado<AlmacenModel>.Success(entityCreada.ToModel());
            }
            catch (ExcepcionReglaNegocio ex)
            {
                return Resultado<AlmacenModel>.Failure($"Error al crear el almacén: {ex.Message}");
            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Resultado<AlmacenModel>.Failure($"Error al crear el almacén");
            }
        }

        public async Task<Resultado<AlmacenModel>> ActualizarAlmacenAsync(Guid id, AlmacenModel model, CancellationToken ct = default)
        {
            try
            {
                //Verificamos si el almacén existe

                var existe = await repositorioAlmacen.ExisteAsync(id);

                if (!existe) return Resultado<AlmacenModel>.Failure("Almacén no encontrado.");


                //Establecemos el Id a actualizar

                model.Id = id;

                var entidadActualizar = model.ToEntity();

                var entityActualizada = await repositorioAlmacen.ActualizarAsync(entidadActualizar, ct);

                return Resultado<AlmacenModel>.Success(entityActualizada.ToModel());

            }
            catch (ExcepcionReglaNegocio ex)
            {
                return Resultado<AlmacenModel>.Failure($"Error al actualizar el almacén: {ex.Message}");
            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Resultado<AlmacenModel>.Failure($"Error al actualizar el almacén. {ex.Message}");
            }
        }

        public async Task<Resultado<bool>> EliminarAlmacenAsync(Guid id, CancellationToken ct)
        {
            try
            {

                await repositorioAlmacen.EliminarAsync(id, ct);

                return Resultado<bool>.Success(true);

            }            
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Resultado<bool>.Failure($"Error al eliminar el almacén.");
            }
        }

        public async Task<Resultado<IReadOnlyList<AlmacenModel>>> ObtenerAlmacenesAsync(CancellationToken ct = default)
        {
            try
            {

                var almacenes = await repositorioAlmacen.ObtenerTodosAsync(ct);

                return Resultado<IReadOnlyList<AlmacenModel>>.Success(almacenes.ToModels());

            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Resultado<IReadOnlyList<AlmacenModel>>.Failure($"Error al obtener los almacenes");
            }
        }

        public async Task<Resultado<AlmacenModel>> ObtenerAlmacenPorIdAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var almacenEncontrado = await repositorioAlmacen.ObtenerPorIdAsync(id);

                if (almacenEncontrado == null) return Resultado<AlmacenModel>.Failure("Almacén no encontrado.");

                return Resultado<AlmacenModel>.Success(almacenEncontrado.ToModel());
            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Resultado<AlmacenModel>.Failure($"Error al obtener el almacen.");
            }
        }

        public async Task<Resultado<bool>> ExisteAlmacenAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var existe = await repositorioAlmacen.ExisteAsync(id);

                return existe ? Resultado<bool>.Success(true) : Resultado<bool>.Failure("Almacén no encontrado.");
            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Resultado<bool>.Failure("Almacén no encontrado.");
            }
        }

        public async Task<Resultado<int>> ContarAlmacenesAsync(CancellationToken ct = default)
        {
            try
            {
                var cantidad= await repositorioAlmacen.ContarAsync();

                return Resultado<int>.Success(cantidad);
            }
            catch (Exception ex)
            {
                //Registrar en el log de errores
                //ex.Message
                return Resultado<int>.Failure("Falla al contar los almacenes.");
            }
        }
    }
}
