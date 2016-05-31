namespace Allors.Bootstrap.Role {
    export class LabeledInputTemplate {
        static name = "allors/bootstrap/role/labeled-input";

        private static view = 
`
<div ng-class="$ctrl.form.horizontal ? 'col-sm-8' : ''">
<ng-transclude/>
</div>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(LabeledInputTemplate.name, LabeledInputTemplate.view);
        }
    }

    class LabeledInputComponent {
        field: Field;

        static $inject = ["$log"];
        constructor($log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("bRoleLabeledInput", {
            controller: LabeledInputComponent,
            transclude: true,
            templateUrl: LabeledInputTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                field: "<"
            }
        } as any);
}
