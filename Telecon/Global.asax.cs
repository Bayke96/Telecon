using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace Telecon
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MvcHandler.DisableMvcResponseHeader = true;
            
        }
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version"); //alternative to above solution
            Response.Headers.Remove("X-AspNetMvc-Version"); //alternative to above solution
        }
    }
}
