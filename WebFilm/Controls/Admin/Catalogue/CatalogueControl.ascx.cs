using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using WebFilm_DAL;

namespace WebFilm.Controls.Admin.Catalogue
{
    public partial class CatalogueControl : System.Web.UI.UserControl
    {
        private DataTable dtCatalogue = clsCatalogue.GetCatalogue();
        private string titleForm;
        private int countRow;
        private Regex regexEmpty = new Regex(@"^\s*$");

        public string TitleForm { get => titleForm; set => titleForm = value; }
        public int CountRow { get => countRow; set => countRow = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Quản trị Danh mục - 123 Play";
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                LoadData();
            }
        }

        #region initial data

        private void LoadData()
        {
            rptCatalogueList.DataSource = dtCatalogue;
            rptCatalogueList.DataBind();
            if (dtCatalogue.Rows.Count > 0)
                TitleForm = "Quản trị Danh mục";
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

        protected void validTxtDanhMuc_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(txtDanhMuc.Text))
            {
                args.IsValid = false;
                validTxtDanhMuc.ErrorMessage = "Nhập danh mục";
                return;
            }
            if (hdIsUpdate.Value != "Update" && dtCatalogue.Select("DanhMuc ='" + txtDanhMuc.Text + "'").Length > 0)
            {
                args.IsValid = false;
                validTxtDanhMuc.ErrorMessage = "Danh mục đã tồn tại";
                return;
            }
        }

        #endregion server validate

        #region event

        protected void rptCatalogueList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            DataTable dt = new DataTable();
            switch (e.CommandName.ToString())
            {
                case "Update":
                    {
                        TitleForm = "Cập nhật Danh mục";
                        MultiView1.ActiveViewIndex = 1;

                        dt = clsCatalogue.GetCatalogue(int.Parse(e.CommandArgument.ToString()));

                        if (dt.Rows.Count > 0)
                        {
                            txtDanhMuc.Text = dt.Rows[0]["DanhMuc"].ToString();
                            txtThuTu.Text = dt.Rows[0]["ThuTu"].ToString();

                            hdCatalogueID.Value = e.CommandArgument.ToString();
                            hdIsUpdate.Value = "Update";
                        }
                    }
                    break;

                case "Delete":
                    {
                        hdCatalogueID.Value = e.CommandArgument.ToString();
                        hdIsUpdate.Value = "Delete";

                        CountRow = clsMovie.GetMoviesByCatalogue(int.Parse(e.CommandArgument.ToString())).Rows.Count;

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirmDeleteModal", "showConfirmDeleteModal()", true);
                    }
                    break;
            }
        }

        protected void btnNextToInsert_Click(object sender, EventArgs e)
        {
            TitleForm = "Thêm mới Danh mục";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (hdIsUpdate.Value.Equals("Update")) //Update catalogue
                {
                    int rowAffected = clsCatalogue.Update(int.Parse(hdCatalogueID.Value), txtDanhMuc.Text, txtThuTu.Text);
                    if (rowAffected > 0)
                    {
                        ModalSuccess.Message = "Cập nhật danh mục thành công";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
                    }
                    else
                    {
                        ModalFailure.Message = "Cập nhật danh mục thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
                    }
                }
                else //Insert catalogue
                {
                    int rowAffected = clsCatalogue.Insert(txtDanhMuc.Text, txtThuTu.Text);
                    if (rowAffected > 0)
                    {
                        ModalSuccess.Message = "Thêm mới danh mục thành công";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
                    }
                    else
                    {
                        ModalFailure.Message = "Thêm mới danh mục thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
                    }
                }
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int res = clsCatalogue.Delete(int.Parse(hdCatalogueID.Value));

            if (res > 0)
            {
                ModalSuccess.Message = "Xóa danh mục thành công";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
            }
            else
            {
                ModalFailure.Message = "Xóa danh mục thất bại";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
            }
        }

        #endregion event
    }
}