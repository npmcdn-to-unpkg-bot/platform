var Allors;
(function (Allors) {
    var Bootstrap;
    (function (Bootstrap) {
        var LabelTemplate = (function () {
            function LabelTemplate() {
            }
            LabelTemplate.register = function (templateCache) {
                templateCache.put(LabelTemplate.name, LabelTemplate.view);
            };
            LabelTemplate.name = "allors/bootstrap/label";
            LabelTemplate.view = "\n<label class=\"control-label\" ng-class=\"$ctrl.form.horizontal ? 'col-sm-2' : '' \">{{$ctrl.field.label}}\n    <span ng-if=\"$ctrl.field.help\" class=\"fa fa-question-circle\"\n          uib-tooltip=\"{{$ctrl.field.help}}\"\n          tooltip-placement=\"right\">\n    </span>\n</label>\n";
            return LabelTemplate;
        }());
        Bootstrap.LabelTemplate = LabelTemplate;
        var LabelComponent = (function () {
            function LabelComponent($log) {
            }
            LabelComponent.$inject = ["$log"];
            return LabelComponent;
        }());
        angular
            .module("allors")
            .component("bLabel", {
            controller: LabelComponent,
            templateUrl: LabelTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                field: "<"
            }
        });
    })(Bootstrap = Allors.Bootstrap || (Allors.Bootstrap = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=Label.js.map