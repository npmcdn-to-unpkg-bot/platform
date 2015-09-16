var App;
(function (App) {
    var Common;
    (function (Common) {
        var Services;
        (function (Services) {
            var AllorsService = (function () {
                function AllorsService($http, $q) {
                    this.$http = $http;
                    this.$q = $q;
                    this.database = new Allors.Database(Allors.Meta.population);
                }
                AllorsService.prototype.load = function (response) {
                    var _this = this;
                    var defer = this.$q.defer();
                    var requireLoadIds = this.database.check(response);
                    if (requireLoadIds.objects.length > 0) {
                        this.$http.post('/Angular/Load', requireLoadIds)
                            .then(function (v) {
                            var loadResponse = v.data;
                            _this.database.load(loadResponse);
                            defer.resolve(_this.createContext(response));
                        })
                            .catch(function (v) {
                            throw v;
                        });
                    }
                    else {
                        defer.resolve(this.createContext(response));
                    }
                    return defer.promise;
                };
                AllorsService.prototype.createContext = function (response) {
                    var workspace = new Allors.Workspace(this.database);
                    var namedObjects = {};
                    _.map(response.namedObjects, function (v, k) {
                        namedObjects[k] = workspace.get(v);
                    });
                    var namedCollections = {};
                    _.map(response.namedCollections, function (v, k) {
                        namedCollections[k] = _.map(v, function (obj) { return workspace.get(obj); });
                    });
                    var namedValues = {};
                    _.map(response.namedValues, function (v, k) {
                        namedObjects[k] = v;
                    });
                    var context = new Allors.Context(workspace, namedObjects, namedCollections, namedValues);
                    return context;
                };
                AllorsService.$inject = ["$http", "$q", "$rootScope"];
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