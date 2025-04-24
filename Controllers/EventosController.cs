using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Natillera_Eventos_Parcial.Clases;
using Natillera_Eventos_Parcial.Models;

namespace Natillera_Eventos_Parcial.Controllers
{
    [Authorize]
    [RoutePrefix("api/eventos")]
    public class EventosController : ApiController
    {
        private clsEventos logica = new clsEventos();

        [HttpPost]
        [Route("crear")]
        public IHttpActionResult Crear([FromBody] Evento evento)
        {
            if (logica.Crear(evento)) return Ok("Evento creado exitosamente.");
            return BadRequest("Error al crear el evento.");
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar(string tipo = null, string nombre = null, DateTime? fecha = null)
        {
            var resultado = logica.Listar(tipo, nombre, fecha);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("actualizar")]
        public IHttpActionResult Actualizar([FromBody] Evento evento)
        {
            if (logica.Actualizar(evento)) return Ok("Evento actualizado.");
            return BadRequest("Error al actualizar el evento.");
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IHttpActionResult Eliminar(int id)
        {
            if (logica.Eliminar(id))
                return Ok("Evento eliminado.");

            return BadRequest("Error al eliminar el evento.");
        }

    }
}