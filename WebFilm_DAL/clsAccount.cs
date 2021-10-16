using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace WebFilm_DAL
{
    public class clsAccount
    {
        protected static string EncryptPassword(string input)
        {
            MD5 encrypt = MD5.Create();
            byte[] hashCode = encrypt.ComputeHash(Encoding.Unicode.GetBytes(input));

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashCode.Length; i++)
            {
                stringBuilder.Append(hashCode[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static int CheckUsername(string username)
        {
            int res = 0;
            if (!string.IsNullOrEmpty(username))
            {
                SqlConnection conn = null;
                try
                {
                    conn = new SqlConnection
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                    };
                    conn.Open();

                    SqlCommand sqlCommand = new SqlCommand("SELECT dbo.uf_CheckUsername(@Username)", conn) { CommandType = CommandType.Text };
                    sqlCommand.Parameters.AddWithValue("@Username", username);

                    res = (int)sqlCommand.ExecuteScalar();
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
            }
            return res;
        }

        public static int Register(string username, string password, string fullname, string email, string phone, bool typeAcc)
        {
            int res = 0;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                SqlConnection conn = null;
                try
                {
                    conn = new SqlConnection
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                    };
                    conn.Open();

                    SqlCommand sqlCommand = new SqlCommand("up_Register", conn) { CommandType = CommandType.StoredProcedure };

                    //Handle fields that are not required
                    //If the field is empty, that parameter = type of NULL database
                    DbParameter paramFullname = sqlCommand.CreateParameter();
                    paramFullname.ParameterName = "@HoTen";
                    paramFullname.Value = string.IsNullOrEmpty(fullname) ? (object)DBNull.Value : fullname;

                    DbParameter paramEmail = sqlCommand.CreateParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.Value = string.IsNullOrEmpty(email) ? (object)DBNull.Value : email;

                    DbParameter paramPhone = sqlCommand.CreateParameter();
                    paramPhone.ParameterName = "@SDT";
                    paramPhone.Value = string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone;

                    sqlCommand.Parameters.AddWithValue("@Username", username);
                    sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(password + username));
                    sqlCommand.Parameters.Add(paramFullname);
                    sqlCommand.Parameters.Add(paramEmail);
                    sqlCommand.Parameters.Add(paramPhone);
                    sqlCommand.Parameters.AddWithValue("@LoaiTK", typeAcc);

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
            }
            return res;
        }

        public static DataTable Login(string username, string password)
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

                SqlCommand sqlCommand = new SqlCommand("up_Login", conn) { CommandType = CommandType.StoredProcedure };

                sqlCommand.Parameters.AddWithValue("@Username", username);
                sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(password + username));

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

        public static int Update(string username, string password, string fullname, string email, string phone, bool typeAcc)
        {
            int res = 0;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                SqlConnection conn = null;
                try
                {
                    conn = new SqlConnection
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                    };
                    conn.Open();

                    SqlCommand sqlCommand = new SqlCommand("up_UpdateAccount", conn) { CommandType = CommandType.StoredProcedure };

                    //Handle fields that are not required
                    //If the field is empty, that parameter = type of NULL database
                    DbParameter paramFullname = sqlCommand.CreateParameter();
                    paramFullname.ParameterName = "@HoTen";
                    paramFullname.Value = string.IsNullOrEmpty(fullname) ? (object)DBNull.Value : fullname;

                    DbParameter paramEmail = sqlCommand.CreateParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.Value = string.IsNullOrEmpty(email) ? (object)DBNull.Value : email;

                    DbParameter paramPhone = sqlCommand.CreateParameter();
                    paramPhone.ParameterName = "@SDT";
                    paramPhone.Value = string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone;

                    sqlCommand.Parameters.AddWithValue("@Username", username);
                    sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(password + username));
                    sqlCommand.Parameters.Add(paramFullname);
                    sqlCommand.Parameters.Add(paramEmail);
                    sqlCommand.Parameters.Add(paramPhone);
                    sqlCommand.Parameters.AddWithValue("@LoaiTK", typeAcc);

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
            }
            return res;
        }

        public static int Delete(string Username)
        {
            int res = 0;
            if (!string.IsNullOrEmpty(Username))
            {
                SqlConnection conn = null;
                try
                {
                    conn = new SqlConnection
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
                    };
                    conn.Open();

                    SqlCommand sqlCommand = new SqlCommand("UPDATE tb_TaiKhoan SET DaXoa=1 WHERE Username=" + Username, conn) { CommandType = CommandType.Text };

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
            }
            return res;
        }

        public static DataTable GetAccount()
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_TaiKhoan WHERE DaXoa=0", conn) { CommandType = CommandType.Text };

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

        public static DataTable GetAccount(string username)
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_TaiKhoan WHERE Username=@Username AND DaXoa=0", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@Username", username);

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

        public static DataTable GetAccount(bool LoaiTK)
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

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_TaiKhoan WHERE LoaiTK=@LoaiTK AND DaXoa=0", conn) { CommandType = CommandType.Text };
                sqlCommand.Parameters.AddWithValue("@LoaiTK", LoaiTK);

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