<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAdmin.aspx.cs" Inherits="WebFilm.LoginAdmin" %>

<!DOCTYPE html>

<html lang="vi">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Trang quản trị 123 Play</title>

    <webopt:BundleReference runat="server" Path="~/Content/css" />

    <!-- Boostrap v5.0.2 CSS -->
    <link rel="stylesheet" href="/Content/bootstrap.min.css" runat="server">

    <!-- Bootstrap JS -->
    <script src="/Scripts/bootstrap.min.js"></script>

    <!-- Boostrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.4.1/font/bootstrap-icons.css">

    <!-- Font Awesome -->
    <script src="https://kit.fontawesome.com/56a30c9aae.js" crossorigin="anonymous"></script>

    <!-- Main Style -->
    <link rel="stylesheet" href="/Content/style.css" runat="server" />

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <div class="container-fluid">
        <div class="header justify-content-center">
            <div class="logo">
                <a href="/Dashboard.aspx" class="logo-img">
                    <img src="/Upload/Logo/logo.png" alt="Logo 123Play" />
                </a>
            </div>
        </div>
        <div class="admin-container d-flex flex-column align-items-center justify-content-between">
            <form runat="server" class="form-login col-xl-6 col-md-6 col-sm-6 col-6">
                <div class="form-floating mb-3 has-valid">
                    <asp:TextBox ID="txtUsernameAdmin_login" runat="server" CssClass="form-control is-invalid" placeholder="Tên đăng nhập *" />
                    <label for="<%=txtUsernameAdmin_login.ClientID %>">Tên đăng nhập *</label>
                    <asp:CustomValidator ID="validTxtUsernameAdmin_login" runat="server" ValidateEmptyText="true" ControlToValidate="txtUsernameAdmin_login" Enabled="true" OnServerValidate="validTxtUsernameAdmin_login_ServerValidate" ClientValidationFunction="validTxtUsernameAdmin_login_ClientValidate" CssClass="invalid-tooltip"></asp:CustomValidator>
                </div>
                <div class="form-floating mb-3 has-valid">
                    <asp:TextBox ID="txtPasswordAdmin_login" runat="server" TextMode="Password" CssClass="form-control is-invalid" placeholder="Mật khẩu *" />
                    <label for="<%=txtPasswordAdmin_login.ClientID %>">Mật khẩu *</label>
                    <asp:CustomValidator ID="validTxtPasswordAdmin_login" runat="server" ValidateEmptyText="true" ControlToValidate="txtPasswordAdmin_login" Enabled="true" OnServerValidate="validTxtPasswordAdmin_login_ServerValidate" ClientValidationFunction="validTxtPasswordAdmin_login_ClientValidate" CssClass="invalid-tooltip"></asp:CustomValidator>
                </div>
                <asp:Button ID="btnLogin" runat="server" Text="ĐĂNG NHẬP" CssClass="btn btn-primary col-xl-12 col-md-12 col-sm-12 col-12" OnClick="btnLogin_Click" />
            </form>
            <footer>
                <a href="/Dashboard.aspx" class="logo-img">
                    <img src="/Upload/Logo/logo.png" alt="Logo 123Play" />
                </a>
                <p>
                    Copyright © 2021 - 123 Play.<br />
                    Đồ án kết thúc học phần <b>Lập trình Web</b><br />
                    Sinh viên thực hiện:
                <a href="https://www.facebook.com/doantuankiett"><b>Đoàn Tuấn Kiệt</b></a>
                    &
                <a href="https://www.facebook.com/nhu.duong.5872"><b>Dương Thị Huỳnh Như</b></a>
                </p>
                <div class="social-links">
                    <a href="mailto:contact@123play.com" target="_blank"><i class="fa fa-envelope"></i></a>
                    <a href="https://www.facebook.com/123play" target="_blank"><i class="fa fa-facebook"></i></a>
                    <a href="https://www.instagram.com/123play" target="_blank">
                        <i class="fa fa-instagram"></i>
                    </a>
                </div>
            </footer>
        </div>
    </div>

    <script type="text/javascript">
        var strRegex = /^\s*$/gm;

        function validTxtUsernameAdmin_login_ClientValidate(source, args) {
            args.IsValid = true;
            var value = document.getElementById('<%=txtUsernameAdmin_login.ClientID%>').value;
            if (strRegex.test(value)) {
                source.innerHTML = 'Nhập tên đăng nhập';
                args.IsValid = false;
                return;
            }
        }

        function validTxtPasswordAdmin_login_ClientValidate(source, args) {
            args.IsValid = true;
            var value = document.getElementById('<%=txtPasswordAdmin_login.ClientID%>').value;
            if (strRegex.test(value)) {
                source.innerHTML = 'Nhập mật khẩu';
                args.IsValid = false;
                return;
            }
        }
    </script>
</body>
</html>