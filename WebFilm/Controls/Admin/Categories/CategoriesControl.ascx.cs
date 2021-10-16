using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFilm_DAL;

namespace WebFilm.Controls.Admin.Categories
{
    public partial class CategoriesControl : System.Web.UI.UserControl
    {
        private DataTable dtCategories = clsCategories.GetCategories();
        private string titleForm;
        private int countRow;
        private Regex regexEmpty = new Regex(@"^\s*$");

        public string TitleForm { get => titleForm; set => titleForm = value; }
        public int CountRow { get => countRow; set => countRow = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                LoadData();
            }
        }

        #region initial data

        private void LoadData()
        {
            rptCategoriesList.DataSource = dtCategories;
            rptCategoriesList.DataBind();
            if (dtCategories.Rows.Count > 0)
                TitleForm = "Quản trị Thể loại";
            else
                TitleForm = "Chưa có dữ liệu";
        }

        #endregion initial data

        #region server validate

        protected void validTxtThuTu_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            string strRegexNumber = @"^\d+$";
            Regex regexNumber = new Regex(strRegexNumber);
            if (!regexEmpty.IsMatch(txtThuTu.Text) && !regexNumber.IsMatch(txtThuTu.Text))
            {
                args.IsValid = false;
                validTxtThuTu.ErrorMessage = "Độ ưu tiên phải là số nguyên";
                return;
            }
        }

        protected void validTxtTheLoai_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(txtTheLoai.Text))
            {
                args.IsValid = false;
                validTxtTheLoai.ErrorMessage = "Nhập thể loại";
                return;
            }
            if (hdIsUpdate.Value != "Update" && dtCategories.Select("TheLoai ='" + txtTheLoai.Text + "'").Length > 0)
            {
                args.IsValid = false;
                validTxtThuTu.ErrorMessage = "Thể loại đã tồn tại";
            }
        }

        #endregion server validate

        #region event

        protected void rptCategoriesList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            DataTable dt = new DataTable();
            switch (e.CommandName.ToString())
            {
                case "Update":
                    {
                        TitleForm = "Cập nhật thể loại";
                        MultiView1.ActiveViewIndex = 1;

                        dt = clsCategories.GetCategories(int.Parse(e.CommandArgument.ToString()));

                        if (dt.Rows.Count > 0)
                        {
                            txtTheLoai.Text = dt.Rows[0]["TheLoai"].ToString();
                            txtThuTu.Text = dt.Rows[0]["ThuTu"].ToString();

                            hdCategoriesID.Value = e.CommandArgument.ToString();
                            hdIsUpdate.Value = "Update";
                        }
                    }
                    break;

                case "Delete":
                    {
                        hdCategoriesID.Value = e.CommandArgument.ToString();
                        hdIsUpdate.Value = "Delete";

                        clsMovie movie = new clsMovie();
                        CountRow = clsMovie.GetMoviesByCatalogue(int.Parse(e.CommandArgument.ToString())).Rows.Count;

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirmDeleteModal", "showConfirmDeleteModal()", true);
                    }
                    break;
            }
        }

        protected void btnNextToInsert_Click(object sender, EventArgs e)
        {
            TitleForm = "Thêm mới Thể loại";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (hdIsUpdate.Value.Equals("Update")) //Update categories
                {
                    int rowAffected = clsCategories.Update(int.Parse(hdCategoriesID.Value), txtTheLoai.Text, txtThuTu.Text);
                    if (rowAffected > 0)
                    {
                        ModalSuccess.Message = "Cập nhật thể loại thành công";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", " showSuccessModal();", true);
                    }
                    else
                    {
                        ModalFailure.Message = "Cập nhật thể loại thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", " showFailureModal();", true);
                    }
                }
                else //Insert categories
                {
                    int rowAffected = clsCategories.Insert(txtTheLoai.Text, txtThuTu.Text);
                    if (rowAffected > 0)
                    {
                        ModalSuccess.Message = "Thêm thể loại thành công";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", " showSuccessModal();", true);
                    }
                    else
                    {
                        ModalFailure.Message = "Thêm thể loại thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", " showFailureModal();", true);
                    }
                }
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int res = clsCategories.Delete(int.Parse(hdCategoriesID.Value));

            if (res > 0)
            {
                ModalSuccess.Message = "Xóa thể loại thành công";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", " showSuccessModal();", true);
            }
            else
            {
                ModalFailure.Message = "Xóa thể loại thất bại";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", " showFailureModal();", true);
            }
        }

        #endregion event
    }
}