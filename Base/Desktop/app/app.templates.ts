namespace App {

    templates.$inject = ["$templateCache"];
    function templates($templateCache: ng.ITemplateCacheService): void {

        // Shared
        // ------
        Allors.Bootstrap.ImageModalTemplate.register($templateCache);

        // Form
        // ----
        Allors.Bootstrap.FormTemplate.register($templateCache);

        // Fields
        // ------
        Allors.Bootstrap.EnumTemplate.register($templateCache);
        Allors.Bootstrap.SelectTemplate.register($templateCache);
        Allors.Bootstrap.TextAngularTemplate.register($templateCache);
        Allors.Bootstrap.RadioButtonTemplate.register($templateCache);
        Allors.Bootstrap.StaticTemplate.register($templateCache);

        // Field Groups
        // ------------
        // Internals
        Allors.Bootstrap.GroupTemplate.register($templateCache);
        Allors.Bootstrap.LabelTemplate.register($templateCache);
        Allors.Bootstrap.InputGroupTemplate.register($templateCache);
        // Controls
        Allors.Bootstrap.TextGroupTemplate.register($templateCache);
        Allors.Bootstrap.TextAngularGroupTemplate.register($templateCache);
        Allors.Bootstrap.SelectGroupTemplate.register($templateCache);
        Allors.Bootstrap.ImageGroupTemplate.register($templateCache);
        Allors.Bootstrap.StaticGroupTemplate.register($templateCache);
    }

    angular
        .module("app")
        .run(templates);
}