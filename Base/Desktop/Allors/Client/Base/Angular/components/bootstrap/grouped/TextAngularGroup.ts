namespace Allors.Bootstrap {
    export class TextAngularGroupTemplate {
        static name = "allors/bootstrap/text-angular-group";

        private static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + TextAngularTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = TextAngularGroupTemplate.createDefaultView()) {
            templateCache.put(TextAngularGroupTemplate.name, view);
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
