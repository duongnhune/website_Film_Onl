<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountControl.ascx.cs" Inherits="WebFilm.Controls.Admin.Account.AccountControl" %>
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
                        <th>Username</th>
                        <th>Password</th>
                        <th>Họ Tên</th>
                        <th>Email</th>
                        <th>Số điện thoại</th>
                        <th>Loại tài khoản</th>
                        <th class="no-content">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptAccountList" OnItemCommand="rptAccountList_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#:Eval("Username") %></td>
                                <td><%#:Eval("Password") %></td>
                                <td><%#:Eval("HoTen") %></td>
                                <td><%#:Eval("Email") %></td>
                                <td><%#:Eval("SDT") %></td>
                                <td><%#:Eval("LoaiTK").ToString()=="True"?"Quản trị viên":"Người dùng" %>
                                <td>
                                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CommandArgument='<%#:Eval("Username") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%#:Eval("Username") %>'><i class="far fa-trash-alt"></i></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <th>Username</th>
                        <th>Password</th>
                        <th>Họ Tên</th>
                        <th>Email</th>
                        <th>Số điện thoại</th>
                        <th>Loại tài khoản</th>
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
                        Bạn có chắn chắn muốn xoá tài khoản này?
                    </div>
                    <div class="modal-footer d-flex justify-content-end">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnConfirmDelete" runat="server" CssClass="btn btn-danger" Text="Xác nhận xoá" OnClientClick="disposeConfirmDeleteModal()" OnClick="btnConfirmDelete_Click" />
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
        <asp:HiddenField ID="hdUsername" runat="server" />
        <asp:HiddenField ID="hdIsUpdate" runat="server" />
        <div class="col-lg-12 layout-spacing px-3">
            <div class="row">
                <div class="col-xl-12 col-md-12 col-sm-12 col-12">
                    <h3 style="font-size: 2rem; font-weight: 700;"><%=this.TitleForm %></h3>
                </div>
            </div>
            <div>
                <div class="form-floating mb-3">
                    <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control"
                        placeholder="Họ và Tên" />
                    <label for="txtFullname">Họ và Tên</label>
                </div>
                <div class="form-floating mb-3 has-invalid">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control is-invalid"
                        placeholder="Email" />
                    <label for="txtEmail">Email</label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ValidationExpression="^\w+([\.\-]\w+)*@([\w\-]+)((\.(\w){2,})+)$" ErrorMessage="Email không hợp lệ" CssClass="invalid-tooltip"></asp:RegularExpressionValidator>
                </div>
                <div class="form-floating mb-3 has-invalid">
                    <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" CssClass="form-control is-invalid" placeholder="Số điện thoại" />
                    <label for="txtPhone">Số điện thoại</label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPhone" ValidationExpression="^0([0-9]){9}$" ErrorMessage="Số điện thoại hợp lệ" CssClass="invalid-tooltip"></asp:RegularExpressionValidator>
                </div>
                <div class="form-floating mb-3 has-invalid">
                    <asp:TextBox ID="txtUsername_reg" runat="server" CssClass="form-control is-invalid"
                        placeholder="Tên đăng nhập *" />
                    <label for="txtUsername_reg">Tên đăng nhập *</label>
                    <asp:CustomValidator ID="validTxtUsername" runat="server" ValidateEmptyText="true" ControlToValidate="txtUsername_reg" Enabled="true" OnServerValidate="validTxtUsername_ServerValidate" ClientValidationFunction="validTxtUsername_ClientValidate" CssClass="invalid-tooltip"></asp:CustomValidator>
                </div>
                <div class="form-floating mb-3 has-invalid">
                    <asp:TextBox ID="txtPassword_reg" runat="server" TextMode="Password" CssClass="form-control is-invalid" placeholder="Mật khẩu*" />
                    <label for="txtPassword_reg">Mật khẩu *</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword_reg" ErrorMessage="Nhập mật khẩu" CssClass="invalid-tooltip"></asp:RequiredFieldValidator>
                </div>
                <div class="form-floating mb-3 has-invalid">
                    <asp:TextBox ID="txtRepassword" runat="server" TextMode="Password" CssClass="form-control is-invalid" placeholder="Nhập lại mật khẩu" />
                    <label for="txtRepassword">Nhập lại mật khẩu</label>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPassword_reg" ControlToCompare="txtRepassword" ErrorMessage="Mật khẩu không khớp" CssClass="invalid-tooltip"></asp:CompareValidator>
                </div>
                <div class="d-flex align-item-center mb-3">
                    <label for="chkTypeAcc" style="margin-right: .5rem;">
                        Tài khoản Quản trị
                    </label>
                    <asp:CheckBox ID="chkTypeAcc" runat="server" />
                </div>
            </div>
            <div class="mt-3" runat="server">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-secondary mr-3" Text="Cancel" OnClientClick="JavaScript:window.history.back(1); return false;" />
                <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary" Text="Cập nhật" OnClick="btnInsert_Click" />
            </div>
        </div>

        <script type="text/javascript">
            function validTxtUsername_ClientValidate(source, args) {
                args.IsValid = true;
                var value = document.getElementById('<%=txtUsername_reg.ClientID%>').value;
                var strRegex = /^\s*$/gm;
                if (strRegex.test(value)) {
                    source.innerHTML = "Nhập tên đăng nhập";
                    args.IsValid = false;
                    return;
                }
            }
        </script>
    </asp:View>
</asp:MultiView>