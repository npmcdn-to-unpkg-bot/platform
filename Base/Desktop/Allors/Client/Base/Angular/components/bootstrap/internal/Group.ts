namespace Allors.Bootstrap {
    export class GroupTemplate {
        static name = "allors/bootstrap/group";

        private static view = 
`<div class="form-group" ng-if="$ctrl.field.canRead">
<ng-transclude/>
</div>`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(GroupTemplate.name, GroupTemplate.view);
        }
    }

    class GroupComponent {
        field: Bootstrap.Field;

        static $inject = ["$log"];
        constructor($log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("bGroup", {
            controller: GroupComponent,
            transclude: true,
            templateUrl: GroupTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                field: "<"
            }
        } as any);
}
