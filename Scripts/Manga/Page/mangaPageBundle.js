"use strict";
var Manga;
(function (Manga) {
    "use strict";
    var MangaPageCtrl = (function () {
        function MangaPageCtrl($scope, $window, mangaDataService, $uibModal) {
            this.$scope = $scope;
            this.$window = $window;
            this.mangaDataService = mangaDataService;
            this.$uibModal = $uibModal;
        }
        MangaPageCtrl.prototype.$onInit = function () {
            this.getMangaPage(this.getMangaUrl());
        };
        MangaPageCtrl.prototype.openModal = function () {
            var modalInstance = this.$uibModal.open({
                template: "<div class=\"modal-header\"> \n                     <h3 class=\"modal-title\" id=\"modal-title\">Modal Title</h3> \n                    </div> \n                    <div class=\"modal-body\" id=\"modal-body\"> \n                    Modal Content here..! \n                    </div> \n                    <div class=\"modal-footer\"> \n                     <button class=\"btn btn-primary\" type=\"button\" ng-click=\"test()\">OK</button> \n                    <button class=\"btn btn-warning\" type=\"button\" ng-click=\"cancel()\">Cancel</button> \n                    </div>",
                controller: function ($scope) {
                    $scope.test = function () {
                        console.log("lol");
                    },
                        $scope.cancel = function () {
                            modalInstance.dismiss();
                        };
                },
                scope: this.$scope,
                resolve: {}
            });
            modalInstance.result.then(function (result) {
            }, function () {
            });
        };
        MangaPageCtrl.prototype.getMangaPage = function (url) {
            var _this = this;
            this.mangaDataService.getMangaPage(url)
                .then(function (data) {
                _this.mangaPage = data;
            });
        };
        MangaPageCtrl.prototype.saveMangaPage = function () {
            this.mangaDataService.saveMangaPage(this.mangaPage);
        };
        MangaPageCtrl.prototype.getMangaUrl = function () {
            var mangaUrl = this.$window.location.search.replace("?searchUrl=", "");
            if (mangaUrl.indexOf("http") < 0) {
                mangaUrl = "https://" + mangaUrl;
            }
            return mangaUrl;
        };
        MangaPageCtrl.$inject = ["$scope", "$window", "mangaDataService", "$uibModal"];
        return MangaPageCtrl;
    }());
    Manga.MangaPageCtrl = MangaPageCtrl;
})(Manga || (Manga = {}));
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
    var app = angular.module("mangaPageApp", ['ui.bootstrap']);
    app.config(["$locationProvider", function ($locationProvider) {
            $locationProvider.html5Mode({ enabled: true, requireBase: false });
        }]);
    app.controller("mangaPageCtrl", Manga.MangaPageCtrl);
    app.service("mangaDataService", Manga.MangaDataService);
})(Manga || (Manga = {}));
//# sourceMappingURL=mangaPageBundle.js.map