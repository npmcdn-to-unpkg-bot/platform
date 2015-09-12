var App;
(function (App) {
    var Common;
    (function (Common) {
        var Services;
        (function (Services) {
            var AllorsService = (function () {
                function AllorsService($http) {
                    this.$http = $http;
                }
                AllorsService.$inject = ["$http"];
                return AllorsService;
            })();
            Services.AllorsService = AllorsService;
            angular
                .module("app")
                .service("allorsService", AllorsService);
        })(Services = Common.Services || (Common.Services = {}));
    })(Common = App.Common || (App.Common = {}));
})(App || (App = {}));
//# sourceMappingURL=allorsService.js.map