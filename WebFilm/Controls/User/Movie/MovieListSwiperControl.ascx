<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MovieListSwiperControl.ascx.cs" Inherits="WebFilm.Controls.User.Movie.MovieListSwiperControl" %>

<div class="list-movie__container swiper-container">
    <div class="d-flex flex-row justify-content-between">
        <h4 class="list-movie__title"><%=this.Title %></h4>
        <p><a href="?index=theloai&theloai=<%=this.IdTheLoai %>">Xem thêm</a></p>
    </div>

    <div class="swiper-wrapper">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div class="swiper-slide movie-card">
                    <div class="movie-card__img">
                        <a href="?index=phim&phim=<%#: Eval("ID") %>">
                            <img src="/Upload/Image/<%#:Eval("UrlHinh") %>"
                                loading="lazy" />
                            <i class="far fa-play-circle"></i>
                        </a>
                    </div>
                    <div class="movie-card__detail">
                        <a href="#" class="moive-card__detail__title-vi"><%#:Eval("TenPhim_vi") %></a>
                        <a href="#" class="moive-card__detail__title-en"><%#:Eval("TenPhim_en") %></a>
                        <a href="#" class="viewd"><i class="fas fa-eye"></i><%#:Eval("LuotXem") %></a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <!-- Add Pagination -->
    <div class="swiper-pagination"></div>
    <!-- Add Arrows -->
    <div class="swiper-button-next"></div>
    <div class="swiper-button-prev"></div>
</div>