using SistemaAlmacen.API.DTOs.Almacen;
using SistemaAlmacen.Application.Models;

namespace SistemaAlmacen.API.Utilities.Mappers
{
    public static class AlmacenMapperExtensions
    {
        public static AlmacenDTO ToDTO(this AlmacenModel almacen)
        {
            return new AlmacenDTO
            {
                Id = almacen.Id.ToString(),
                Codigo = almacen.Codigo,
                Nombre = almacen.Nombre,
                Direccion = almacen.Direccion,
                Telefono = almacen.Telefono,
                LocalidadId = almacen.LocalidadId.ToString()
            };
        }
        public static AlmacenModel ToModel(this AlmacenDTO almacenDTO)
        {
            return new AlmacenModel
            {
                Id = string.IsNullOrWhiteSpace(almacenDTO.Id) ? null : Guid.Parse(almacenDTO.Id),
                Codigo = almacenDTO.Codigo,
                Nombre = almacenDTO.Nombre,
                Direccion = almacenDTO.Direccion,
                Telefono = almacenDTO.Telefono,
                LocalidadId = Guid.Parse(almacenDTO.LocalidadId)
            };
        }

        public static IReadOnlyList<AlmacenModel> ToModels(this IReadOnlyList<AlmacenDTO> dtos)
            => [.. dtos.Select(e => e.ToModel())];

        public static IReadOnlyList<AlmacenDTO> ToDTOs(this IReadOnlyList<AlmacenModel> models)
            => [.. models.Select(d => d.ToDTO())];
    }

}
