using System;
using System.Data;
using WebFilm_DAL;

namespace WebFilm.Controls.User.Movie
{
    public partial class MovieDetailControl : System.Web.UI.UserControl
    {
        private string srcImg, srcVideo, idTheLoai;

        public string IdTheLoai { get => idTheLoai; set => idTheLoai = value; }
        public string SrcImg { get => srcImg; set => srcImg = value; }
        public string SrcVideo { get => srcVideo; set => srcVideo = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                this.Page.Title = ltrTenPhim_vi.Text + " - 123 Play";
            }
        }

        private void LoadData()
        {
            try
            {
                string request = Request["phim"];
                if (request == null) return;

                DataTable dt = new DataTable();
                dt = clsMovie.GetMovieDetail(int.Parse(request));

                if (dt.Rows.Count > 0)
                {
                    ltrTenPhim_en.Text = dt.Rows[0]["TenPhim_en"].ToString();
                    ltrTenPhim_vi.Text = dt.Rows[0]["TenPhim_vi"].ToString();
                    ltrLuotXem.Text = dt.Rows[0]["LuotXem"].ToString();
                    ltrLuotThich.Text = dt.Rows[0]["LuotThich"].ToString();
                    ltrMoTa.Text = dt.Rows[0]["MoTa"].ToString();
                    ltrThoiLuong.Text = dt.Rows[0]["ThoiLuong"].ToString();
                    ltrQuocGia.Text = dt.Rows[0]["QuocGia"].ToString();
                    ltrPhatHanh.Text = dt.Rows[0]["NamPhatHanh"].ToString();
                    SrcImg = "/Upload/Image/" + dt.Rows[0]["UrlHinh"].ToString();
                    SrcVideo += dt.Rows[0]["UrlPhim"].ToString();
                }
                else
                {
                    Response.Write("Không tìm thấy phim");
                    return;
                }

                dt = clsActor.GetActorsByMovie(int.Parse(request));
                if (dt.Rows.Count > 0)
                {
                    string actors = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        actors += row["TenDienVien"].ToString() + ", ";
                    }
                    actors = actors.Remove(actors.Length - 2); //Cut the last comma
                    ltrDienVien.Text = actors;
                }
                else
                {
                    ltrDienVien.Text = "Đang cập nhật";
                }

                dt = clsCategories.GetCategoriesByMovie(int.Parse(request));
                MovieListSwiperControl.IdTheLoai = dt.Rows[0]["ID"].ToString();
                if (dt.Rows.Count > 0)
                {
                    string categories = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        categories += row["TheLoai"].ToString() + ", ";
                    }
                    categories = categories.Remove(categories.Length - 2); //Cut the last comma
                    ltrTheLoai.Text = categories;
                }
                else
                {
                    ltrTheLoai.Text = "Đang cập nhật";
                }

                dt = clsDirector.GetDirectorsByMovie(int.Parse(request));
                if (dt.Rows.Count > 0)
                {
                    string directors = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        directors += row["TenDaoDien"].ToString() + ", ";
                    }
                    directors = directors.Remove(directors.Length - 2); //Cut the last comma
                    ltrDaoDien.Text = directors;
                }
                else
                {
                    ltrDaoDien.Text = "Đang cập nhật";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}