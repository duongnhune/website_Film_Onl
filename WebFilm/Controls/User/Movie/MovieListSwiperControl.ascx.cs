using System;
using System.Data;
using WebFilm_DAL;

namespace WebFilm.Controls.User.Movie
{
    public partial class MovieListSwiperControl : System.Web.UI.UserControl
    {
        private string title;
        private string idTheLoai;

        public string Title { get => title; set => title = value; }
        public string IdTheLoai { get => idTheLoai; set => idTheLoai = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            DataTable dt = clsMovie.GetTop10ByCategory(int.Parse(IdTheLoai));

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
    }
}