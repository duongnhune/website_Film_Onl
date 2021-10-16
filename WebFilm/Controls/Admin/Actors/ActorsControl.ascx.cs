using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using WebFilm_DAL;

namespace WebFilm.Controls.Admin.Actors
{
    public partial class ActorsControl : System.Web.UI.UserControl
    {
        private DataTable dtActors = clsActor.GetActors();
        private string titleForm;
        private int countRow;
        private Regex regexEmpty = new Regex(@"^\s*$");
        private Regex regexFormatField = new Regex(@"^(\w+\s*(;(\s*\w+)+)*)+$");

        public string TitleForm { get => titleForm; set => titleForm = value; }
        public int CountRow { get => countRow; set => countRow = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Quản trị Diễn viên - 123 Play";
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                LoadData();
            }
        }

        #region initial data

        private void LoadData()
        {
            rptActorsList.DataSource = dtActors;
            rptActorsList.DataBind();
            if (dtActors.Rows.Count > 0)
                TitleForm = "Quản trị Diễn viên";
            else
                TitleForm = "Chưa có dữ liệu";
        }

        #endregion initial data

        #region server validate

        protected void validTxtDienVien_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(txtDienVien.Text))
            {
                args.IsValid = false;
                validTxtDienVien.ErrorMessage = "Nhập tên diễn viên";
                return;
            }

            if (!regexFormatField.IsMatch(txtDienVien.Text))
            {
                args.IsValid = false;
                validTxtDienVien.ErrorMessage = "Vui lòng nhập theo dạng: diễn viên 1; diễn viên 2;...";
                return;
            }

            if (hdIsUpdate.Value != "Update")
            {
                List<string> lstActorsIsExist = new List<string>();
                string[] arrActors = txtDienVien.Text.Split(';').Select(str => str.Trim()).ToArray();
                foreach (string item in arrActors)
                {
                    if (dtActors.Select("TenDienVien ='" + item + "'").Length != 0)
                        lstActorsIsExist.Add(item.Trim());
                }

                if (lstActorsIsExist.Count != 0)
                {
                    string str = string.Join(", ", lstActorsIsExist.ToArray());
                    validTxtDienVien.ErrorMessage = "Diễn viên " + str + " đã tồn tại";
                    args.IsValid = false;
                    return;
                }
            }
        }

        #endregion server validate

        #region event

        protected void rptActorsList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            DataTable dt = new DataTable();
            switch (e.CommandName.ToString())
            {
                case "Update":
                    {
                        TitleForm = "Cập nhật Diễn viên";
                        MultiView1.ActiveViewIndex = 1;

                        dt = clsActor.GetActors(int.Parse(e.CommandArgument.ToString()));

                        if (dt.Rows.Count > 0)
                        {
                            txtDienVien.Text = dt.Rows[0]["TenDienVien"].ToString();

                            hdActorsID.Value = e.CommandArgument.ToString();
                            hdIsUpdate.Value = "Update";
                        }
                    }
                    break;

                case "Delete":
                    {
                        hdActorsID.Value = e.CommandArgument.ToString();
                        hdIsUpdate.Value = "Delete";

                        CountRow = clsMovie_Actor.GetMovies(int.Parse(e.CommandArgument.ToString())).Rows.Count;

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirmDeleteModal", "showConfirmDeleteModal()", true);
                    }
                    break;
            }
        }

        protected void btnNextToInsert_Click(object sender, EventArgs e)
        {
            TitleForm = "Thêm mới Diễn viên";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (hdIsUpdate.Value.Equals("Update")) //Update actor
                {
                    int rowAffected = clsActor.Update(int.Parse(hdActorsID.Value), txtDienVien.Text);
                    if (rowAffected > 0)
                    {
                        ModalSuccess.Message = "Cập nhật diễn viên thành công";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
                    }
                    else
                    {
                        ModalFailure.Message = "Cập nhật diễn viên thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
                    }
                }
                else //Insert actors
                {
                    int rowAffected = clsActor.Insert(txtDienVien.Text.Split(';').Select(str => str.Trim()).ToArray());
                    if (rowAffected > 0)
                    {
                        ModalSuccess.Message = "Thêm mới diễn viên thành công";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
                    }
                    else
                    {
                        ModalFailure.Message = "Thêm mới diễn viên thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
                    }
                }
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int res = clsActor.Delete(int.Parse(hdActorsID.Value));

            if (res > 0)
            {
                ModalSuccess.Message = "Xóa diễn viên thành công";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", "showSuccessModal();", true);
            }
            else
            {
                ModalFailure.Message = "Xóa diễn viên thất bại";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", "showFailureModal();", true);
            }
        }

        #endregion event
    }
}