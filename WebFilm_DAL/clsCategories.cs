using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WebFilm_DAL
{
    public class clsCategories
    {
        public static DataTable GetCategories()
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_TheLoai", conn) { CommandType = CommandType.Text };

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

        public static DataTable GetCategories(int id)
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_TheLoai WHERE ID=@ID", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@ID", id);

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

        public static DataTable GetCategoriesByMovie(int idPhim)
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

                SqlCommand sqlCommand = new SqlCommand("up_CategoriesByMovie", conn) { CommandType = CommandType.StoredProcedure };
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

        public static int Insert(string TheLoai, string ThuTu)
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

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO tb_TheLoai(TheLoai, ThuTu) VALUES(@TheLoai, @ThuTu)", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@TheLoai", TheLoai);
                DbParameter paramThuTu = sqlCommand.CreateParameter();
                paramThuTu.ParameterName = "@ThuTu";
                paramThuTu.Value = string.IsNullOrEmpty(ThuTu) ? (object)DBNull.Value : int.Parse(ThuTu);
                sqlCommand.Parameters.Add(paramThuTu);

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

        public static int Update(int id, string TheLoai, string ThuTu)
        {
            SqlConnection conn = null;
            int res = 0;
            try
            {
                conn = new SqlConnection { ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString };
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("UPDATE tb_TheLoai SET TheLoai=@TheLoai, ThuTu=@ThuTu WHERE ID=@ID", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@TheLoai", TheLoai);
                DbParameter paramThuTu = sqlCommand.CreateParameter();
                paramThuTu.ParameterName = "@ThuTu";
                paramThuTu.Value = ThuTu;
                sqlCommand.Parameters.Add(paramThuTu);
                sqlCommand.Parameters.AddWithValue("@ID", id);

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

        public static int Delete(int idTheLoai)
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

                SqlCommand sqlCommand = new SqlCommand("up_DeleteCategories", conn) { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.AddWithValue("@ID", idTheLoai); ;

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