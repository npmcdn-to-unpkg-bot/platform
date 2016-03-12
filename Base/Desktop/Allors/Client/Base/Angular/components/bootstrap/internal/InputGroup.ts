namespace Allors.Bootstrap {
    export class InputGroupTemplate {
        static name = "allors/bootstrap/input-group";

        static bootstrap = 
`
<div ng-class="$ctrl.form.horizontal ? 'col-sm-6' : ''">
<ng-transclude/>
</div>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(InputGroupTemplate.name, InputGroupTemplate.bootstrap);
        }
    }

    class InputGroupComponent {
        field: Bootstrap.Field;

        static $inject = ["$log"];
        constructor($log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("bInputGroup", {
            controller: InputGroupComponent,
            transclude: true,
            templateUrl: InputGroupTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                field: "<"
            }
        } as any);
}
