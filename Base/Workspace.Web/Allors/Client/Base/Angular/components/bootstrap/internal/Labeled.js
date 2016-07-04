var Allors;
(function (Allors) {
    var Bootstrap;
    (function (Bootstrap) {
        var LabeledTemplate = (function () {
            function LabeledTemplate() {
            }
            LabeledTemplate.register = function (templateCache) {
                templateCache.put(LabeledTemplate.name, LabeledTemplate.view);
            };
            LabeledTemplate.name = "allors/bootstrap/labeled";
            LabeledTemplate.view = "<div class=\"form-group\" ng-if=\"$ctrl.field.canRead\">\n<ng-transclude/>\n</div>";
            return LabeledTemplate;
        }());
        Bootstrap.LabeledTemplate = LabeledTemplate;
        var LabeledComponent = (function () {
            function LabeledComponent($log) {
            }
            LabeledComponent.$inject = ["$log"];
            return LabeledComponent;
        }());
        angular
            .module("allors")
            .component("bLabeled", {
            controller: LabeledComponent,
            transclude: true,
            templateUrl: LabeledTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                field: "<"
            }
        });
    })(Bootstrap = Allors.Bootstrap || (Allors.Bootstrap = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=Labeled.js.map