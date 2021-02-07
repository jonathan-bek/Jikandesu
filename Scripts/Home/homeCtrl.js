"use strict";
var Home;
(function (Home) {
    "use strict";
    var HomeCtrl = (function () {
        function HomeCtrl($scope, $http, testCtrl) {
            this.$scope = $scope;
            this.$http = $http;
            this.testCtrl = testCtrl;
            this.testCtrl = "TEST CTRL";
            this.$scope.testCtrlScope = "TEST CTRL SCOPE";
        }
        HomeCtrl.prototype.getInt = function () {
            return 1;
        };
        HomeCtrl.$inject = ["$scope", "$http"];
        return HomeCtrl;
    }());
    Home.HomeCtrl = HomeCtrl;
})(Home || (Home = {}));
//# sourceMappingURL=homeCtrl.js.map