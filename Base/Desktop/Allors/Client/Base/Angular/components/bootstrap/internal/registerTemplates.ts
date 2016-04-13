namespace Allors.Bootstrap {

    export function registerTemplates($templateCache: ng.ITemplateCacheService): void {

        // Shared
        // ------
        Bootstrap.ImageModalTemplate.register($templateCache);

        // Form
        // ----
        Bootstrap.FormTemplate.register($templateCache);

        // Fields
        // ------
        Bootstrap.StaticTemplate.register($templateCache);
        Bootstrap.StaticEnumTemplate.register($templateCache);
        Bootstrap.TextTemplate.register($templateCache);
        Bootstrap.TextareaTemplate.register($templateCache);
        Bootstrap.TextAngularTemplate.register($templateCache);
        Bootstrap.EnumTemplate.register($templateCache);
        Bootstrap.SelectTemplate.register($templateCache);
        Bootstrap.ImageTemplate.register($templateCache);
        Bootstrap.RadioButtonTemplate.register($templateCache);
        Bootstrap.DatepickerPopupTemplate.register($templateCache);

        // Field Groups
        // ------------
        // Internals
        Bootstrap.GroupTemplate.register($templateCache);
        Bootstrap.LabelTemplate.register($templateCache);
        Bootstrap.InputGroupTemplate.register($templateCache);
        // Controls
        Bootstrap.StaticGroupTemplate.register($templateCache);
        Bootstrap.StaticEnumGroupTemplate.register($templateCache);
        Bootstrap.TextGroupTemplate.register($templateCache);
        Bootstrap.TextareaGroupTemplate.register($templateCache);
        Bootstrap.TextAngularGroupTemplate.register($templateCache);
        Bootstrap.EnumGroupTemplate.register($templateCache);
        Bootstrap.SelectGroupTemplate.register($templateCache);
        Bootstrap.ImageGroupTemplate.register($templateCache);
        Bootstrap.RadioButtonGroupTemplate.register($templateCache);
        Bootstrap.DatepickerPopupGroupTemplate.register($templateCache);
    }
}