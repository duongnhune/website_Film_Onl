using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using WebFilm_DAL;

namespace WebFilm.Controls.User.Account
{
    public partial class LoginControl : System.Web.UI.UserControl
    {
        Regex regexEmpty = new Regex(@"^\s*$");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //Go to Login
                MultiView1.ActiveViewIndex = 0;
        }

        #region server validate

        protected void ValidTxtUsernameLogin_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(TxtUsernameLogin.Text))
            {
                args.IsValid = false;
                ValidTxtUsernameLogin.ErrorMessage = "Nhập tên đăng nhập";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showLoginModal", "showLoginModal()", true);
            }
        }

        protected void ValidEmail_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;
            Regex regexEmail = new Regex(@"^\w+([\.\-]\w+)*@([\w\-]+)((\.(\w){2,})+)$");

            if (!regexEmail.IsMatch(TxtEmail.Text) && !regexEmpty.IsMatch(TxtEmail.Text))
            {
                args.IsValid = false;
                ValidEmail.ErrorMessage = "Email không hợp lệ";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showRegisterModal", "showRegisterModal();", true);
            }
        }

        protected void ValidPhone_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;
            Regex regexPhone = new Regex(@"^0([0-9]){9}$");

            if (!regexPhone.IsMatch(TxtPhone.Text) && !regexEmpty.IsMatch(TxtPhone.Text))
            {
                args.IsValid = false;
                ValidPhone.ErrorMessage = "Số điện thoại không hợp lệ";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showRegisterModal", "showRegisterModal();", true);
            }
        }

        protected void ValidUsernameReg_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(TxtUsernameReg.Text))
            {
                args.IsValid = false;
                ValidUsernameReg.ErrorMessage = "Nhập tên đăng nhập";
                return;
            }
            if (clsAccount.CheckUsername(TxtUsernameReg.Text) > 0)
            {
                args.IsValid = false;
                ValidUsernameReg.ErrorMessage = "Tên đăng nhập đã tồn tại";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showRegisterModal", "showRegisterModal();", true);
            }
        }

        protected void ValidPasswordReg_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(TxtPasswordReg.Text))
            {
                args.IsValid = false;
                ValidPasswordReg.ErrorMessage = "Nhập mật khẩu";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showRegisterModal", "showRegisterModal();", true);
            }
        }

        protected void ValidPasswordLogin_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(TxtPasswordLogin.Text))
            {
                args.IsValid = false;
                ValidPasswordLogin.ErrorMessage = "Nhập mật khẩu";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showLoginModal", "showLoginModal()", true);
            }
        }

        #endregion

        #region event

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1; //Go to Register
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showRegisterModal", "showRegisterModal();", true);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                DataTable dt = new DataTable();
                dt = clsAccount.Login(TxtUsernameLogin.Text, TxtPasswordLogin.Text);

                if (dt.Rows.Count > 0) //Login success
                {
                    Session["username"] = TxtUsernameLogin.Text.Trim();
                    Response.Redirect(Request.RawUrl);
                }
                else //Login fail
                {
                    ValidTxtUsernameLogin.IsValid = false;
                    ValidTxtUsernameLogin.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showLoginModal", "showLoginModal()", true);
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = TxtUsernameReg.Text;
                string password = TxtPasswordReg.Text;
                string fullname = TxtFullname.Text;
                string email = TxtEmail.Text;
                string phone = TxtPhone.Text;

                //Register a user-level account, so the account type is false
                int check = clsAccount.Register(username, password, fullname, email, phone, false);

                if (check > 0)
                {
                    ModalSuccess.Message = "Chúc mừng bạn đã tạo tài khoản thành công 🎉 123 Play with luv 💕";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
                }
                else
                {
                    ModalFailure.Message = "Rất tiếc, đăng ký tài khoản thất bại. 😢";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
                }
            }
        }

        #endregion
    }
}