using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebFilm.Controls.User.Home
{
    public partial class HomeControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        #region initial data

        private void LoadData()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tb_TheLoai WHERE ThuTu Is Not Null ORDER BY ThuTu", connection);

            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
            sda.Fill(ds);

            Repeater1.DataSource = ds.Tables[0];
            Repeater1.DataBind();
        }

        #endregion
    }
}