"use strict";
var Manga;
(function (Manga) {
    "use strict";
    var MangaDataService = (function () {
        function MangaDataService($http, $window) {
            this.$http = $http;
            this.$window = $window;
        }
        MangaDataService.prototype.getUserManga = function () {
            var url = "/Manga/MangaApi/GetUserManga";
            return this.$http.get(url)
                .then(function (res) {
                return res.data;
            }, function (msg) { return alert("Error: You must be signed in to use this feature."); });
        };
        MangaDataService.prototype.getMangaPage = function (mangaUrl) {
            var url = "/Manga/MangaApi/GetMangaPage";
            var postObj = { mangaUrl: mangaUrl };
            return this.$http.post(url, postObj)
                .then(function (res) {
                return res.data;
            }, function (msg) { return console.log("ERROR:", msg); });
        };
        MangaDataService.prototype.saveMangaPage = function (mangaPage) {
            var url = this.$window.location.origin + "/Manga/MangaApi/SaveMangaPage";
            var postObj = { mangaPageStr: JSON.stringify(mangaPage) };
            return this.$http.post(url, postObj)
                .then(function (res) {
                alert(res.data);
            }, function (msg) { return alert("Error: You must be signed in to use this feature."); });
        };
        MangaDataService.$inject = ["$http", "$window"];
        return MangaDataService;
    }());
    Manga.MangaDataService = MangaDataService;
})(Manga || (Manga = {}));
var Manga;
(function (Manga) {
    "use strict";
    var UserMangaCtrl = (function () {
        function UserMangaCtrl($scope, mangaDataService) {
            this.$scope = $scope;
            this.mangaDataService = mangaDataService;
            this.mangaPages = [];
        }
        UserMangaCtrl.prototype.$onInit = function () {
            this.getUserManga();
        };
        UserMangaCtrl.prototype.getUserManga = function () {
            var _this = this;
            this.mangaDataService.getUserManga()
                .then(function (data) {
                _this.mangaPages = data;
            });
        };
        UserMangaCtrl.inject = ["mangaDataService"];
        return UserMangaCtrl;
    }());
    Manga.UserMangaCtrl = UserMangaCtrl;
})(Manga || (Manga = {}));
var Manga;
(function (Manga) {
    "use strict";
    var app = angular.module("userMangaApp", []);
    app.config(["$locationProvider", function ($locationProvider) {
            $locationProvider.html5Mode({ enabled: true, requireBase: false });
        }]);
    app.controller("userMangaCtrl", Manga.UserMangaCtrl);
    app.service("mangaDataService", Manga.MangaDataService);
})(Manga || (Manga = {}));
//# sourceMappingURL=userMangaBundle.js.map