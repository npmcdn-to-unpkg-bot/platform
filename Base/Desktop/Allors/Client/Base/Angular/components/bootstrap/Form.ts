namespace Allors.Bootstrap {
    export class FormTemplates {

        static form =
`<form>
{{$ctrl.horizontal}}
<ng-transclude />
</form>`;

        static register(templateCache: ng.ITemplateCacheService) {
            templateCache.put("allors/bootstrap/form", FormTemplates.form);
        }
    }

    export class FormController  {

        horizontal: boolean;

        static $inject = ["$log"];
        constructor(private $log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("bForm", {
            controller: FormController,
            transclude: true,
            templateUrl: ["$element", "$attrs", ($element: ng.IAugmentedJQuery, $attrs: ng.IAttributes) => "allors/bootstrap/form"],
            bindings: {
                horizontal: "<"
            }
        } as any);
}
