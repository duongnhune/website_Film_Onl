<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MovieDetailControl.ascx.cs" Inherits="WebFilm.Controls.User.Movie.MovieDetailControl" %>
<%@ Register Src="~/Controls/User/Movie/MovieListSwiperControl.ascx" TagPrefix="uc1" TagName="MovieListSwiperControl" %>

<div class="info-movie row">
    <div class="info-movie__poster col-md-4 col-12">
        <a href="#">
            <img
                src="<%=this.SrcImg %>" /></a>
    </div>
    <div class="col-md-8 col-12">
        <h2 class="info-movie__title-vi">
            <asp:Literal ID="ltrTenPhim_vi" runat="server"></asp:Literal></h2>
        <h3 class="info-movie__title-en">
            <asp:Literal ID="ltrTenPhim_en" runat="server"></asp:Literal></h3>
        <div class="btn-group">
            <asp:LinkButton ID="btnPlay" runat="server" CssClass="btn btn-play" href="#player" OnClientClick="playVideo()"><i class="fas fa-play"></i>PLAY</asp:LinkButton>
            <button class="btn">
                <i class="fas fa-eye"></i>
                <asp:Literal ID="ltrLuotXem" runat="server"></asp:Literal></button>
            <button class="btn">
                <i class="fas fa-heart"></i>
                <asp:Literal ID="ltrLuotThich" runat="server"></asp:Literal></button>
        </div>

        <p class="description">
            <asp:Literal ID="ltrMoTa" runat="server"></asp:Literal>
        </p>

        <ul class="more-info">
            <li>
                <span>Thời lượng: </span>
                <asp:Literal ID="ltrThoiLuong" runat="server"></asp:Literal>
            </li>
            <li><span>Đạo diễn: </span>
                <asp:Literal ID="ltrDaoDien" runat="server"></asp:Literal></li>
            <li><span>Quốc gia: </span>
                <asp:Literal ID="ltrQuocGia" runat="server"></asp:Literal></li>
            <li><span>Thể loại: </span>
                <asp:Literal ID="ltrTheLoai" runat="server"></asp:Literal>
            </li>
            <li><span>Phát hành: </span>
                <asp:Literal ID="ltrPhatHanh" runat="server"></asp:Literal></li>
            <li><span>Diễn viên: </span>
                <asp:Literal ID="ltrDienVien" runat="server"></asp:Literal></li>
        </ul>
    </div>

    <iframe id="player" width="640" height="480" src="<%=this.SrcVideo %>" frameborder="0"
        allow="autoplay" allowfullscreen="true"></iframe>

    <uc1:MovieListSwiperControl runat="server" ID="MovieListSwiperControl" Title="Có thể bạn thích" />

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

        const player = document.getElementById('player');
        function playVideo() {
            player.scrollIntoView({ behavior: 'smooth' });
        }
    </script>
</div>