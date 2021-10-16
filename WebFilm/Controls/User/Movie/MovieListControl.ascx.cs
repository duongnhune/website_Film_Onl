using System;
using WebFilm_DAL;

namespace WebFilm.Controls.User.Movie
{
    public partial class MovieListControl : System.Web.UI.UserControl
    {
        private string title;

        public string Title { get => title; set => title = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
            this.Page.Title = "Phim Lẻ - 123 Play";
        }

        private void LoadData()
        {
            rptMovieList.DataSource = clsMovie.GetMoviesByCatalogue(2); //List Phim lẻ (idDanhMuc=2)
            rptMovieList.DataBind();

            if (clsMovie.GetMoviesByCatalogue(2).Rows.Count > 0)
                Title = "Lẻ";
            else Title = "đang cập nhật dữ liệu";
        }
    }
}