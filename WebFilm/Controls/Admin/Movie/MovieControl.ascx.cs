using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFilm_DAL;

namespace WebFilm.Controls.Admin.Movie
{
    public partial class MovieControl : System.Web.UI.UserControl
    {
        private string titleForm, urlImage;
        private DataTable dtMovie = clsMovie.GetMovies();
        private DataTable dtDirector = clsDirector.GetDirectors();
        private DataTable dtActor = clsActor.GetActors();
        private DataTable dtCatalogue = clsCatalogue.GetCatalogue();
        private DataTable dtNation = clsNation.GetNation();
        private DataTable dtCategories = clsCategories.GetCategories();
        private List<string> lstActorNotExist = new List<string>();
        private List<string> lstDirectorNotExist = new List<string>();
        private Regex regexEmpty = new Regex(@"^\s*$");
        private Regex regexFormatField = new Regex(@"^(\w+\s*(;(\s*\w+)+)*)+$");

        public string TitleForm { get => titleForm; set => titleForm = value; }
        public string UrlImage { get => urlImage; set => urlImage = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                LoadData();
            }
        }

        #region initial data, dropdown list, checkbox list

        private void LoadData()
        {
            DataTable dt = clsMovie.GetMovies();
            rptMovieList.DataSource = dt;
            rptMovieList.DataBind();
            if (dt.Rows.Count > 0)
                TitleForm = "Quản trị Phim";
            else TitleForm = "Chưa có dữ liệu";
        }

        protected void drpDanhMuc_Load(object sender, EventArgs e)
        {
            drpDanhMuc.DataSource = dtCatalogue;
            drpDanhMuc.DataTextField = "DanhMuc";
            drpDanhMuc.DataValueField = "ID";
            drpDanhMuc.DataBind();
        }

        protected void drpQuocGia_Load(object sender, EventArgs e)
        {
            drpQuocGia.Items.Add(new ListItem("-- Chọn Quốc gia --", ""));
            drpQuocGia.DataSource = dtNation;
            drpQuocGia.DataTextField = "QuocGia";
            drpQuocGia.DataValueField = "ID";
            drpQuocGia.DataBind();
        }

