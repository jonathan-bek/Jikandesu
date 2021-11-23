window.onload = function () {
    //Make "Enter" submit search box
    var searchBox = document.getElementById("searchBox");
    searchBox.addEventListener("keyup", function (event) {
        // Number 13 is the "Enter" key on the keyboard
        if (event.keyCode === 13) {
            event.preventDefault();
            onClickSearch();
        }
    });
}

function onClickSearch() {
    var searchValue = document.getElementById("searchBox").value;
    searchValue = searchValue.replace("https://", "");
    searchValue = searchValue.replace("http://", "");
    var url = window.location.origin + "/Manga/Manga/Page?searchUrl=" + searchValue;
    //encodeURIComponent(searchValue);
    window.location.href = url;
}
