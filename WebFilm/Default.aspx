<%@ Page Title="123 Play - Xem phim online cực hay" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFilm.Default" %>

<%@ Register Src="~/Controls/User/userControl.ascx" TagPrefix="uc1" TagName="userControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="plhContent" runat="server">
    <uc1:userControl runat="server" ID="userControl" />
</asp:Content>