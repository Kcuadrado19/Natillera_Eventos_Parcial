using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Natillera_Eventos_Parcial.Models
{
    public class clsLogin
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public Login login { get; set; }
        private LoginRespuesta logRpta;
        private bool ValidarUsuario()
        {
            try
            {
                clsCypher cifrar = new clsCypher();
                Usuario usuario = dbSuper.Usuarios.FirstOrDefault(u => u.userName == login.Usuario);
                if (usuario == null)
                {
                    logRpta = new LoginRespuesta();
                    logRpta.Mensaje = "Usuario no existe";
                    return false;
                }
                byte[] arrBytesSalt = Convert.FromBase64String(usuario.Salt);
                string ClaveCifrada = cifrar.HashPassword(login.Clave, arrBytesSalt);
                login.Clave = ClaveCifrada;
                return true;
            }
            catch (Exception ex)
            {
                logRpta = new LoginRespuesta();
                logRpta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario())
            {
                //string token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                return from U in dbSuper.Set<Usuario>()
                       join UP in dbSuper.Set<Usuario_Perfil>()
                       on U.id equals UP.idUsuario
                       join P in dbSuper.Set<Perfil>()
                       on UP.idPerfil equals P.id
                       where U.userName == login.Usuario &&
                             U.Clave == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = U.userName,
                           Autenticado = true,
                           Token = token,
                           Perfil = P.Nombre,
                           PaginaInicio = P.PaginaNavegar,
                           Mensaje = "Usuario autenticado",
                       };
            }
            else
            {
                return null;
            }
        }
    }
}