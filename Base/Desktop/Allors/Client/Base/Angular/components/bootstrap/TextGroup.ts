namespace Allors.Bootstrap {
    export class TextGroupTemplate {
        static name = "allors/bootstrap/text-group";

        private static view = 
`
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
        <input placeholder="{{$ctrl.placeholder}}" class="form-control"
               ng-model="$ctrl.role"
               ng-disabled="!$ctrl.canWrite"
               ng-required="$ctrl.roleType.isRequired">
    </b-input-group>
</b-group>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(TextGroupTemplate.name, TextGroupTemplate.view);
        }
    }

    class TextGroupController extends Bootstrap.Field {
        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bTextGroup", {
            controller: TextGroupController,
            templateUrl: ["$element", "$attrs", () => TextGroupTemplate.name],
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@"
            }
        } as any);
}
