namespace Allors.Bootstrap {
    export class TextGroupTemplate {
        static name = "allors/bootstrap/text-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + TextTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        };

        static register(templateCache: angular.ITemplateCacheService, view = TextGroupTemplate.createDefaultView()) {
            templateCache.put(TextGroupTemplate.name, view);
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
