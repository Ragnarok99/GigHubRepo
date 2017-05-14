var DetailsController = function (detailService) {
    var button;

    var init = function () {
        $(".js-toggle-follow").click(toggleArtist);
    }

    var toggleArtist = function (e) {

        button = $(e.target);
   
        var userId = button.attr("data-user-id");

        if (button.hasClass("btn-default")) {
            detailService.followArtist(userId, done, fail);
        } else
            detailService.unFollowArtist(userId, done, fail);

    }
    var done = function () {
        var text = (button.text()) === "btn-default" ? "Following" : "Follow";
        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    }

    var fail = function (err) {
        console.log(err);
    }





    return {
        init: init

    }
}(DetailService);