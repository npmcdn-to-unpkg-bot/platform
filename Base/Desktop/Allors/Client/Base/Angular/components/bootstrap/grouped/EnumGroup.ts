namespace Allors.Bootstrap {
    export class EnumGroupTemplate {
        static name = "allors/bootstrap/enum-group";

        static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + EnumTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        };

        static register(templateCache: angular.ITemplateCacheService, view = EnumGroupTemplate.createDefaultView()) {
            templateCache.put(EnumGroupTemplate.name, view);
        }
    }

    class EnumGroupController extends Bootstrap.Field {
        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bEnumGroup", {
            controller: EnumGroupController,
            templateUrl: ["$element", "$attrs", () => EnumGroupTemplate.name],
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@"
            }
        } as any);
}
