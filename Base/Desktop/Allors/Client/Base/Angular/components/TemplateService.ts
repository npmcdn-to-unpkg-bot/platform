namespace Allors {
    export class TemplateService {
        static framework = "bootstrap";

        static $inject = ["$log"];
        constructor(private $log: angular.ILogService) {
        }

        getUrl($element: ng.IAugmentedJQuery, $attrs: ng.IAttributes) {
            const element = $element[0];
            const name = element.nodeName.toLowerCase();
            const url = `allors/form/${name}/${TemplateService.framework}`;
            return url;
        }
    }

    angular.module("allors")
        .service("templateService",
        TemplateService);
}