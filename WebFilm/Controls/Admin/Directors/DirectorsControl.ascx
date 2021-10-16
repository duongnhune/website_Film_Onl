<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectorsControl.ascx.cs" Inherits="WebFilm.Controls.Admin.Directors.DirectorsControl" %>
<%@ Register Src="~/Controls/Utilities/Modal/ModalSuccess.ascx" TagPrefix="uc1" TagName="ModalSuccess" %>
<%@ Register Src="~/Controls/Utilities/Modal/ModalFailure.ascx" TagPrefix="uc1" TagName="ModalFailure" %>

<uc1:ModalFailure runat="server" ID="ModalFailure" />
<uc1:ModalSuccess runat="server" ID="ModalSuccess" />
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="View1" runat="server">
        <div class="table-responsive table-container">
            <div class="d-flex align-items-center justify-content-between">
                <h3 style="font-size: 2rem; font-weight: 700;"><%=this.TitleForm %></h3>
                <asp:LinkButton ID="btnNextToInsert" runat="server" OnClick="btnNextToInsert_Click"><i class="fas fa-plus fa-2x"></i></asp:LinkButton>
            </div>

            <table
                id="datatable"
                class="table dt-table-hover"
                style="width: 100%">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Tên đạo diễn</th>
                        <th class="no-content">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDirectorsList" OnItemCommand="rptDirectorsList_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#:Eval("ID") %></td>
                                <td><%#:Eval("TenDaoDien") %></td>
                                <td>
                                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CommandArgument='<%#:Eval("ID") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                    <asp:LinkButton ID="btnNextToConfirm" runat="server" CommandName="Delete" CommandArgument='<%#:Eval("ID") %>'><i class="far fa-trash-alt"></i></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <th>ID</th>
                        <th>Tên đạo diễn</th>
                        <th></th>
                    </tr>
                </tfoot>
            </table>
        </div>

        <svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
            <symbol id="exclamation-triangle-fill" fill="currentColor" viewBox="0 0 16 16">
                <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z" />
            </symbol>
        </svg>
        <div class="modal fade" id="confirmDeleteModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="confirm-delete-modal-label"
            aria-hidden="true">
            <div class="modal-dialog modal-fullscreen-md-down">
                <div class="modal-content">
                    <div class="modal-header">
                        <a href="#" class="btn-close" data-bs-dismiss="modal" aria-label="Close"><i
                            class="fas fa-times"></i></a>
                        <h5 class="modal-title">Xác nhận xóa</h5>
                    </div>
                    <div class="modal-body alert alert-danger" role="alert">
                        <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Danger:">
                            <use xlink:href="#exclamation-triangle-fill" />
                        </svg>
                        <%if (this.CountRow == 0)
                            {%>Bạn có chắc chắn muốn xóa đạo diễn này? <%}
                        else
                        { %>
                        Xoá đạo diễn này sẽ làm ảnh hưởng <%=this.CountRow %> phim hiện đang liên kết. Bạn có chắn chắn muốn xoá?<%}%>
                    </div>
                    <div class="modal-footer d-flex justify-content-end">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-danger" Text="Xác nhận xoá" OnClientClick="disposeConfirmDeleteModal()" OnClick="btnConfirmDelete_Click" />
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            var confirmDeleteModal = new bootstrap.Modal(document.getElementById('confirmDeleteModal'), {});
            function showConfirmDeleteModal() {
                confirmDeleteModal.toggle();
            }

            function disposeConfirmDeleteModal() {
                confirmDeleteModal.dispose();
            }
        </script>
    </asp:View>

    <asp:View ID="View2" runat="server">
        <asp:HiddenField ID="hdDirectorsID" runat="server" />
        <asp:HiddenField ID="hdIsUpdate" runat="server" />
        <div class="col-lg-12 layout-spacing  px-3">
            <div class="row">
                <div class="col-xl-12 col-md-12 col-sm-12 col-12">
                    <h3 style="font-size: 2rem; font-weight: 700;"><%=this.TitleForm %></h3>
                </div>
            </div>
            <div class="row" runat="server">
                <div class="form-floating col-md-12 has-valid">
                    <asp:TextBox ID="txtDaoDien" runat="server" CssClass="form-control is-invalid" placeholder="Đạo diễn *" />
                    <label for="<%=txtDaoDien.ClientID %>">Đạo diễn *</label>
                    <asp:CustomValidator ID="validTxtDaoDien" runat="server" ValidateEmptyText="true" ControlToValidate="txtDaoDien" Enabled="true" OnServerValidate="validTxtDaoDien_ServerValidate" ClientValidationFunction="validTxtDaoDien_ClientValidate" CssClass="invalid-tooltip"></asp:CustomValidator>
                </div>
            </div>
            <div class="mt-3" runat="server">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-secondary mr-3" Text="Cancel" OnClientClick="JavaScript:window.history.back(1); return false;" />
                <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary" Text="Cập nhật" OnClick="btnInsert_Click" />
            </div>
        </div>
        <script type="text/javascript">
            var strRegexEmpty = /^\s*$/gm;

            function removeUnicode(str) {
                if (str === null || str === undefined) return str;
                str = str.toLowerCase();
                str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
                str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
                str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
                str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
                str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
                str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
                str = str.replace(/đ/g, "d");
                return str;
            }

            function validTxtDaoDien_ClientValidate(source, args) {
                args.IsValid = true;
                var value = document.getElementById('<%=txtDaoDien.ClientID%>').value;

                if (strRegexEmpty.test(value)) {
                    args.IsValid = false;
                    source.innerHTML = "Nhập tên đạo diễn";
                    return;
                }

                strRegex = /^(\w+\s*(;(\s*\w+)+)*)+$/gm;
                if (!strRegex.test(removeUnicode(value))) {
                    args.IsValid = false;
                    source.innerHTML = "Vui lòng nhập theo dạng: đạo diễn 1; đạo diễn 2;...";
                    return;
                }
            }
        </script>
    </asp:View>
</asp:MultiView>