using SistemaAlmacen.Application.Models;
using SistemaAlmacen.Domain.Entities;
using SistemaAlmacen.Domain.ValueObjects;

namespace SistemaAlmacen.Application.Utilities.Mappers
{
    public static class AlmacenMapperExtensions
    {
        public static AlmacenModel ToModel(this Almacen entity)
            => new()
            {
                Id = entity.Id,
                Codigo = entity.Codigo.Valor,
                Nombre = entity.Nombre,
                Direccion = entity.Direccion,
                Telefono = entity.Telefono.Valor,
                LocalidadId = entity.LocalidadId
            };

        public static Almacen ToEntity(this AlmacenModel model)
        {
            return new Almacen(
                id: model.Id ?? Guid.Empty,
                codigo: new CodigoVO(model.Codigo),
                nombre: model.Nombre,
                direccion: model.Direccion,
                telefono: new TelefonoVO(model.Telefono),
                localidadId: model.LocalidadId
            );
        }

        public static IReadOnlyList<AlmacenModel> ToModels(this IReadOnlyList<Almacen> entities)
            => [.. entities.Select(e => e.ToModel())];

        public static IReadOnlyList<Almacen> ToEntities(this IReadOnlyList<AlmacenModel> models)
            => [.. models.Select(d => d.ToEntity())];

        public static IReadOnlyList<AlmacenModel> ToModelsClasico(this IReadOnlyList<Almacen> entities)
                => entities.Select(e => e.ToModel()).ToList();

        public static IReadOnlyList<Almacen> ToEntitiesClasico(this IReadOnlyList<AlmacenModel> models)
            => models.Select(d => d.ToEntity()).ToList();
    }
}
