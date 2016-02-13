module App {

    class FormController {
        static $inject = ["$log"];
        constructor(private $log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("aForm", {
            controller: FormController,
            templateUrl: "allors/client/base/angular/components/form/form.html",
            bindings: {
            },
            transclude: true
        });
}
