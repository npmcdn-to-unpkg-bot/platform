namespace Allors.Form {
    export class TextTemplates {

        static bootstrap = 
`<div class="form-group">
<label>{{$ctrl.label }}</label> <input placeholder="{{$ctrl.placeholder}}" class="form-control" ng-model="$ctrl.role">
</div>`;

        static register(templateCache: ng.ITemplateCacheService) {
            templateCache.put("allors/form/text/bootstrap", TextTemplates.bootstrap);
        }
    }

    class TextController extends FieldController {
        static $inject = ["$log"];
        constructor($log: angular.ILogService) {
            super($log);
        }
    }

    angular
        .module("allors")
        .component("text", {
            controller: TextController,
            templateUrl: ["$element", "$attrs", "templateService", ($element: ng.IAugmentedJQuery, $attrs: ng.IAttributes, templateService: TemplateService) => templateService.getUrl($element, $attrs)],
            require: {
                formManager: "^formManager"
            },
            bindings: {
                object: "<o",
                roleTypeName: "@r"
            }
        } as any);
}
