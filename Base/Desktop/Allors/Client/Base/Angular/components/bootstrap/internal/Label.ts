namespace Allors.Bootstrap {
    export class LabelTemplate {
        static name = "allors/bootstrap/label";

        static bootstrap = 
`<label>{{$ctrl.field.label}}</label>`;

        static register(templateCache: ng.ITemplateCacheService) {
            templateCache.put(LabelTemplate.name, LabelTemplate.bootstrap);
        }
    }

    class LabelController {
        field: Bootstrap.Field;

        static $inject = ["$log"];
        constructor($log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("bLabel", {
            controller: LabelController,
            templateUrl: ["$element", "$attrs", () => LabelTemplate.name],
            bindings: {
                field: "<f"
            }
        } as any);
}
