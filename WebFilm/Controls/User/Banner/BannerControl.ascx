<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerControl.ascx.cs" Inherits="WebFilm.Controls.User.Banner.BannerControl" %>

<!-- Slider main container -->
<div id="swiper-banner" class="swiper-container">
    <div class="swiper-wrapper">
        <!-- Slides -->
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div class="swiper-slide">
                    <img src="/Upload/Banner/<%#:Container.DataItem %>" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="swiper-pagination"></div>
    <div class="swiper-button-prev"></div>
    <div class="swiper-button-next"></div>
</div>
<script type="text/javascript">
    const swiper = new Swiper('#swiper-banner', {
        // Optional parameters
        direction: 'horizontal',
        loop: true,
        autoplay: {
            delay: 2500,
            disableOnInteraction: false,
        },
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