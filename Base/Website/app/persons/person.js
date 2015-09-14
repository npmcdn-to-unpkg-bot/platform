var App;
(function (App) {
    var Person;
    (function (Person) {
        var PersonController = (function () {
            function PersonController($rootScope, $scope, $http, allorsService) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$http = $http;
                this.allorsService = allorsService;
                this.refresh();
                this.$scope.$on("refresh", function () { _this.refresh(); });
            }
            PersonController.prototype.save = function () {
                var _this = this;
                var save = this.context.workspace.save();
                this.$http.post('/Angular/Save', { objects: save.objects }).then(function (response) {
                    _this.$rootScope.$broadcast("refresh");
                });
            };
            PersonController.prototype.refresh = function () {
                var _this = this;
                this.$http.post('/Angular/Person', {}).then(function (response) {
                    _this.allorsService.load(response.data)
                        .then(function (context) {
                        _this.context = context;
                        _this.root = context.objectByName["root"];
                    });
                });
            };
            PersonController.$inject = ["$rootScope", "$scope", "$http", "allorsService"];
            return PersonController;
        })();
        angular
            .module("app")
            .controller("personController", PersonController);
    })(Person = App.Person || (App.Person = {}));
})(App || (App = {}));
//# sourceMappingURL=person.js.map