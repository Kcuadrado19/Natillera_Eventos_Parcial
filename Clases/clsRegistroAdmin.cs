using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Natillera_Eventos_Parcial.Models;

namespace Natillera_Eventos_Parcial.Clases
{
    public class clsRegistroAdmin
    {
        public static void RegistrarNuevoAdmin(string userName, string clavePlano)
        {
            var db = new EVENTOS_NATILLERAEntities();
            var cifrador = new clsCypher();

            byte[] salt = clsCypher.GenerateSalt();
            string claveCifrada = cifrador.HashPassword(clavePlano, salt);
            string saltBase64 = Convert.ToBase64String(salt);

            var nuevo = new Administrador
            {
                Documento = "12345678", // Verifica longitud y obligatoriedad
                NombreCompleto = "Laura Martínez", // Verifica longitud y obligatoriedad
                Usuario = userName,
                Clave = claveCifrada,
                Salt = saltBase64
            };

            db.Administradors.Add(nuevo);

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("❌ Error: " + validationError.PropertyName + " -> " + validationError.ErrorMessage);
                    }
                }
                throw; // Re-lanzar la excepción para que siga mostrando el error
            }
        }

    }
}