        protected void drpNamPhatHanh_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year; i >= 1990; i--)
                drpNamPhatHanh.Items.Add(new ListItem(i.ToString()));
            drpNamPhatHanh.SelectedIndex = 0;
        }

        protected void chkTheLoai_Load(object sender, EventArgs e)
        {
            foreach (DataRow row in dtCategories.Rows)
            {
                chkTheLoai.Items.Add(new ListItem { Text = row["TheLoai"].ToString(), Value = row["ID"].ToString() });
            }
            chkTheLoai.DataValueField = "Value";
            chkTheLoai.DataTextField = "Text";
        }

        #endregion initial data, dropdown list, checkbox list

        #region server validate

        protected void validTxtTenPhimEn_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(txtTenPhimEn.Text.Trim()))
            {
                args.IsValid = false;
                validTxtTenPhimEn.ErrorMessage = "Nhập tên phim EN";
                return;
            }
        }

        protected void validTxtTenPhimVi_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (regexEmpty.IsMatch(txtTenPhimVi.Text.Trim()))
            {
                args.IsValid = false;
                validTxtTenPhimVi.ErrorMessage = "Nhập tên phim VI";
                return;
            }
        }

        protected void validDrpDanhMuc_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (drpDanhMuc.SelectedValue == "")
            {
                args.IsValid = false;
                validDrpDanhMuc.ErrorMessage = "Chọn danh mục";
                return;
            }
        }

        protected void validDrpQuocGia_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (drpQuocGia.SelectedValue == "")
            {
                args.IsValid = false;
                validDrpQuocGia.ErrorMessage = "Chọn quốc gia";
                return;
            }
        }

        protected void validDrpNamPhatHanh_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (drpNamPhatHanh.SelectedValue == "")
            {
                args.IsValid = false;
                validDrpNamPhatHanh.ErrorMessage = "Chọn năm phát hành";
                return;
            }
        }

        protected void validTxtThoiLuong_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (regexEmpty.IsMatch(txtThoiLuong.Text))
            {
                args.IsValid = false;
                validTxtThoiLuong.ErrorMessage = "Nhập thời lượng phim";
                return;
            }

            Regex regexNumber = new Regex(@"^\d+$");
            if (!regexNumber.IsMatch(txtThoiLuong.Text))
            {
                args.IsValid = false;
                validTxtThoiLuong.ErrorMessage = "Thời lượng phải là số nguyên";
                return;
            }

            if (int.Parse(txtThoiLuong.Text) < 0)
            {
                args.IsValid = false;
                validTxtThoiLuong.ErrorMessage = "Thời lượng phải >=0";
                return;
            }
        }

        protected void validChkTheLoai_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = chkTheLoai.SelectedItem != null;
            return;
        }

        protected void validUploadHinh_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (hdIsUpdate.Value != "Update")
            {
                if (!uploadHinh.HasFile)
                {
                    args.IsValid = false;
                    validUploadHinh.ErrorMessage = "Chọn file hình ảnh";
                    return;
                }
            }
        }

        protected void validTxtDaoDien_ServerValidate(object source, ServerValidateEventArgs args)
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

            string[] arrDirectors = txtDaoDien.Text.Split(';');
            foreach (string item in arrDirectors)
            {
                if (dtDirector.Select("TenDaoDien ='" + item + "'").Length <= 0)
                    lstDirectorNotExist.Add(item.Trim());
            }

            if (lstDirectorNotExist.Count != 0)
            {
                string str = "";
                lstDirectorNotExist.ForEach(item => str += item + ", ");
                str = str.Remove(str.Length - 2);
                validTxtDaoDien.ErrorMessage = "Đạo diễn " + str + " hiện chưa có sẵn dữ liệu";
                args.IsValid = false;
                btnInsertDirector.Enabled = true;
                return;
            }
        }

        protected void validTxtDienVien_ServerValidate(object source, ServerValidateEventArgs args)
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

            string[] arrActors = txtDienVien.Text.Split(';');
            foreach (string item in arrActors)
            {
                if (dtActor.Select("TenDienVien ='" + item.Trim() + "'").Length <= 0)
                    lstActorNotExist.Add(item.Trim());
            }

            if (lstActorNotExist.Count != 0)
            {
                string str = "";
                lstActorNotExist.ForEach(item => str += item + ", ");
                str = str.Remove(str.Length - 2);

                validTxtDienVien.ErrorMessage = "Diễn viên " + str + " hiện chưa có sẵn dữ liệu";
                args.IsValid = false;
                btnInsertActor.Enabled = true;
            }
        }

        #endregion server validate

        #region event

        protected void btnNextToInsert_Click(object sender, EventArgs e)
        {
            TitleForm = "Thêm mới Phim";
            hdIsUpdate.Value = "Insert";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void rptMovieList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {
                case "Update":
                    {
                        TitleForm = "Cập nhật Phim";
                        MultiView1.ActiveViewIndex = 1;

                        DataTable dt = clsMovie.GetMovie(int.Parse(e.CommandArgument.ToString()));

                        if (dt.Rows.Count > 0)
                        {
                            txtTenPhimEn.Text = dt.Rows[0]["TenPhim_en"].ToString();
                            txtTenPhimVi.Text = dt.Rows[0]["TenPhim_vi"].ToString();
                            drpDanhMuc.SelectedValue = dt.Rows[0]["DanhMuc"].ToString();
                            drpQuocGia.SelectedValue = dt.Rows[0]["QuocGia"].ToString();
                            drpNamPhatHanh.SelectedValue = dt.Rows[0]["NamPhatHanh"].ToString();
                            txtThoiLuong.Text = dt.Rows[0]["ThoiLuong"].ToString();
                            //Get list categories
                            DataTable dtCategories = clsCategories.GetCategoriesByMovie(int.Parse(e.CommandArgument.ToString()));
                            foreach (DataRow row in dtCategories.Rows)
                                chkTheLoai.Items.FindByValue(row["ID"].ToString()).Selected = true;
                            //Get list directors
                            DataTable dtDirectors = clsDirector.GetDirectorsByMovie(int.Parse(e.CommandArgument.ToString()));
                            foreach (DataRow row in dtDirectors.Rows)
                                txtDaoDien.Text += row["TenDaoDien"].ToString() + ";";
                            txtDaoDien.Text = txtDaoDien.Text.Remove(txtDaoDien.Text.Length - 1);
                            //Get list actors
                            DataTable dtActors = clsActor.GetActorsByMovie(int.Parse(e.CommandArgument.ToString()));
                            foreach (DataRow row in dtActors.Rows)
                                txtDienVien.Text += row["TenDienVien"].ToString() + ";";
                            txtDienVien.Text = txtDienVien.Text.Remove(txtDienVien.Text.Length - 1);
                            txtUrlPhim.Text = dt.Rows[0]["UrlPhim"].ToString();
                            UrlImage += dt.Rows[0]["UrlHinh"].ToString();
                            txtEditorContent.Text = dt.Rows[0]["MoTa"].ToString();

                            hdMovieID.Value = e.CommandArgument.ToString();
                            hdIsUpdate.Value = "Update";
                        }
                    }
                    break;

                case "Delete":
                    {
                        hdMovieID.Value = e.CommandArgument.ToString();
                        hdIsUpdate.Value = "Delete";

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirmDeleteModal", "showConfirmDeleteModal()", true);
                    }
                    break;
            }
        }

        protected void btnInsertDirector_Click(object sender, EventArgs e)
        {
            if (clsDirector.Insert(lstDirectorNotExist.ToArray()) > 0)
            {
                btnInsertDirector.Enabled = false;
                Response.Write("<script>alert('Thêm đạo diễn thành công.');</script>");
            }
            else Response.Write("<script>alert('Thêm đạo diễn thất bại.');</script>");
        }

        protected void btnInsertActor_Click(object sender, EventArgs e)
        {
            if (clsActor.Insert(lstActorNotExist.ToArray()) > 0)
            {
                btnInsertActor.Enabled = false;
                Response.Write("<script>alert('Thêm đạo diễn thành công.');</script>");
            }
            else Response.Write("<script>alert('Thêm đạo diễn thất bại.');</script>");
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            string strFolder = Server.MapPath("/Upload/Image/");
            string oldUrl = dtMovie.Select("ID ='" + hdMovieID.Value.ToString() + "'")[0]["UrlHinh"].ToString();
            //DeleteByMovie old image
            if (File.Exists(strFolder + oldUrl))
            {
                File.Delete(strFolder + oldUrl);
            }

            if (clsMovie.Delete(int.Parse(hdMovieID.Value)) > 0)
            {
                ModalSuccess.Message = "Xóa phim thành công";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", " showSuccessModal();", true);
            }
            else
            {
                ModalFailure.Message = "Xóa phim thất bại";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", " showFailureModal();", true);
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string tenPhim_en = txtTenPhimEn.Text.Trim();
                string tenPhim_vi = txtTenPhimVi.Text.Trim();
                int danhMuc = int.Parse(drpDanhMuc.SelectedValue);
                int quocGia = int.Parse(drpQuocGia.SelectedValue);
                int namPhatHanh = int.Parse(drpNamPhatHanh.SelectedValue);
                int thoiLuong = int.Parse(txtThoiLuong.Text);
                string urlPhim = txtUrlPhim.Text;
                string moTa = txtEditorContent.Text;
                //Get Categories was checked
                List<string> lstTheLoaiChecked = new List<string>();
                foreach (ListItem item in chkTheLoai.Items)
                    if (item.Selected)
                        lstTheLoaiChecked.Add(item.Value);
                //Get Directors (is valid)
                List<string> lstIdDirector = new List<string>();
                foreach (string str in txtDaoDien.Text.Split(';'))
                    lstIdDirector.Add(dtDirector.Select("TenDaoDien='" + str.Trim() + "'")[0]["ID"].ToString());
                //Get Actors (is valid)
                List<string> lstIdActor = new List<string>();
                foreach (string str in txtDienVien.Text.Split(';'))
                    lstIdActor.Add(dtActor.Select("TenDienVien='" + str.Trim() + "'")[0]["ID"].ToString());

                string urlHinh = "";
                //Upload image
                string strFileName;
                string strFilePath;
                string strFolder = Server.MapPath("/Upload/Image/");

                if (hdIsUpdate.Value == "Update") //Update movie
                {
                    string oldUrl = dtMovie.Select("ID ='" + hdMovieID.Value.ToString() + "'")[0]["UrlHinh"].ToString();
                    if (uploadHinh.HasFile)
                    {
                        //DeleteByMovie old image
                        if (File.Exists(strFolder + oldUrl))
                        {
                            File.Delete(strFolder + oldUrl);
                        }
                        //Save new image
                        strFileName = uploadHinh.PostedFile.FileName;
                        strFileName = DateTime.Now.ToString("ddMMyyyy-hhmmss-") + Path.GetFileName(strFileName);
                        if (uploadHinh.HasFile)
                        {
                            if (!Directory.Exists(strFolder))
                            {
                                Directory.CreateDirectory(strFolder);
                            }
                            strFilePath = strFolder + strFileName;
                            uploadHinh.PostedFile.SaveAs(strFilePath);
                            urlHinh = strFileName;
                        }
                    }
                    else urlHinh = oldUrl;
                    int idPhim = int.Parse(hdMovieID.Value.ToString());
                    clsMovie_Categories.Delete(idPhim);
                    clsMovie_Director.Delete(idPhim);
                    clsMovie_Actor.DeleteByMovie(idPhim);

                    if (clsMovie.Update(idPhim, tenPhim_en, tenPhim_vi, danhMuc, quocGia, namPhatHanh, thoiLuong, urlPhim, urlHinh, moTa) > 0)
                    {
                        int rowAffected1 = clsMovie_Categories.Insert(idPhim, lstTheLoaiChecked.ToArray());
                        int rowAffected2 = clsMovie_Director.Insert(idPhim, lstIdDirector.ToArray());
                        int rowAffected3 = clsMovie_Actor.Insert(idPhim, lstIdActor.ToArray());

                        if (rowAffected1 > 0 && rowAffected2 > 0 && rowAffected3 > 0)
                        {
                            ModalSuccess.Message = "Cập nhật phim thành công";
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", " showSuccessModal();", true);
                        }
                        else
                        {
                            ModalFailure.Message = "Xảy ra lỗi trong quá trình cập nhật phim";
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", " showFailureModal();", true);
                        }
                    }
                    else
                    {
                        ModalFailure.Message = "Cập nhật phim thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", " showFailureModal();", true);
                    }
                }
                else //Insert new movie
                {
                    // Retrieve the name of the file that is posted.
                    strFileName = uploadHinh.PostedFile.FileName;
                    strFileName = DateTime.Now.ToString("ddMMyyyy-hhmmss-") + Path.GetFileName(strFileName);
                    if (uploadHinh.HasFile)
                    {
                        // Create the folder if it does not exist.
                        if (!Directory.Exists(strFolder))
                        {
                            Directory.CreateDirectory(strFolder);
                        }
                        // Save the uploaded file to the server.
                        strFilePath = strFolder + strFileName;
                        uploadHinh.PostedFile.SaveAs(strFilePath);
                        urlHinh = strFileName;
                    }

                    int newID = -1;
                    int row = clsMovie.Insert(tenPhim_en, tenPhim_vi, danhMuc, quocGia, namPhatHanh, thoiLuong, urlPhim, urlHinh, moTa, ref newID);
                    if (row > 0)
                    {
                        int rowAffected1 = clsMovie_Categories.Insert(newID, lstTheLoaiChecked.ToArray());
                        int rowAffected2 = clsMovie_Director.Insert(newID, lstIdDirector.ToArray());
                        int rowAffected3 = clsMovie_Actor.Insert(newID, lstIdActor.ToArray());

                        if (rowAffected1 > 0 && rowAffected2 > 0 && rowAffected3 > 0)
                        {
                            ModalSuccess.Message = "Thêm mới phim thành công";
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageSuccess", " showSuccessModal();", true);
                        }
                        else
                        {
                            ModalFailure.Message = "Xảy ra lỗi trong quá trình thêm mới phim";
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", " showFailureModal();", true);
                        }
                    }
                    else
                    {
                        ModalFailure.Message = "Thêm mới phim thất bại";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "messageFailure", " showFailureModal();", true);
                    }
                }
            }
        }
    }

    #endregion event
}