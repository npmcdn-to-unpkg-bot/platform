namespace Allors.Bootstrap {
    export class TextareaGroupTemplate {
        static name = "allors/bootstrap/textarea-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + TextareaTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = TextareaGroupTemplate.createDefaultView()) {
            templateCache.put(TextareaGroupTemplate.name, view);
        }
    }

    class TextareaGroupController extends Bootstrap.Field {
        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bTextareaGroup", {
            controller: TextareaGroupController,
            templateUrl: ["$element", "$attrs", () => TextareaGroupTemplate.name],
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@"
            }
        } as any);
}
