var DetailService = function () {

    var followArtist = function (userId, done, fail) {
        console.log("hi");
        $.post("/api/followings", { followeeId: userId})
            .done(done)
            .fail(fail);
    }

    var unFollowArtist = function (userId, done, fail) {
        console.log("hi");
        $.ajax({
            url: "/api/followings/" + userId,
            method: "DELETE"
            })
            .done(done)
            .fail(fail);
    }

    return {
        followArtist: followArtist,
        unFollowArtist: unFollowArtist
    }
}();
