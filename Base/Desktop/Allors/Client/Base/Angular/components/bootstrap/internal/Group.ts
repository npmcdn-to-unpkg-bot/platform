namespace Allors.Bootstrap {
    export class GroupTemplate {
        static name = "allors/bootstrap/group";

        static bootstrap = 
`<div class="form-group" ng-if="$ctrl.field.canRead">
<ng-transclude/>
</div>`;

        static register(templateCache: ng.ITemplateCacheService) {
            templateCache.put(GroupTemplate.name, GroupTemplate.bootstrap);
        }
    }

    class GroupController {
        field: Bootstrap.Field;

        static $inject = ["$log"];
        constructor($log: ng.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("bGroup", {
            controller: GroupController,
            transclude: true,
            templateUrl: ["$element", "$attrs", () => GroupTemplate.name],
            bindings: {
                field: "<f"
            }
        } as any);
}
