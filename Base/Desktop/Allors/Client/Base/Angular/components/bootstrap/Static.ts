﻿namespace Allors.Bootstrap {

    export class StaticTemplate {
        static name = "allors/bootstrap/static";

        static createDefaultView() {
            return `
<p class="form-control-static" ng-bind="$ctrl.role"></p>
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = StaticTemplate.createDefaultView()) {
            templateCache.put(StaticTemplate.name, view);
        }
    }

    class StaticController extends Bootstrap.Field {
        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bStatic", {
            controller: StaticController,
            templateUrl: StaticTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@"
            }
        } as any);
}
