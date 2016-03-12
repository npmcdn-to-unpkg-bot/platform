namespace Allors.Bootstrap {
    export class FormTemplate {

        static name = "allors/bootstrap/form";

        static form =
`<form ng-class="$ctrl.horizontal ? 'form-horizontal' : ''">
<ng-transclude />
</form>`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(FormTemplate.name, FormTemplate.form);
        }
    }

    export class FormComponent  {

        horizontal: boolean;

        static $inject = ["$log"];
        constructor(private $log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("bForm", {
            controller: FormComponent,
            templateUrl: FormTemplate.name,
            transclude: true,
            bindings: {
                horizontal: "<"
            }
        } as any);
}
