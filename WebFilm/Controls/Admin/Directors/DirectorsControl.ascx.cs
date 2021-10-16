using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using WebFilm_DAL;

namespace WebFilm.Controls.Admin.Directors
{
    public partial class DirectorsControl : System.Web.UI.UserControl
    {
        private DataTable dtDirectors = clsDirector.GetDirectors();
        private string titleForm;
        private int countRow;
        private Regex regexEmpty = new Regex(@"^\s*$");
        private Regex regexFormatField = new Regex(@"^(\w+\s*(;(\s*\w+)+)*)+$");

        public string TitleForm { get => titleForm; set => titleForm = value; }
        public int CountRow { get => countRow; set => countRow = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Quản trị Đạo diễn - 123 Play";
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                LoadData();
            }
        }

        #region initial data

        private void LoadData()
        {
            rptDirectorsList.DataSource = dtDirectors;
            rptDirectorsList.DataBind();
            if (dtDirectors.Rows.Count > 0)
                TitleForm = "Quản trị Đạo diễn";
            else
                TitleForm = "Chưa có dữ liệu";
        }

        #endregion initial data

        #region server validate

        protected void validTxtDaoDien_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(txtDaoDien.Text))
            {
                args.IsValid = false;
                validTxtDaoDien.ErrorMessage = "Nhập tên đạo diễn";
                return;
            }

            if (!regexFormatField.IsMatch(txtDaoDien.Text))
            {
                args.IsValid = false;
                validTxtDaoDien.ErrorMessage = "Vui lòng nhập theo dạng: đạo diễn 1; đạo diễn 2;...";
                return;
            }

            if (hdIsUpdate.Value != "Update")
            {
                List<string> lstDirectorsIsExist = new List<string>();
                string[] arrDirectors = txtDaoDien.Text.Split(';');
                foreach (string item in arrDirectors)
                {
                    if (dtDirectors.Select("TenDaoDien ='" + item + "'").Length != 0)
                        lstDirectorsIsExist.Add(item.Trim());
                }

                if (lstDirectorsIsExist.Count != 0)
                {
                    string str = "";
                    lstDirectorsIsExist.ForEach(itemStr => str += itemStr + ", ");
                    str = str.Remove(str.Length - 2);
                    validTxtDaoDien.ErrorMessage = "Đạo diễn " + str + " đã tồn tại";
                    args.IsValid = false;
                    return;
                }
            }
        }

        #endregion server validate

        #region event

        protected void rptDirectorsList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            DataTable dt = new DataTable();
            switch (e.CommandName.ToString())
            {
                case "Update":
                    {
                        TitleForm = "Cập nhật Đạo diễn";
                        MultiView1.ActiveViewIndex = 1;

                        dt = clsDirector.GetDirectors(int.Parse(e.CommandArgument.ToString()));

                        if (dt.Rows.Count > 0)
                        {
                            txtDaoDien.Text = dt.Rows[0]["TenDaoDien"].ToString();

                            hdDirectorsID.Value = e.CommandArgument.ToString();
                            hdIsUpdate.Value = "Update";
                        }
                    }
                    break;

                case "Delete":
                    {
                        hdDirectorsID.Value = e.CommandArgument.ToString();
                        hdIsUpdate.Value = "Delete";

                        CountRow = clsMovie_Director.GetMovies(int.Parse(e.CommandArgument.ToString())).Rows.Count;

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirmDeleteModal", "showConfirmDeleteModal()", true);
                    }
                    break;
            }
        }

        protected void btnNextToInsert_Click(object sender, EventArgs e)
        {
            TitleForm = "Thêm mới Đạo diễn";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (hdIsUpdate.Value.Equals("Update")) //Update director
                {
                    int rowAffected = clsDirector.Update(int.Parse(hdDirectorsID.Value), txtDaoDien.Text);
                    if (rowAffected > 0)
                    {
                        ModalSuccess.Message = "Cập nhật đạo diễn thành công";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
                    }
                    else
                    {
                        ModalFailure.Message = "Cập nhật đạo diễn thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
                    }
                }
                else //Insert directors
                {
                    int rowAffected = clsDirector.Insert(txtDaoDien.Text.Split(';').Select(str => str.Trim()).ToArray());
                    if (rowAffected > 0)
                    {
                        ModalSuccess.Message = "Thêm mới đạo diễn thành công";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
                    }
                    else
                    {
                        ModalFailure.Message = "Thêm mới đạo diễn thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
                    }
                }
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int res = clsDirector.Delete(int.Parse(hdDirectorsID.Value));

            if (res > 0)
            {
                ModalSuccess.Message = "Xóa đạo diễn thành công";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
            }
            else
            {
                ModalFailure.Message = "Xóa đạo diễn thất bại";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
            }
        }

        #endregion event
    }
}