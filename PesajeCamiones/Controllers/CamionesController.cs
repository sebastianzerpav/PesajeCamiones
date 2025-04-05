using Microsoft.AspNetCore.Mvc;
using PesajeCamiones.Data.Models;
using PesajeCamiones.Services;

namespace PesajeCamiones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamionesController : ControllerBase
    {
        private readonly ICamionService camionService;

        public CamionesController(ICamionService camionService) {
            this.camionService = camionService; }

        [HttpPost("InsertarCamion")]
        public async Task<IActionResult> Insert([FromBody] Camion camion) {
            bool respuesta = await camionService.Insert(camion);
            if (respuesta) { return Ok("Camion insertado con éxito"); } else { return StatusCode(500, "No fue posible insertar el camión"); }
        }

        [HttpPut("ActualizarCamion/{placa}")]
        public async Task<IActionResult> Update([FromRoute]String placa, [FromBody] Camion camion)
        {
            bool respuesta = await camionService.Update(placa, camion);
            if (respuesta) { return Ok("Camion actualizado con éxito"); } else { return StatusCode(500, "No fue posible actualizar el camión"); }
        }

        [HttpDelete("EliminarCamion/{placa}")]
        public async Task<IActionResult> Delete([FromRoute] String placa)
        {
            bool respuesta = await camionService.Delete(placa);
            if (respuesta) { return Ok("Camion eliminado con éxito"); } else { return StatusCode(500, "No fue posible eliminar el camión"); }
        }


        [HttpGet("ConsultarCamion/{placa}")]
        public async Task<IActionResult> ConsultarCamion([FromRoute] String placa)
        {
            Camion? camion = await camionService.ConsultarCamion(placa);
            if (camion == null) { return NotFound("El camión no fue encontrado"); }
            else {
                return Ok(camion);
            }
        }

        [HttpGet("ListadoCamiones")]
        public async Task<IActionResult> listaCamiones(int id)
        {
            List<Camion> lista = await camionService.ListaCamiones();
            if (!lista.Any()) { return NotFound("No hay camiones registrados"); }
            else
            {
                return Ok(lista);
            }
        }
    }
}
