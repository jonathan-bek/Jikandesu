var searchBox = document.getElementById("searchBox");
//var searchBoxBtn = document.getElementById("searchBoxBtn");

function onClickSearch() {
    var searchValue = document.getElementById("searchBox").value;
    searchValue = searchValue.replace("https://", "");
    searchValue = searchValue.replace("http://", "");
    var url = window.location.origin + "/Manga/Manga/Page?searchUrl=" + searchValue;
    //encodeURIComponent(searchValue);
    window.location.href = url;
}
