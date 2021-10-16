using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebFilm_DAL
{
    public class clsMovie_Director
    {
        public static int Insert(int idPhim, string[] arrIdDaoDien)
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

                string str = "";
                foreach (string idDaoDien in arrIdDaoDien)
                {
                    str += "(" + idPhim + "," + idDaoDien + "),";
                }
                str = str.Remove(str.Length - 1);

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO tb_Phim_DaoDien (idPhim, idDaoDien) VALUES" + str, conn) { CommandType = CommandType.Text };

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

                SqlCommand sqlCommand = new SqlCommand("up_DeleteDirectorsByMovie", conn) { CommandType = CommandType.StoredProcedure };

                sqlCommand.Parameters.AddWithValue("@idPhim", idPhim);

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

        public static DataTable GetMovies(int idDaoDien)
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_Phim_DaoDien WHERE idDaoDien=@idDaoDien", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@idDaoDien", idDaoDien);

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

        public static DataTable GetDirectors(int idPhim)
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_Phim_DaoDien WHERE idPhim=@idPhim", conn) { CommandType = CommandType.Text };
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
    }
}