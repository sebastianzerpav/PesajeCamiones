using Microsoft.AspNetCore.Mvc;
using PesajeCamiones.Data.DTOs;
using PesajeCamiones.Services;

namespace PesajeCamiones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotosPesajeController : ControllerBase
    {
        private readonly IFotosPesajeService fotosPesajeService;

        public FotosPesajeController(IFotosPesajeService fotosPesajeService) { 
            this.fotosPesajeService = fotosPesajeService;
        }

        [HttpPost("SubirFicheros/{idPesaje}")]
        [Consumes("multipart/form-data")]
        public IActionResult Upload([FromRoute] int idPesaje, [FromForm] FotoDto fotoDto) {
            bool respuesta = fotosPesajeService.Upload(idPesaje, fotoDto.Fichero!);
            if (respuesta) { return Ok("Fichero insertado con éxito"); } else { return BadRequest("No se envió un archivo"); }
        }

        [HttpPut("ActualizarFicheros/{idFichero}")]
        [Consumes("multipart/form-data")]
        public IActionResult Update([FromRoute] int idFichero, [FromForm] FotoDto fotoDto) {
            bool respuesta = fotosPesajeService.Update(idFichero, fotoDto.Fichero!);
            if (respuesta) { return Ok("Fichero actualizado"); } else { return BadRequest("No se pudo actualizar la foto"); }
        }

        [HttpDelete("EliminarFicheros/{idFichero}")]
        public IActionResult Delete([FromRoute] int idFichero) {
            bool respuesta = fotosPesajeService.Delete (idFichero);
            if (respuesta) { return Ok("Fichero eliminado exitosamente"); } else { return BadRequest("No se pudo eliminar la foto"); }
        }
    }
}
