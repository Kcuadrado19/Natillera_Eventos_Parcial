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
                login.Clave = ClaveCifrada;
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {
                Administrador admon = dbeventos_natillera.Administradors.FirstOrDefault(u => u.Usuario == login.Usuario && u.Clave == login.Clave);
                if (admon == null)
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
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario() && ValidarClave())
            {
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);

                return from A in dbeventos_natillera.Set<Administrador>()
                       join E in dbeventos_natillera.Set<Evento>()
                       on A.idAministrador equals E.idAdministrador
                       where A.Usuario == login.Usuario && A.Clave == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = A.Usuario,
                           Autenticado = true,
                           Token = token,
                           Mensaje = "Usuario autenticado",
                           Perfil = "Administrador", // valor fijo ya que no hay tabla Perfil
                           PaginaInicio = "/admin/eventos" // ejemplo estático
                       };
            }
            else
            {
                List<LoginRespuesta> List = new List<LoginRespuesta>();
                List.Add(loginRespuesta);
                return List.AsQueryable();
            }
        }
    }
}