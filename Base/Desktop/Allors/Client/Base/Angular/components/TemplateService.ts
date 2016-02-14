namespace Allors {
    export class TemplateService {
        templateByName: { [name: string]: string; } = {};

        get($element: ng.IAugmentedJQuery, $attrs: ng.IAttributes) {
            const element: HTMLElement = $element[0];
            const name = element.nodeName.toLowerCase();
            return this.templateByName[name];
        }
    }

    angular.module("allors")
        .service("templateService",
        TemplateService);
}