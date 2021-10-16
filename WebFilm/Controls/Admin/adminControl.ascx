<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="adminControl.ascx.cs" Inherits="WebFilm.Controls.Admin.adminControl" %>
<%@ Register Src="~/Controls/Admin/Account/AccountControl.ascx" TagPrefix="uc1" TagName="AccountControl" %>
<%@ Register Src="~/Controls/Admin/Catalogue/CatalogueControl.ascx" TagPrefix="uc1" TagName="CatalogueControl" %>
<%@ Register Src="~/Controls/Admin/Movie/MovieControl.ascx" TagPrefix="uc1" TagName="MovieControl" %>
<%@ Register Src="~/Controls/Admin/Categories/CategoriesControl.ascx" TagPrefix="uc1" TagName="CategoriesControl" %>
<%@ Register Src="~/Controls/Admin/Nation/NationControl.ascx" TagPrefix="uc1" TagName="NationControl" %>
<%@ Register Src="~/Controls/Admin/Directors/DirectorsControl.ascx" TagPrefix="uc1" TagName="DirectorsControl" %>
<%@ Register Src="~/Controls/Admin/Actors/ActorsControl.ascx" TagPrefix="uc1" TagName="ActorsControl" %>

<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="View1" runat="server">
        <uc1:AccountControl runat="server" ID="AccountControl" />
    </asp:View>
    <asp:View ID="View2" runat="server">
        <uc1:CatalogueControl runat="server" ID="CatalogueControl" />
    </asp:View>
    <asp:View ID="View3" runat="server">
        <uc1:MovieControl runat="server" ID="MovieControl" />
    </asp:View>
    <asp:View ID="View4" runat="server">
        <uc1:CategoriesControl runat="server" ID="CategoriesControl" />
    </asp:View>
    <asp:View ID="View5" runat="server">
        <uc1:NationControl runat="server" ID="NationControl" />
    </asp:View>
    <asp:View ID="View6" runat="server">
        <uc1:DirectorsControl runat="server" ID="DirectorsControl" />
    </asp:View>
    <asp:View ID="View7" runat="server">
        <uc1:ActorsControl runat="server" ID="ActorsControl" />
    </asp:View>
</asp:MultiView>