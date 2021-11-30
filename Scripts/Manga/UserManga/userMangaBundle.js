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
            var _this = this;
            var url = "/Manga/MangaApi/GetUserManga";
            return this.$http.get(url)
                .then(function (res) {
                return res.data;
            }, function (msg) {
                _this.handleError(msg);
            });
        };
        MangaDataService.prototype.getMangaPage = function (mangaUrl) {
            var _this = this;
            var url = "/Manga/MangaApi/GetMangaPage";
            var postObj = { mangaUrl: mangaUrl };
            return this.$http.post(url, postObj)
                .then(function (res) {
                return res.data;
            }, function (msg) {
                _this.handleError(msg);
            });
        };
        MangaDataService.prototype.saveMangaPage = function (mangaPage) {
            var _this = this;
            var url = "/Manga/MangaApi/SaveMangaPage";
            var postObj = { mangaPageStr: JSON.stringify(mangaPage) };
            return this.$http.post(url, postObj)
                .then(function (res) {
                alert(res.data);
            }, function (msg) {
                _this.handleError(msg);
            });
        };
        MangaDataService.prototype.handleError = function (res) {
            var msg = "";
            if (res.status == -1) {
                msg = "Network error: You must be signed in to use this feature.";
            }
            else {
                var errorMsgs = res.data;
                msg = errorMsgs.join();
            }
            alert(msg);
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
        function UserMangaCtrl($scope, $window, mangaDataService) {
            this.$scope = $scope;
            this.$window = $window;
            this.mangaDataService = mangaDataService;
            this.mangaPages = [];
        }
        UserMangaCtrl.prototype.$onInit = function () {
            this.getUserManga();
        };
        UserMangaCtrl.prototype.redirectToManga = function (page) {
            var mangaUrl = page.Url;
            mangaUrl = mangaUrl.replace("https://", "");
            mangaUrl = mangaUrl.replace("http://", "");
            var redirect = this.$window.location.origin + "/Manga/Manga/Page?searchUrl=" + mangaUrl;
            window.location.href = redirect;
        };
        UserMangaCtrl.prototype.getUserManga = function () {
            var _this = this;
            this.mangaDataService.getUserManga()
                .then(function (data) {
                _this.mangaPages = data;
            });
        };
        UserMangaCtrl.inject = ["$scope", "$window", "mangaDataService"];
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