using Microsoft.AspNetCore.Mvc;
using SistemaAlmacen.API.DTOs.Almacen;
using SistemaAlmacen.API.Utilities.Mappers;
using SistemaAlmacen.Application.Contracts.UseCases;

namespace SistemaAlmacen.API.Controllers
{
    [ApiController]
    [Route("api/almacenes")]
    public class AlmacenController : ControllerBase
    {
        private readonly IGestionAlmacenUseCase gestionAlmacenUseCase;

        public AlmacenController(IGestionAlmacenUseCase gestionAlmacenUseCase)
        {
            this.gestionAlmacenUseCase = gestionAlmacenUseCase;
        }

        [HttpPost("guardar")]
        public async Task<IActionResult> GuardarAlmacen([FromBody] AlmacenDTO almacenDTO)
        {
            // Validamos el modelo recibido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await gestionAlmacenUseCase.GuardarAlmacenAsync(almacenDTO.ToModel());

            if (resultado.IsSuccess)
            {                
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [HttpGet("obtener")]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await gestionAlmacenUseCase.ObtenerAlmacenesAsync();

            if (resultado.IsSuccess)
            {
                return Ok(resultado);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("obtener:{id}")]
        public async Task<IActionResult> Obtener(string id)
        {
            var GuidId = Guid.TryParse(id, out var guid) ? guid : Guid.Empty;
            if (GuidId == Guid.Empty)
            {
                return BadRequest("ID inválido.");
            }

            var resultado = await gestionAlmacenUseCase.ObtenerAlmacenPorIdAsync(GuidId);

            if (resultado.IsSuccess)
            {
                return Ok(resultado);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("eliminar:{id}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var GuidId = Guid.TryParse(id, out var guid) ? guid : Guid.Empty;
            if (GuidId == Guid.Empty)
            {
                return BadRequest("ID inválido.");
            }

            var resultado = await gestionAlmacenUseCase.EliminarAlmacenAsync(GuidId);

            if (resultado.IsSuccess)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest($"No se pudo eliminar el id:{id}");
            }
        }
    }
}
