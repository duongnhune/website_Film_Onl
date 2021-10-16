using System;
using WebFilm_DAL;

namespace WebFilm.Controls.User.Movie
{
    public partial class MovieListByCategoryControl : System.Web.UI.UserControl
    {
        private string title;

        public string Title { get => title; set => title = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
            this.Page.Title = "Phim " + Title + " - 123 Play";
        }

        private void LoadData()
        {
            string request = Request["the-loai"];
            if (request == null) return;

            rptMovieList.DataSource = clsMovie.GetMoviesByCategory(int.Parse(request));
            rptMovieList.DataBind();

            if (clsMovie.GetMoviesByCategory(int.Parse(request)).Rows.Count > 0)
                Title = (string)clsMovie.GetMoviesByCategory(int.Parse(request)).Rows[0]["TheLoai"];
            else Title = "đang được cập nhật..";
        }
    }
}