using System;
using System.Collections.Generic;
using System.IO;

namespace WebFilm.Controls.User.Banner
{
    public partial class BannerControl : System.Web.UI.UserControl
    {
        private List<string> listBanner = new List<string>();

        public List<string> ListBanner { get { return listBanner; } set { listBanner = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadListBanner();
        }

        #region initial data

        public void LoadListBanner()
        {
            DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/Upload/Banner"));
            FileInfo[] directories = d.GetFiles("*.jpg");

            foreach (FileInfo item in directories)
            {
                listBanner.Add(item.Name);
            }

            Repeater1.DataSource = listBanner;
            Repeater1.DataBind();
        }

        #endregion
    }
}