using System;

namespace WebFilm.Controls.Admin
{
    public partial class adminControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (Request["dashboard"])
                {
                    case "tai-khoan":
                        MultiView1.ActiveViewIndex = 0;
                        break;

                    case "danh-muc":
                        MultiView1.ActiveViewIndex = 1;
                        break;

                    case "phim":
                        MultiView1.ActiveViewIndex = 2;
                        break;

                    case "the-loai":
                        MultiView1.ActiveViewIndex = 3;
                        break;

                    case "quoc-gia":
                        MultiView1.ActiveViewIndex = 4;
                        break;

                    case "dao-dien":
                        MultiView1.ActiveViewIndex = 5;
                        break;

                    case "dien-vien":
                        MultiView1.ActiveViewIndex = 6;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}