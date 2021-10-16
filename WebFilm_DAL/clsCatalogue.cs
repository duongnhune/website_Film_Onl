using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WebFilm_DAL
{
    public class clsCatalogue
    {
        public static DataTable GetCatalogue()
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_DanhMuc", conn) { CommandType = CommandType.Text };

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

        public static DataTable GetCatalogue(int ID)
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_DanhMuc WHERE ID=@ID", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@ID", ID);

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

        public static int Insert(string DanhMuc, string ThuTu)
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

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO tb_DanhMuc(DanhMuc, ThuTu) VALUES(@DanhMuc, @ThuTu)", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@DanhMuc", DanhMuc);
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

        public static int Update(int ID, string DanhMuc, string ThuTu)
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

                SqlCommand sqlCommand = new SqlCommand("UPDATE tb_DanhMuc SET DanhMuc=@DanhMuc, ThuTu=@ThuTu WHERE ID=@ID", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlCommand.Parameters.AddWithValue("@DanhMuc", DanhMuc);
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

        public static int Delete(int idDanhMuc)
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

                SqlCommand sqlCommand = new SqlCommand("up_DeleteCatalogue", conn) { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.AddWithValue("@ID", idDanhMuc); ;

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