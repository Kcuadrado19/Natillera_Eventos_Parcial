using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Natillera_Eventos_Parcial.Clases;

namespace Natillera_Eventos_Parcial.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult IniciarSesion([FromBody] Natillera_Eventos_Parcial.Models.Login login)


        {
            clsLogin logica = new clsLogin();
            logica.login = login;

            var respuesta = logica.Ingresar();
            if (respuesta != null && respuesta.Any())
                return Ok(respuesta);

            return Unauthorized();
        }
    }
}