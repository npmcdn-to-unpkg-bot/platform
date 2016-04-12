namespace Allors.Bootstrap {

    export class TextareaTemplate {
        static name = "allors/bootstrap/textarea";

        static createDefaultView() {
            return `
<textarea placeholder="{{$ctrl.placeholder}}" class="form-control"
        ng-model="$ctrl.role"
        ng-disabled="!$ctrl.canWrite"
        ng-required="$ctrl.roleType.isRequired">
`;
        }

        static register(templateCache: angular.ITemplateCacheService, view = TextareaTemplate.createDefaultView()) {
            templateCache.put(TextareaTemplate.name, view);
        }
    }

    class TextareaController extends Bootstrap.Field {
        static $inject = ["$log", "$translate"];
        constructor($log: angular.ILogService, $translate: angular.translate.ITranslateService) {
            super($log, $translate);
        }
    }

    angular
        .module("allors")
        .component("bTextarea", {
            controller: TextareaController,
            templateUrl: TextareaTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                object: "<",
                relation: "@"
            }
        } as any);
}
