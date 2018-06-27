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
            $("#SearchResults").append("<div onclick=\"ChooseVideo('" + elem.id.videoId + "')\" class=\"video\"><img src=\"" + elem.snippet.thumbnails.high.url + "\" /><h1>" + elem.snippet.title + "</h1></div>");
        });
    });
}
function ChooseVideo(id) {
    $("[name=YoutubeVideo]").val(id);
    $("#VideoSearch")[0].close();
    $.ajax("https://www.googleapis.com/youtube/v3/videos?id=" + $("[name=YoutubeVideo]").val() + "&part=snippet&key=AIzaSyDLd-4mlOr7Ql1v2KZftgNNdoj-dRc6Fog").done((data) => {
        $("#video").css("background-image", "url(" + data.items[0].snippet.thumbnails.high.url + ")");
    });

}

// #endregion
$("dialog")[0].addEventListener('click', function (e) {
    if (e.target.tagName === 'DIALOG') $("dialog")[0].close()
});
$("#picture").click(() => {
    $("#fileUpload").trigger("click");
});

$("[data-link]").keyup((event) => {
    $($(event.target).attr("data-link")).text($(event.target).val());
});

$("#fileUpload").change(() => {
    var file = $("#fileUpload")[0].files[0];
    var reader = new FileReader();
    reader.addEventListener("load", function () {
        $("#picture").attr("src", reader.result);
        $("[name=Image]").val(reader.result);
    }, false);
    if (file) {
        reader.readAsDataURL(file);
    }
});
if ($("[name=Image]").val() != "") {
    $("#picture").attr("src", $("[name=Image]").val());
}

if ($("[name=YoutubeVideo]").val() != "") {
    $.ajax("https://www.googleapis.com/youtube/v3/videos?id=" + $("[name=YoutubeVideo]").val() + "&part=snippet&key=AIzaSyDLd-4mlOr7Ql1v2KZftgNNdoj-dRc6Fog").done((data) => {
        $("#video").css("background-image", "url(" + data.items[0].snippet.thumbnails.high.url + ")");
    });
}