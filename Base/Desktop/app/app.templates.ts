namespace App {

    templates.$inject = ["$templateCache"];
    function templates($templateCache: ng.ITemplateCacheService): void {

        Allors.Bootstrap.FormTemplate.register($templateCache);

        Allors.Bootstrap.GroupTemplate.register($templateCache);
        Allors.Bootstrap.LabelTemplate.register($templateCache);
        Allors.Bootstrap.InputGroupTemplate.register($templateCache);

        Allors.Bootstrap.TextTemplate.register($templateCache);
        Allors.Bootstrap.SelectTemplate.register($templateCache);
        Allors.Bootstrap.ImageTemplate.register($templateCache);
        Allors.Bootstrap.ImageModalTemplate.register($templateCache);
    }

    angular
        .module("app")
        .run(templates);
}