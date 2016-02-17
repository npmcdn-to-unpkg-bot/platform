namespace App {

    templates.$inject = ["$templateCache"];
    function templates($templateCache: ng.ITemplateCacheService): void {

        Allors.Bootstrap.FormTemplate.register($templateCache);

        Allors.Bootstrap.GroupTemplate.register($templateCache);
        Allors.Bootstrap.LabelTemplate.register($templateCache);

        Allors.Bootstrap.TextTemplate.register($templateCache);
        Allors.Bootstrap.SelectOneTemplate.register($templateCache);
        Allors.Bootstrap.SelectManyTemplate.register($templateCache);
    }

    angular
        .module("app")
        .run(templates);
}