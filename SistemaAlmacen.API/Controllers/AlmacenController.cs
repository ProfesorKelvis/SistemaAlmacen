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
    }
}
