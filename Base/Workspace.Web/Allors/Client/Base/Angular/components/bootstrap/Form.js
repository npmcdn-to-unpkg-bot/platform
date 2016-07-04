var Allors;
(function (Allors) {
    var Bootstrap;
    (function (Bootstrap) {
        var FormTemplate = (function () {
            function FormTemplate() {
            }
            FormTemplate.register = function (templateCache) {
                templateCache.put(FormTemplate.name, FormTemplate.view);
            };
            FormTemplate.name = "allors/bootstrap/form";
            FormTemplate.view = "<form ng-class=\"$ctrl.horizontal ? 'form-horizontal' : ''\">\n<ng-transclude />\n</form>";
            return FormTemplate;
        }());
        Bootstrap.FormTemplate = FormTemplate;
        var FormController = (function () {
            function FormController($log) {
                this.$log = $log;
            }
            FormController.require = {
                form: "^bForm"
            };
            FormController.bindings = {
                horizontal: "<"
            };
            FormController.$inject = ["$log"];
            return FormController;
        }());
        Bootstrap.FormController = FormController;
        angular
            .module("allors")
            .component("bForm", {
            controller: FormController,
            templateUrl: FormTemplate.name,
            transclude: true,
            bindings: FormController.bindings
        });
    })(Bootstrap = Allors.Bootstrap || (Allors.Bootstrap = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=Form.js.map