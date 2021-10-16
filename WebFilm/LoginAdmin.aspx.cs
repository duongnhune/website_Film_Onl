using System;
using System.Data;
using System.Text.RegularExpressions;
using WebFilm_DAL;

namespace WebFilm
{
    public partial class LoginAdmin : System.Web.UI.Page
    {
        private Regex regexEmpty = new Regex(@"^\s*$");

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsAccount.Login(txtUsernameAdmin_login.Text, txtPasswordAdmin_login.Text);

            if (dt.Rows.Count > 0 && (bool)dt.Rows[0]["LoaiTK"] == true)
            {
                Session["admin"] = txtUsernameAdmin_login.Text;
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                validTxtUsernameAdmin_login.IsValid = false;
                validTxtUsernameAdmin_login.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
        }

        protected void validTxtUsernameAdmin_login_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(txtUsernameAdmin_login.Text))
            {
                args.IsValid = false;
                validTxtUsernameAdmin_login.ErrorMessage = "Nhập tên đăng nhập";
                return;
            }
            if (clsAccount.CheckUsername(txtUsernameAdmin_login.Text) <= 0)
            {
                args.IsValid = false;
                validTxtUsernameAdmin_login.ErrorMessage = "Tên đăng nhập không tồn tại";
                return;
            }
        }

        protected void validTxtPasswordAdmin_login_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (regexEmpty.IsMatch(txtPasswordAdmin_login.Text))
            {
                args.IsValid = false;
                validTxtPasswordAdmin_login.ErrorMessage = "Nhập mật khẩu";
                return;
            }
        }
    }
}