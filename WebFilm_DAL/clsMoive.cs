using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebFilm_DAL
{
    public class clsMovie
    {
        public static DataTable GetMovie(int idPhim)
        {
            SqlConnection conn = null;
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                };
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_Phim WHERE ID=@idPhim AND DaXoa=0", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@idPhim", idPhim);

                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

            return ds.Tables[0];
        }

        public static DataTable GetMovies()
        {
            SqlConnection conn = null;
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                };
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("up_MovieList", conn) { CommandType = CommandType.StoredProcedure };

                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

            return ds.Tables[0];
        }

        public static DataTable GetMoviesByCatalogue(int idDanhMuc)
        {
            SqlConnection conn = null;
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                };
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_Phim WHERE DanhMuc=@idDanhMuc AND DaXoa=0 ORDER BY TgianCapNhat DESC, NamPhatHanh DESC", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@idDanhMuc", idDanhMuc);

                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return ds.Tables[0];
        }

        public static DataTable GetMoviesByCategory(int idTheLoai)
        {
            SqlConnection conn = null;
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                };
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("up_MovieListByCategory", conn) { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.AddWithValue("@idTheLoai", idTheLoai);

                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

            return ds.Tables[0];
        }

        public static DataTable GetTop10ByCategory(int idTheLoai)
        {
            SqlConnection conn = null;
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("up_MovieListTop10ByCategory", conn) { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.AddWithValue("@idTheLoai", idTheLoai);

                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

            return ds.Tables[0];
        }

        public static DataTable GetMoviesByNation(int idQuocGia)
        {
            SqlConnection conn = null;
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                };

                SqlCommand sqlCommand = new SqlCommand("up_MovieListByNation", conn) { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.AddWithValue("@idQuocGia", idQuocGia);

                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return ds.Tables[0];
        }

        public static DataTable GetMovieDetail(int idPhim)
        {
            SqlConnection conn = null;
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                };

                SqlCommand sqlCommand = new SqlCommand("up_MovieDetailById", conn) { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.AddWithValue("@idPhim", idPhim);

                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

            return ds.Tables[0];
        }

        public static int Insert(string tenPhim_en, string tenPhim_vi, int danhMuc, int quocGia, int namPhatHanh, int thoiLuong, string urlPhim, string urlHinh, string moTa, ref int newId)
        {
            SqlConnection conn = null;
            int res = 0;
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                };
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("up_InsertMovie", conn) { CommandType = CommandType.StoredProcedure };

                sqlCommand.Parameters.AddWithValue("@TenPhim_en", tenPhim_en);
                sqlCommand.Parameters.AddWithValue("@TenPhim_vi", tenPhim_vi);
                sqlCommand.Parameters.AddWithValue("@DanhMuc", danhMuc);
                sqlCommand.Parameters.AddWithValue("@QuocGia", quocGia);
                sqlCommand.Parameters.AddWithValue("@NamPhatHanh", namPhatHanh);
                sqlCommand.Parameters.AddWithValue("@ThoiLuong", thoiLuong);
                sqlCommand.Parameters.AddWithValue("@UrlPhim", urlPhim);
                sqlCommand.Parameters.AddWithValue("@UrlHinh", urlHinh);
                sqlCommand.Parameters.AddWithValue("@MoTa", moTa);
                sqlCommand.Parameters.Add("@IdOutput", SqlDbType.Int).Direction = ParameterDirection.Output;

                res = sqlCommand.ExecuteNonQuery();
                newId = Convert.ToInt32(sqlCommand.Parameters["@IdOutput"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

            return res;
        }

        public static int Update(int ID, string tenPhim_en, string tenPhim_vi, int danhMuc, int quocGia, int namPhatHanh, int thoiLuong, string urlPhim, string urlHinh, string moTa)
        {
            SqlConnection conn = null;
            int res = 0;
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                };
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("up_UpdateMovie", conn) { CommandType = CommandType.StoredProcedure };

                sqlCommand.Parameters.AddWithValue("ID", ID);
                sqlCommand.Parameters.AddWithValue("@TenPhim_en", tenPhim_en);
                sqlCommand.Parameters.AddWithValue("@TenPhim_vi", tenPhim_vi);
                sqlCommand.Parameters.AddWithValue("@DanhMuc", danhMuc);
                sqlCommand.Parameters.AddWithValue("@QuocGia", quocGia);
                sqlCommand.Parameters.AddWithValue("@NamPhatHanh", namPhatHanh);
                sqlCommand.Parameters.AddWithValue("@ThoiLuong", thoiLuong);
                sqlCommand.Parameters.AddWithValue("@UrlPhim", urlPhim);
                sqlCommand.Parameters.AddWithValue("@UrlHinh", urlHinh);
                sqlCommand.Parameters.AddWithValue("@MoTa", moTa);

                res = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

            return res;
        }

        public static int Delete(int idPhim)
        {
            SqlConnection conn = null;
            int res = 0;
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                };
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("up_DeleteMovie", conn) { CommandType = CommandType.StoredProcedure };

                sqlCommand.Parameters.AddWithValue("@ID", idPhim);

                res = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

            return res;
        }
    }
}