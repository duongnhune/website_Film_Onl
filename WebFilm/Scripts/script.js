////const swiper2 = new Swiper('.list-movie__container', {
////    // Optional parameters
////    slidesPerView: 5,
////    spaceBetween: 30,
////    slidesPerGroup: 5,
////    loop: true,
////    loopFillGroupWithBlank: true,
////    // Pagination
////    pagination: {
////        el: '.swiper-pagination',
////        clickable: true,
////    },

////    // Navigation arrows
////    navigation: {
////        nextEl: '.swiper-button-next',
////        prevEl: '.swiper-button-prev',
////    },
////});

$(function () {
    $('.navbar-item').click(function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
            return;
        }

        $('.navbar-item').removeClass('active');
        $(this).addClass('active');
    });

    var toggle = $('#toggleNavbar');
    menu = $('nav');
    menuHeight = menu.height();

    $(toggle).click(function (e) {
        e.preventDefault();
        menu.slideToggle('fast');
        if (document.getElementById('overlay').style.display == 'none')
            document.getElementById('overlay').style.display = "block";
        else document.getElementById('overlay').style.display = 'none';
    });

    $(window).resize(function () {
        var w = $(window).width();
        if (w > 320 && menu.is(':hidden')) {
            menu.removeAttr('style');
        }
    });

    $('#overlay').click(function (e) {
        e.preventDefault();
        menu.slideToggle('fast');
        document.getElementById('overlay').style.display = 'none';
    });
});

//const player = document.getElementById('player');
//function playVideo() {
//    player.scrollIntoView({ behavior: 'smooth' });
//}