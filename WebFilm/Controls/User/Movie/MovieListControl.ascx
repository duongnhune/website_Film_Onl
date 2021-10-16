<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MovieListControl.ascx.cs" Inherits="WebFilm.Controls.User.Movie.MovieListControl" %>

<div class="list-movie__container">
    <h4 class="list-movie__title">Phim <%=this.Title %></h4>
    <div class="list-movie__grid">
        <asp:ListView ID="rptMovieList" runat="server">
            <ItemTemplate>
                <div class="movie-card">
                    <div class="movie-card__img">
                        <a href="?index=phim&phim=<%#: Eval("ID") %>">
                            <img href="" src="/Upload/Image/<%#:Eval("UrlHinh") %>"
                                loading="lazy" />
                            <i class="far fa-play-circle"></i>
                        </a>
                    </div>
                    <div class="movie-card__detail">
                        <a href="#" class="moive-card__detail__title-vi"><%#:Eval("TenPhim_vi") %></a>
                        <a href="#" class="moive-card__detail__title-en"><%#:Eval("TenPhim_en") %></a>
                        <a href="#" class="viewed"><i class="fas fa-eye"></i><%#:Eval("LuotXem") %></a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>

    <div class="page-nav">
        <asp:DataPager ID="DataPager1" QueryStringField="page" PageSize="15" PagedControlID="rptMovieList" runat="server">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Link" RenderNonBreakingSpacesBetweenControls="true" ShowPreviousPageButton="true" ShowNextPageButton="false" PreviousPageText="&laquo;" ButtonCssClass="page-button" />
                <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="page-button" NextPreviousButtonCssClass="page-button" CurrentPageLabelCssClass="current-button" />
                <asp:NextPreviousPagerField ButtonType="Link" RenderNonBreakingSpacesBetweenControls="true" ShowNextPageButton="true" ShowPreviousPageButton="false" NextPageText="&raquo;" ButtonCssClass="page-button" />
            </Fields>
        </asp:DataPager>
    </div>
</div>