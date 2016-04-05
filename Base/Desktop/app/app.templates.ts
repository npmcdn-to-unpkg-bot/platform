namespace App {

    templates.$inject = ["$templateCache"];
    function templates($templateCache: ng.ITemplateCacheService): void {

        // Shared
        Allors.Bootstrap.ImageModalTemplate.register($templateCache);

        // Form
        Allors.Bootstrap.FormTemplate.register($templateCache);

        // Field 
        Allors.Bootstrap.EnumTemplate.register($templateCache);
        Allors.Bootstrap.SelectTemplate.register($templateCache);
        Allors.Bootstrap.TinyMceTemplate.register($templateCache);
        Allors.Bootstrap.RadioButtonTemplate.register($templateCache);

        // Field Group Internals
        Allors.Bootstrap.GroupTemplate.register($templateCache);
        Allors.Bootstrap.LabelTemplate.register($templateCache);
        Allors.Bootstrap.InputGroupTemplate.register($templateCache);

        // Field Groups
        Allors.Bootstrap.TextGroupTemplate.register($templateCache);
        Allors.Bootstrap.SelectGroupTemplate.register($templateCache);
        Allors.Bootstrap.ImageGroupTemplate.register($templateCache);
    }

    angular
        .module("app")
        .run(templates);
}