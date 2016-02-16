namespace App {

    templates.$inject = ["$templateCache"];
    function templates($templateCache: ng.ITemplateCacheService): void {

        Allors.Form.FormManagerTemplates.register($templateCache);
        Allors.Form.TextTemplates.register($templateCache);

    }

    angular
        .module("app")
        .run(templates);
}