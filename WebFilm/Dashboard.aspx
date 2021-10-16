<%@ Page Title="Trang quản trị 123 Play" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WebFilm.Dashboard" %>

<%@ Register Src="~/Controls/Admin/adminControl.ascx" TagPrefix="uc1" TagName="adminControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhContent" runat="server">
    <%if (Session["admin"] != null)
        {%>
    <uc1:admincontrol runat="server" id="adminControl" />
    <%}
        else { Response.Redirect("LoginAdmin.aspx"); } %>
</asp:Content>