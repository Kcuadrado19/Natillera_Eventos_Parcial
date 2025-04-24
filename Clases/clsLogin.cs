using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Natillera_Eventos_Parcial.Models;




namespace Natillera_Eventos_Parcial.Clases
{
    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }
        public EVENTOS_NATILLERAEntities dbeventos_natillera = new EVENTOS_NATILLERAEntities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }
        public bool ValidarUsuario()
        {
            try
            {
                clsCypher cifrar = new clsCypher();
                Administrador admon = dbeventos_natillera.Administradors.FirstOrDefault(u => u.Usuario == login.Usuario);

                if (admon == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario no existe";
                    return false;
                }

                byte[] arrBytesSalt = Convert.FromBase64String(admon.Salt);
                string ClaveCifrada = cifrar.HashPassword(login.Clave, arrBytesSalt);

                if (admon.Clave != ClaveCifrada)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "La clave no coincide";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = "Error: " + ex.Message;
                return false;
            }
        }

        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario())
            {
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);

                return from A in dbeventos_natillera.Administradors
                       where A.Usuario == login.Usuario
                       select new LoginRespuesta
                       {
                           Usuario = A.Usuario,
                           Autenticado = true,
                           Token = token,
                           Mensaje = "Usuario autenticado",
                           Perfil = "Administrador", // aún fijo
                           PaginaInicio = "/admin/eventos"
                       };
            }
            else
            {
                return new List<LoginRespuesta> { loginRespuesta }.AsQueryable();
            }
        }



    }


}