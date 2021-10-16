using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebFilm_DAL
{
    public class clsDirector
    {
        public static DataTable GetDirectors()
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_DaoDien", conn) { CommandType = CommandType.Text };

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

        public static DataTable GetDirectors(int ID)
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_DaoDien WHERE ID=" + ID, conn) { CommandType = CommandType.Text };

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

        public static DataTable GetDirectorsByMovie(int idPhim)
        {
            SqlConnection conn = null;
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                };

                SqlCommand sqlCommand = new SqlCommand("up_DirectorsByMovie", conn) { CommandType = CommandType.StoredProcedure };
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

        public static int Insert(string TenDaoDien)
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

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO tb_DaoDien(TenDaoDien) VALUES(@TenDaoDien)", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@TenDaoDien", TenDaoDien);

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

        public static int Insert(string[] TenDaoDien)
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
                foreach (string item in TenDaoDien)
                {
                    str += "(N'" + item + "'),";
                }
                str = str.Remove(str.Length - 1);
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO tb_DaoDien(TenDaoDien) VALUES" + str, conn) { CommandType = CommandType.Text };

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

        public static int Update(int ID, string TenDaoDien)
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

                SqlCommand sqlCommand = new SqlCommand("UPDATE tb_DaoDien SET TenDaoDien=@TenDaoDien WHERE ID=@ID", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlCommand.Parameters.AddWithValue("@TenDaoDien", TenDaoDien);

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

        public static int Delete(int ID)
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

                SqlCommand sqlCommand = new SqlCommand("up_DeleteDirectors", conn) { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.AddWithValue("@ID", ID);

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