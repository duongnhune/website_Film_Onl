<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs" Inherits="WebFilm.Controls.User.Account.LoginControl" %>
<%@ Register Src="~/Controls/Utilities/Modal/ModalSuccess.ascx" TagPrefix="uc1" TagName="ModalSuccess" %>
<%@ Register Src="~/Controls/Utilities/Modal/ModalFailure.ascx" TagPrefix="uc1" TagName="ModalFailure" %>

<uc1:ModalFailure runat="server" ID="ModalFailure" IsBack="true" />
<uc1:ModalSuccess runat="server" ID="ModalSuccess" />
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="View1" runat="server">
        <div class="modal fade" id="loginModal" tabindex="-1" aria-labelledby="login-modal-label"
            aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <a href="#" class="btn-close" data-bs-dismiss="modal" aria-label="Close"><i
                            class="fas fa-times"></i></a>
                        <h4 class="modal-title">Đăng nhập
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-floating mb-3 has-valid">
                            <asp:TextBox ID="TxtUsernameLogin" runat="server" CssClass="form-control is-invalid" placeholder="Tên đăng nhập" />
                            <label for="<%=TxtUsernameLogin.ClientID %>">Tên đăng nhập *</label>
                            <asp:CustomValidator ID="ValidTxtUsernameLogin" runat="server" ValidateEmptyText="true" ControlToValidate="TxtUsernameLogin" Enabled="true" OnServerValidate="ValidTxtUsernameLogin_ServerValidate" ClientValidationFunction="validTxtUsernameLogin_ClientValidate" CssClass="invalid-tooltip"></asp:CustomValidator>
                        </div>
                        <div class="form-floating mb-3 has-valid">
                            <asp:TextBox ID="TxtPasswordLogin" runat="server" TextMode="Password" CssClass="form-control is-invalid" placeholder="Mật khẩu *" />
                            <label for="<%=TxtPasswordLogin.ClientID %>">Mật khẩu *</label>
                            <asp:CustomValidator ID="ValidPasswordLogin" runat="server" ValidateEmptyText="true" ControlToValidate="TxtPasswordLogin" Enabled="true" OnServerValidate="ValidPasswordLogin_ServerValidate" ClientValidationFunction="validPasswordLogin_ClientValidate" CssClass="invalid-tooltip"></asp:CustomValidator>
                        </div>
                        <asp:Button ID="btnLogin" runat="server" Text="ĐĂNG NHẬP" CssClass="btn btn-primary col-xl-12 col-md-12 col-sm-12 col-12" OnClick="btnLogin_Click" />
                    </div>
                    <div class="modal-footer">
                        <p>
                            <span>HOẶC</span><br />
                            Bạn chưa có tài khoản?
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CausesValidation="False">Đăng ký</asp:LinkButton>
                            <%--<a href="?index=dang-ky">Đăng ký</a>--%>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            var loginModal = new bootstrap.Modal(document.getElementById('loginModal'), {})

            function showLoginModal() {
                loginModal.toggle()
            }

            document.getElementById('loginModal').addEventListener('hidden.bs.modal', function (event) {
                window.history.back(-1);
            })

            var strRegexEmpty = /^\s*$/gm;

            function validTxtUsernameLogin_ClientValidate(source, args) {
                var value = document.getElementById('<%=TxtUsernameLogin.ClientID%>').value;

                if (strRegexEmpty.test(value)) {
                    source.innerHTML = "Nhập tên đăng nhập";
                    args.IsValid = false;
                    return;
                }
            }

            function validPasswordLogin_ClientValidate(source, args) {
                var value = document.getElementById('<%=TxtPasswordLogin.ClientID%>').value;

                if (strRegexEmpty.test(value)) {
                    source.innerHTML = "Nhập mật khẩu";
                    args.IsValid = false;
                    return;
                }
            }
        </script>
    </asp:View>

    <asp:View ID="View2" runat="server">
        <div class="modal fade" id="registerModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="register-modal-label" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <a href="#" class="btn-close" data-bs-dismiss="modal" onclick="window.location.href=window.location.href;" aria-label="Close"><i
                            class="fas fa-times"></i></a>
                        <h4 class="modal-title">Đăng ký
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-floating mb-3">
                            <asp:TextBox ID="TxtFullname" runat="server" CssClass="form-control"
                                placeholder="Họ và Tên" />
                            <label for="<%=TxtFullname.ClientID %>">Họ và Tên</label>
                        </div>
                        <div class="form-floating mb-3 has-invalid">
                            <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control is-invalid" placeholder="Email" />
                            <label for="<%=TxtEmail.ClientID %>">Email</label>
                            <asp:CustomValidator ID="ValidEmail" runat="server" ValidateEmptyText="true" ControlToValidate="TxtEmail" Enabled="true" OnServerValidate="ValidEmail_ServerValidate" ClientValidationFunction="validEmail_ClientValidate" CssClass="invalid-tooltip"></asp:CustomValidator>
                        </div>
                        <div class="form-floating mb-3 has-invalid">
                            <asp:TextBox ID="TxtPhone" runat="server" TextMode="Phone" CssClass="form-control is-invalid" placeholder="Số điện thoại" />
                            <label for="<%=TxtPhone.ClientID %>">Số điện thoại</label>
                            <asp:CustomValidator ID="ValidPhone" runat="server" ValidateEmptyText="true" ControlToValidate="TxtPhone" Enabled="true" OnServerValidate="ValidPhone_ServerValidate" ClientValidationFunction="validPhone_ClientValidate" CssClass="invalid-tooltip"></asp:CustomValidator>
                        </div>
                        <div class="form-floating mb-3 has-invalid">
                            <asp:TextBox ID="TxtUsernameReg" runat="server" CssClass="form-control is-invalid" placeholder="Tên đăng nhập *" />
                            <label for="<%=TxtUsernameReg.ClientID %>">Tên đăng nhập *</label>
                            <asp:CustomValidator ID="ValidUsernameReg" runat="server" ValidateEmptyText="true" ControlToValidate="TxtUsernameReg" Enabled="true" OnServerValidate="ValidUsernameReg_ServerValidate" ClientValidationFunction="validUsernameReg_ClientValidate" CssClass="invalid-tooltip"></asp:CustomValidator>
                        </div>
                        <div class="form-floating mb-3 has-invalid">
                            <asp:TextBox ID="TxtPasswordReg" runat="server" TextMode="Password" CssClass="form-control is-invalid" placeholder="Mật khẩu*" />
                            <label for="<%=TxtPasswordReg.ClientID %>">Mật khẩu *</label>
                            <asp:CustomValidator ID="ValidPasswordReg" runat="server" ValidateEmptyText="true" ControlToValidate="TxtPasswordReg" Enabled="true" OnServerValidate="ValidPasswordReg_ServerValidate" ClientValidationFunction="validPasswordReg_ClientValidate" CssClass="invalid-tooltip"></asp:CustomValidator>
                        </div>
                        <div class="form-floating mb-3 has-invalid">
                            <asp:TextBox ID="TxtRepassword" runat="server" TextMode="Password" CssClass="form-control is-invalid" placeholder="Nhập lại mật khẩu" />
                            <label for="<%=TxtRepassword.ClientID %>">Nhập lại mật khẩu</label>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TxtPasswordReg" ControlToCompare="TxtRepassword" ErrorMessage="Mật khẩu không khớp" CssClass="invalid-tooltip"></asp:CompareValidator>
                        </div>
                        <asp:Button ID="btnRegister" runat="server" Text="ĐĂNG KÝ" CssClass="btn btn-primary col-xl-12 col-md-12 col-sm-12 col-12" OnClick="btnRegister_Click" />
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            var registerModal = new bootstrap.Modal(document.getElementById('registerModal'), {});
            //registerModal.toggle();

            function showRegisterModal() {
                registerModal.toggle();
            }

            var strRegexEmpty = /^\s*$/gm;

            function validEmail_ClientValidate(source, args) {
                var value = document.getElementById('<%=TxtEmail.ClientID%>').value;
                var strRegexEmail = /^\w+([\.\-]\w+)*@([\w\-]+)((\.(\w){2,})+)$/gm;

                if (!strRegexEmail.test(value) && !strRegexEmpty.test(value)) {
                    source.innerHTML = "Email không hợp lệ";
                    args.IsValid = false;
                    return;
                }
            }

            function validPhone_ClientValidate(source, args) {
                var value = document.getElementById('<%=TxtPhone.ClientID%>').value;
                var strRegexPhone = /^0([0-9]){9}$/gm;

                if (!strRegexPhone.test(value) && !strRegexEmpty.test(value)) {
                    source.innerHTML = "Số điện thoại không hợp lệ";
                    args.IsValid = false;
                    return;
                }
            }

            function validUsernameReg_ClientValidate(source, args) {
                var value = document.getElementById('<%=TxtUsernameReg.ClientID%>').value;

                if (strRegexEmpty.test(value)) {
                    source.innerHTML = "Nhập tên đăng nhập";
                    args.IsValid = false;
                    return;
                }
            }

            function validPasswordReg_ClientValidate(source, args) {
                var value = document.getElementById('<%=TxtPasswordReg.ClientID%>').value;

                if (strRegexEmpty.test(value)) {
                    source.innerHTML = "Nhập mật khẩu";
                    args.IsValid = false;
                    return;
                }
            }
        </script>
    </asp:View>
</asp:MultiView>
