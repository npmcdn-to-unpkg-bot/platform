var App;
(function (App) {
    var AllorsService = (function () {
        function AllorsService($log, $http, $q, $rootScope, toastr) {
            this.$log = $log;
            this.$http = $http;
            this.$q = $q;
            this.$rootScope = $rootScope;
            this.toastr = toastr;
            var prefix = "/Database/";
            var postfix = "/Pull";
            this.database = new Allors.Database(this.$http, this.$q, prefix, postfix);
            this.workspace = new Allors.Workspace(Allors.Data.metaPopulation);
        }
        AllorsService.$inject = ["$log", "$http", "$q", "$rootScope", "toastr"];
        return AllorsService;
    }());
    App.AllorsService = AllorsService;
    angular.module("app")
        .service("allorsService", AllorsService);
})(App || (App = {}));
//# sourceMappingURL=Allors.js.map