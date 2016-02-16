namespace Allors.Form {
    export class FormManagerTemplates {

        static bootstrap = 
`<ng-transclude />`;

        static register(templateCache: ng.ITemplateCacheService) {
            templateCache.put("allors/form/form-manager/bootstrap", FormManagerTemplates.bootstrap);
        }
    }

    export class FormManagerController  {

        form: ng.IFormController;
        settings;

        static $inject = ["$log"];
        constructor(private $log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("formManager", {
            controller: FormManagerController,
            transclude: true,
            templateUrl: ["$element", "$attrs", "templateService", ($element: ng.IAugmentedJQuery, $attrs: ng.IAttributes, templateService: TemplateService) => templateService.getUrl($element, $attrs)],
            require: {
                 form: "^form"
            },
            bindings: {
                settings: "<settings"
            }
        } as any);
}
