using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natillera_Eventos_Parcial.Models
{
    public class LoginRespuesta
    {
        public string Usuario { get; set; }
        public string Perfil { get; set; }
        public string PaginaInicio { get; set; }
        public bool Autenticado { get; set; }
        public string Token { get; set; }
        public string Mensaje { get; set; }

    }
}