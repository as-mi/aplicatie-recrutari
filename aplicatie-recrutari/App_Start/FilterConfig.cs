using System.Web;
using System.Web.Mvc;

namespace aplicatie_recrutari {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
