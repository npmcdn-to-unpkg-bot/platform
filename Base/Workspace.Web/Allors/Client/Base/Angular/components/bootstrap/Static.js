var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Allors;
(function (Allors) {
    var Bootstrap;
    (function (Bootstrap) {
        var StaticTemplate = (function () {
            function StaticTemplate() {
            }
            StaticTemplate.createDefaultView = function () {
                return "\n<p class=\"form-control-static\" ng-bind=\"$ctrl.role\"></p>\n";
            };
            StaticTemplate.register = function (templateCache, view) {
                if (view === void 0) { view = StaticTemplate.createDefaultView(); }
                templateCache.put(StaticTemplate.name, view);
            };
            StaticTemplate.name = "allors/bootstrap/static";
            return StaticTemplate;
        }());
        Bootstrap.StaticTemplate = StaticTemplate;
        var StaticController = (function (_super) {
            __extends(StaticController, _super);
            function StaticController($log, $translate) {
                _super.call(this, $log, $translate);
            }
            StaticController.bindings = {
                object: "<",
                relation: "@"
            };
            StaticController.$inject = ["$log", "$translate"];
            return StaticController;
        }(Bootstrap.Field));
        Bootstrap.StaticController = StaticController;
        angular
            .module("allors")
            .component("bStatic", {
            controller: StaticController,
            templateUrl: StaticTemplate.name,
            require: Bootstrap.FormController.require,
            bindings: StaticController.bindings
        });
    })(Bootstrap = Allors.Bootstrap || (Allors.Bootstrap = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=Static.js.map