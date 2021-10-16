using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebFilm
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //// Add Routes.
            //RegisterCustomRoutes(RouteTable.Routes);
        }

        //private void RegisterCustomRoutes(RouteCollection routes)
        //{
        //    routes.MapPageRoute(
        //        "DefaultRoute",
        //        "{controller}",
        //        "~/Default.aspx"
        //    );
        //    routes.MapPageRoute(
        //        "XemPhimRoute",
        //        "{controller}/phim-{phim}",
        //        "~/Default.aspx"
        //    );
        //    routes.MapPageRoute(
        //        "TheLoaiRoute",
        //        "{controller}/the-loai-{theloai}",
        //        "~/Default.aspx"
        //    );
        //    routes.MapPageRoute(
        //        "QuocGiaRoute",
        //        "{controller}/quoc-gia-{quocgia}",
        //        "~/Default.aspx"
        //    );
        //    routes.MapPageRoute(
        //        "DefaultDashboard",
        //        "dashboard",
        //        "~/Dashboard.aspx"
        //    );
        //    routes.MapPageRoute(
        //        "DashboardRoute",
        //        "dashboard/{dashboard}",
        //        "~/Dashboard.aspx"
        //    );
        //}
    }
}