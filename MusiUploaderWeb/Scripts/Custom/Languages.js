$(document).ready(function () {
    var lang = MusiUploaderWeb.Cookies.getCookie("LangForMusiUploaderWeb");
    $(".setLang[data-lang='" + lang + "'] img").addClass("active-lang");

    $(".setLang").on("click", function (event) {
        var lang = $(this).attr("data-lang");
        MusiUploaderWeb.Cookies.setCookie("LangForMusiUploaderWeb", lang, 30);
        location.reload(true);
    })
});