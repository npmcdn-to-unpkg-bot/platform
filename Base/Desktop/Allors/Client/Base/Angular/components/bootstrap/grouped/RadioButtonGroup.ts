namespace Allors.Bootstrap {
    export class RadioButtonGroupTemplate {
        static name = "allors/bootstrap/radio-button-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + RadioButtonTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = RadioButtonGroupTemplate.createDefaultView()) {
            templateCache.put(RadioButtonGroupTemplate.name, view);
        }
    }

    class RadioButtonGroupController extends Bootstrap.Field {
        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bRadioButtonGroup", {
            controller: RadioButtonGroupController,
            templateUrl: ["$element", "$attrs", () => RadioButtonGroupTemplate.name],
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@"
            }
        } as any);
}
