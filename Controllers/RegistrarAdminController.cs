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
    [RoutePrefix("api/admin")]
    public class RegistrarAdminController : ApiController
    {
        [HttpPost]
        [Route("registrar")]
        public IHttpActionResult Registrar([FromBody] AdminRegistroRequest request)
        {
            try
            {
                var db = new EVENTOS_NATILLERAEntities();
                var cifrador = new clsCypher();

                byte[] salt = clsCypher.GenerateSalt();
                string claveCifrada = cifrador.HashPassword(request.Clave, salt);
                string saltBase64 = Convert.ToBase64String(salt);

                var nuevo = new Administrador
                {
                    Documento = request.Documento,
                    NombreCompleto = request.NombreCompleto,
                    Usuario = request.Usuario,
                    Clave = claveCifrada,
                    Salt = saltBase64
                };

                db.Administradors.Add(nuevo);
                db.SaveChanges();

                return Ok("Administrador registrado correctamente.");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errores = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"Campo: {e.PropertyName}, Error: {e.ErrorMessage}")
                    .ToList();

                return BadRequest("Errores de validación:\n" + string.Join("\n", errores));
            }
            catch (Exception ex)
            {
                return BadRequest("Error general: " + ex.Message);
            }
        }


    }

    public class AdminRegistroRequest
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }
    }

}