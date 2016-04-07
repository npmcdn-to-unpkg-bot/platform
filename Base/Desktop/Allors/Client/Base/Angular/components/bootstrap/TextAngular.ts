namespace Allors.Bootstrap {

    export class TextAngularTemplate {
        static name = "allors/bootstrap/text-angular";

        static view = 
`
<text-angular ta-disabled="!$ctrl.canWrite" ng-model="$ctrl.role"/>
`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(TextAngularTemplate.name, TextAngularTemplate.view);
        }
    }

    class TextAngularController extends Bootstrap.Field {
        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bTextAngular", {
            controller: TextAngularController,
            templateUrl: TextAngularTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@"
            }
        } as any);
}
