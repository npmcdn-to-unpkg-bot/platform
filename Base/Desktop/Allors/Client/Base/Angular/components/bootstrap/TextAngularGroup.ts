namespace Allors.Bootstrap {
    export class TextAngularGroupTemplate {
        static name = "allors/bootstrap/text-angular-group";

        private static view = 
`
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
       <text-angular ta-disabled="!$ctrl.canWrite" ng-model="$ctrl.role"/>
    </b-input-group>
</b-group>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(TextAngularGroupTemplate.name, TextAngularGroupTemplate.view);
        }
    }

    class TextAngularGroupController extends Bootstrap.Field {
        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bTextAngularGroup", {
            controller: TextAngularGroupController,
            templateUrl: ["$element", "$attrs", () => TextAngularGroupTemplate.name],
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@"
            }
        } as any);
}
