namespace Allors.Bootstrap {
    export class StaticGroupTemplate {
        static name = "allors/bootstrap/static-group";

        private static createDefaultView() {
            return `
<b-group field="$ctrl">
    <b-label field="$ctrl"/>
    <b-input-group field="$ctrl">
` + StaticTemplate.createDefaultView() + `
    </b-input-group>
</b-group>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = StaticGroupTemplate.createDefaultView()) {
            templateCache.put(StaticGroupTemplate.name, view);
        }
    }

    class StaticGroupController extends Bootstrap.Field {
        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bStaticGroup", {
            controller: StaticGroupController,
            templateUrl: ["$element", "$attrs", () => StaticGroupTemplate.name],
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@"
            }
        } as any);
}
