<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeControl.ascx.cs" Inherits="WebFilm.Controls.User.Home.HomeControl" %>
<%@ Register Src="~/Controls/User/Banner/BannerControl.ascx" TagPrefix="uc1" TagName="BannerControl" %>
<%@ Register Src="~/Controls/User/Movie/MovieListSwiperControl.ascx" TagPrefix="uc1" TagName="MovieListSwiperControl" %>

<uc1:BannerControl runat="server" ID="BannerControl" />
<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <uc1:MovieListSwiperControl runat="server" ID="MovieListSwiperControl" IdTheLoai='<%#:Eval("ID") %>' Title='<%#:Eval("TheLoai") %>' />
    </ItemTemplate>
</asp:Repeater>
<script type="text/javascript">
    var numSlide
    if ($(window).width() <= 360)
        numSlide = 2;
    else if ($(window).width() <= 576)
        numSlide = 3;
    else if ($(window).width() <= 720)
        numSlide = 4;
    else numSlide = 5;
    const swiper2 = new Swiper('.list-movie__container', {
        // Optional parameters
        slidesPerView: numSlide,
        spaceBetween: 30,
        slidesPerGroup: numSlide,
        loop: true,
        loopFillGroupWithBlank: true,
        // Pagination
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },

        // Navigation arrows
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
    });
</script>