using System;
using System.Web.UI;

namespace WebFilm.Controls.User
{
    public partial class userControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (Request["index"])
                {
                    case "dang-ky":
                        plhLoadContent.Controls.Add(LoadControl("~/Controls/User/Account/RegisterControl.ascx"));
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "registerModal", "showRegisterModal()", true);
                        break;

                    case "dang-nhap":
                        plhLoadContent.Controls.Add(LoadControl("~/Controls/User/Account/LoginControl.ascx"));
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "loginModal", "showLoginModal()", true);
                        break;

                    case "phim-le":
                        plhLoadContent.Controls.Add(LoadControl("~/Controls/User/Movie/MovieListControl.ascx"));
                        break;

                    case "the-loai":
                        plhLoadContent.Controls.Add(LoadControl("~/Controls/User/Movie/MovieListByCategoryControl.ascx"));
                        break;

                    case "quoc-gia":
                        plhLoadContent.Controls.Add(LoadControl("~/Controls/User/Movie/MovieListByNationControl.ascx"));
                        break;

                    case "phim":
                        plhLoadContent.Controls.Add(LoadControl("~/Controls/User/Movie/MovieDetailControl.ascx"));
                        break;

                    default:
                        plhLoadContent.Controls.Add(LoadControl("~/Controls/User/Home/HomeControl.ascx"));
                        break;
                }
            }
        }
    }
}