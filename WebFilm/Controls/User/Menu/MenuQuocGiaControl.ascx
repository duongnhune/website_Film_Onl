<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuQuocGiaControl.ascx.cs" Inherits="WebFilm.Controls.User.Menu.MenuQuocGiaControl" %>

<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <li class="navbar-item">
            <a href="?index=quoc-gia&quoc-gia=<%#:Eval("ID") %>" class="nav-link">Phim <%#: Eval("QuocGia") %> </a>
        </li>
    </ItemTemplate>
</asp:Repeater>