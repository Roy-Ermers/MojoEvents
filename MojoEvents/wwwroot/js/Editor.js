// #region Videos
$("#video").click(() => {
    $("#VideoSearch")[0].showModal();
});
let Timeout;
$("#SearchQuery").keydown(() => {
    if (Timeout) clearTimeout(Timeout);

    Timeout = setTimeout(SearchVideo, 500);
});

function SearchVideo() {
    $.ajax("https://www.googleapis.com/youtube/v3/search?q=" + $("#SearchQuery").val() + "&part=snippet&type=video&maxResults=25&key=AIzaSyDLd-4mlOr7Ql1v2KZftgNNdoj-dRc6Fog").done((data) => {
        $("#SearchResults").html("");
        data.items.forEach((elem) => {
            $("#SearchResults").append("<div class=\"video\"><img src=\"" + elem.snippet.thumbnails.high.url + "\" /><h1>" + elem.snippet.title + "</h1></div>").click(() => ChooseVideo(elem.id.videoId));
        });
    });
}
function ChooseVideo(id) {
    $("[name=YoutubeVideo]").val(id);
    $("#VideoSearch")[0].close();
}

// #endregion

$("#picture").click(() => {
    $("#fileUpload").trigger("click");
})

$("#fileUpload").change(() => {

})