namespace Allors.Bootstrap {
    export class InputGroupTemplate {
        static name = "allors/bootstrap/input-group";

        private static view = 
`
<div ng-class="$ctrl.form.horizontal ? 'col-sm-8' : ''">
<ng-transclude/>
</div>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(InputGroupTemplate.name, InputGroupTemplate.view);
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
