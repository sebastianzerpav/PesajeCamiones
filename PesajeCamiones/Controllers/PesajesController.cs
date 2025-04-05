using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PesajeCamiones.Data.DTOs;
using PesajeCamiones.Data.Models;
using PesajeCamiones.Services;

namespace PesajeCamiones.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PesajesController : ControllerBase
    {
        private readonly IPesajeService pesajeService;
        private readonly IReportesPesaje reportesPesaje;
        public PesajesController(IPesajeService pesajeService, IReportesPesaje reportesPesaje) {
            this.pesajeService = pesajeService;
            this.reportesPesaje = reportesPesaje;
        }

        [HttpPost("Insertar")]
        public async Task<ActionResult> Insert([FromBody] Pesaje pesaje) {
            bool resultado = await pesajeService.Insert(pesaje);
            if (!resultado)
            {
                return StatusCode(500, "Ocurrió un error al insertar el pesaje");
            }
            else
            {
                return Ok("Pesaje insertado exitosamente");
            }
        }

        [Route("Actualizar/{id}")]
        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] Pesaje pesaje) {
            bool resultado = await pesajeService.Update(id, pesaje);
            if (resultado)
            {
                return Ok("Pesaje actualizado correctamente");
            }
            else { return NotFound("No se encontró el pesaje que busca. Si está seguro de que existe el pesaje, verificar errores de app.");  }
        }

        [Route("Eliminar/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            bool resultado = await pesajeService.Delete(id);
            if (resultado)
            {
                return Ok("Pesaje eliminado correctamente");
            }
            else { return NotFound("No se encontró el pesaje que busca eliminar. Si está seguro de que existe el pesaje, verificar errores de app."); }
        }

        [Route("PesajePorPlaca/{placa}")]
        [HttpGet]
        public async Task<IActionResult> ListaPesajesPorPlaca([FromRoute]String placa) {
            List<PesajePorPlacaReporte> pesajes = await reportesPesaje.PesajePorPlaca(placa);
            if (!pesajes.Any()) { return NotFound("No se encontró ningún pesaje para la placa"); } else {
                return Ok(pesajes);
            }
        }
    }
}
