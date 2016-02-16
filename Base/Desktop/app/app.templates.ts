namespace App {

    templates.$inject = ["$templateCache"];
    function templates($templateCache: ng.ITemplateCacheService): void {

        Allors.Bootstrap.FormTemplates.register($templateCache);
        Allors.Bootstrap.TextTemplates.register($templateCache);

    }

    angular
        .module("app")
        .run(templates);
}