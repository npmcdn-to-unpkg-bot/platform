namespace Allors.Bootstrap {
    export class FormTemplate {

        static form =
`<form>
{{$ctrl.horizontal}}
<ng-transclude />
</form>`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put("allors/bootstrap/form", FormTemplate.form);
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
            templateUrl: ["$element", "$attrs", ($element: angular.IAugmentedJQuery, $attrs: angular.IAttributes) => "allors/bootstrap/form"],
            bindings: {
                horizontal: "<"
            }
        } as any);
}
