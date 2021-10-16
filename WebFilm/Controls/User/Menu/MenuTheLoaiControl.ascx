<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuTheLoaiControl.ascx.cs" Inherits="WebFilm.Controls.User.Menu.MenuTheLoaiControl" %>

<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <li class="navbar-item">
            <a href="?index=the-loai&the-loai=<%#:Eval("ID") %>" class="nav-link">Phim <%#: Eval("TheLoai") %> </a>
        </li>
    </ItemTemplate>
</asp:Repeater>