using System.Web;
using System.Web.Mvc;

namespace Natillera_Eventos_Parcial
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
