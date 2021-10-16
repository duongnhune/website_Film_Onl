using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using WebFilm_DAL;

namespace WebFilm.Controls.Admin.Nation
{
    public partial class NationControl : System.Web.UI.UserControl
    {
        private DataTable dtNation = clsNation.GetNation();
        private string titleForm;
        private int countRow;
        private Regex regexEmpty = new Regex(@"^\s*$");

        public string TitleForm { get => titleForm; set => titleForm = value; }
        public int CountRow { get => countRow; set => countRow = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Quản trị Quốc gia - 123 Play";
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                LoadData();
            }
        }

        #region initial data

        private void LoadData()
        {
            rptNationList.DataSource = dtNation;
            rptNationList.DataBind();
            if (dtNation.Rows.Count > 0)
                TitleForm = "Quản trị Quốc gia";
            else
                TitleForm = "Chưa có dữ liệu";
        }

        #endregion initial data

        #region server validate

        protected void validTxtThuTu_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            Regex regexNumber = new Regex(@"^\d+$");
            if (!regexEmpty.IsMatch(txtThuTu.Text) && !regexNumber.IsMatch(txtThuTu.Text))
            {
                args.IsValid = false;
                validTxtThuTu.ErrorMessage = "Độ ưu tiên phải là số nguyên";
                return;
            }
        }

        protected void validTxtQuocGia_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(txtQuocGia.Text))
            {
                args.IsValid = false;
                validTxtQuocGia.ErrorMessage = "Nhập quốc gia";
                return;
            }
            if (hdIsUpdate.Value != "Update" && dtNation.Select("QuocGia ='" + txtQuocGia.Text + "'").Length > 0)
            {
                args.IsValid = false;
                validTxtQuocGia.ErrorMessage = "Quốc gia đã tồn tại";
                return;
            }
        }

        #endregion server validate

        #region event

        protected void rptNationList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            DataTable dt = new DataTable();
            switch (e.CommandName.ToString())
            {
                case "Update":
                    {
                        TitleForm = "Cập nhật Quốc gia";
                        MultiView1.ActiveViewIndex = 1;

                        dt = clsNation.GetNation(int.Parse(e.CommandArgument.ToString()));

                        if (dt.Rows.Count > 0)
                        {
                            txtQuocGia.Text = dt.Rows[0]["QuocGia"].ToString();
                            txtThuTu.Text = dt.Rows[0]["ThuTu"].ToString();

                            hdNationID.Value = e.CommandArgument.ToString();
                            hdIsUpdate.Value = "Update";
                        }
                    }
                    break;

                case "Delete":
                    {
                        hdNationID.Value = e.CommandArgument.ToString();
                        hdIsUpdate.Value = "Delete";

                        CountRow = clsMovie.GetMoviesByNation(int.Parse(e.CommandArgument.ToString())).Rows.Count;

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirmDeleteModal", "showConfirmDeleteModal()", true);
                    }
                    break;
            }
        }

        protected void btnNextToInsert_Click(object sender, EventArgs e)
        {
            TitleForm = "Thêm mới Quốc gia";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (hdIsUpdate.Value.Equals("Update")) //Update catalogue
                {
                    int rowAffected = clsNation.Update(int.Parse(hdNationID.Value), txtQuocGia.Text, txtThuTu.Text);
                    if (rowAffected > 0)
                    {
                        ModalSuccess.Message = "Cập nhật quốc gia thành công";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
                    }
                    else
                    {
                        ModalFailure.Message = "Cập nhật quốc gia thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
                    }
                }
                else //Insert catalogue
                {
                    int rowAffected = clsNation.Insert(txtQuocGia.Text, txtThuTu.Text);
                    if (rowAffected > 0)
                    {
                        ModalSuccess.Message = "Thêm mới quốc gia thành công";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
                    }
                    else
                    {
                        ModalFailure.Message = "Thêm mới quốc gia thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
                    }
                }
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int res = clsNation.Delete(int.Parse(hdNationID.Value));

            if (res > 0)
            {
                ModalSuccess.Message = "Xóa quốc gia thành công";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
            }
            else
            {
                ModalFailure.Message = "Xóa quốc gia thất bại";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
            }
        }

        #endregion event
    }
}