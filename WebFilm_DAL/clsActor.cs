using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebFilm_DAL
{
    public class clsActor
    {
        public static DataTable GetActors()
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_DienVien", conn) { CommandType = CommandType.Text };

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

        public static DataTable GetActors(int ID)
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_DienVien WHERE ID=" + ID, conn) { CommandType = CommandType.Text };

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

        public static DataTable GetActorsByMovie(int idPhim)
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

                SqlCommand sqlCommand = new SqlCommand("up_ActorsByMovie", conn) { CommandType = CommandType.StoredProcedure };
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

        public static int Insert(string TenDienVien)
        {
            SqlConnection conn = null;
            int res = 0;
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO tb_DienVien(TenDienVien) VALUES(@TenDienVien)", conn);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@TenDienVien", TenDienVien);

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

        public static int Insert(string[] TenDienVien)
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
                foreach (string item in TenDienVien)
                {
                    str += "(N'" + item + "'),";
                }
                str = str.Remove(str.Length - 1);
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO tb_DienVien(TenDienVien) VALUES" + str, conn) { CommandType = CommandType.Text };

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

        public static int Update(int ID, string TenDienVien)
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

                SqlCommand sqlCommand = new SqlCommand("UPDATE tb_DienVien SET TenDienVien=@TenDienVien WHERE ID=@ID", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlCommand.Parameters.AddWithValue("@TenDienVien", TenDienVien);

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

                SqlCommand sqlCommand = new SqlCommand("up_DeleteActors", conn) { CommandType = CommandType.StoredProcedure };
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