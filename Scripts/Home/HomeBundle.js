"use strict";
var JdAnime;
(function (JdAnime) {
    "use strict";
    var AnimeHomeCtrl = (function () {
        function AnimeHomeCtrl($scope, mangaDataService) {
            this.$scope = $scope;
            this.mangaDataService = mangaDataService;
            this.mangaUrl = "";
        }
        AnimeHomeCtrl.prototype.getMangaPage = function () {
            var _this = this;
            this.mangaDataService.getMangaPage(this.mangaUrl)
                .then(function (data) {
                _this.mangaPage = data;
            });
        };
        AnimeHomeCtrl.prototype.saveMangaPage = function () {
            this.mangaDataService.saveMangaPage(this.mangaPage);
        };
        AnimeHomeCtrl.$inject = ["$scope", "mangaDataService"];
        return AnimeHomeCtrl;
    }());
    JdAnime.AnimeHomeCtrl = AnimeHomeCtrl;
})(JdAnime || (JdAnime = {}));
var Manga;
(function (Manga) {
    "use strict";
    var MangaDataService = (function () {
        function MangaDataService($http) {
            this.$http = $http;
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
            var url = "Manga/MangaApi/SaveMangaPage";
            var postObj = { mangaPageStr: JSON.stringify(mangaPage) };
            return this.$http.post(url, postObj)
                .then(function (res) {
                alert(res.data);
            }, function (msg) { return alert("Error: You must be signed in to use this feature."); });
        };
        MangaDataService.$inject = ["$http"];
        return MangaDataService;
    }());
    Manga.MangaDataService = MangaDataService;
})(Manga || (Manga = {}));
var JdRowDirective;
(function (JdRowDirective_1) {
    var JdRowDirective = (function () {
        function JdRowDirective() {
            this.restrict = "AE";
            this.template = "\n            <div ng-repeat=\"item in value\">\n                <img ng-src=\"{{item.image_url}}\" />\n                <p>{{item.title}}</p>\n                <p>{{item.score}}</p>\n            </div>";
            this.scope = {
                value: "=value"
            };
            this.replace = true;
            this.link = function (scope, element, attrs, ctrl) {
            };
        }
        JdRowDirective.factory = function () {
            var directive = function () { return new JdRowDirective(); };
            directive.$inject = [];
            return directive;
        };
        return JdRowDirective;
    }());
    JdRowDirective_1.JdRowDirective = JdRowDirective;
})(JdRowDirective || (JdRowDirective = {}));
var Home;
(function (Home) {
    "use strict";
    var app = angular.module("homeApp", []);
    app.config(["$locationProvider", function ($locationProvider) {
            $locationProvider.html5Mode({ enabled: true, requireBase: false });
        }]);
    app.controller("animeHomeCtrl", JdAnime.AnimeHomeCtrl);
    app.service("mangaDataService", Manga.MangaDataService);
})(Home || (Home = {}));
//# sourceMappingURL=HomeBundle.js.map