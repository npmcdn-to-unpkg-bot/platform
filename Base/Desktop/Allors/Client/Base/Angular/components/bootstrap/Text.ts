namespace Allors.Bootstrap {
    export class TextTemplate {
        static name = "allors/bootstrap/text";

        static bootstrap = 
`
<b-group f="$ctrl">
    <b-label f="$ctrl"/> 
    <input placeholder="{{$ctrl.placeholder}}" class="form-control"
           ng-model="$ctrl.role"
           ng-disabled="!$ctrl.canWrite"
           ng-required="$ctrl.roleType.isRequired">
</b-group>
`;

        static register(templateCache: ng.ITemplateCacheService) {
            templateCache.put(TextTemplate.name, TextTemplate.bootstrap);
        }
    }

    class TextController extends Bootstrap.Field {
        static $inject = ["$log"];
        constructor($log: angular.ILogService) {
            super($log);
        }
    }

    angular
        .module("allors")
        .component("bText", {
            controller: TextController,
            templateUrl: ["$element", "$attrs", () => TextTemplate.name],
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<o",
                roleTypeName: "@r"
            }
        } as any);
}
