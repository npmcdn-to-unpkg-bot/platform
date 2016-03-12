namespace Allors.Bootstrap {
    export class GroupTemplate {
        static name = "allors/bootstrap/group";

        static bootstrap = 
`<div class="form-group" ng-if="$ctrl.field.canRead">
<ng-transclude/>
</div>`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(GroupTemplate.name, GroupTemplate.bootstrap);
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
            templateUrl: ["$element", "$attrs", () => GroupTemplate.name],
            bindings: {
                field: "<f"
            }
        } as any);
}
