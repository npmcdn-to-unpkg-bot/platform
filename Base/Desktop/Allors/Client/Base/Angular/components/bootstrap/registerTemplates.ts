namespace Allors.Bootstrap {

    export function registerTemplates($templateCache: angular.ITemplateCacheService): void {

        // Form
        // ----
        FormTemplate.register($templateCache);

        // Object
        // ------

        // Shared
        // ------
        Role.ImageModalTemplate.register($templateCache);

        // Fields
        // ------
        Role.StaticTemplate.register($templateCache);
        Role.StaticEnumTemplate.register($templateCache);
        Role.TextTemplate.register($templateCache);
        Role.TextareaTemplate.register($templateCache);
        Role.TextAngularTemplate.register($templateCache);
        Role.EnumTemplate.register($templateCache);
        Role.SelectTemplate.register($templateCache);
        Role.ImageTemplate.register($templateCache);
        Role.RadioButtonTemplate.register($templateCache);
        Role.DatepickerPopupTemplate.register($templateCache);

        // Field Groups
        // ------------
        // Internals
        Role.LabeledTemplate.register($templateCache);
        Role.LabelTemplate.register($templateCache);
        Role.LabeledInputTemplate.register($templateCache);
        // Controls
        Role.LabeledStaticTemplate.register($templateCache);
        Role.StaticEnumGroupTemplate.register($templateCache);
        Role.LabeledTextTemplate.register($templateCache);
        Role.LabeledTextareaTemplate.register($templateCache);
        Role.LabeledTextAngularTemplate.register($templateCache);
        Role.LabeledEnumTemplate.register($templateCache);
        Role.LabeledSelectTemplate.register($templateCache);
        Role.LabeledImageTemplate.register($templateCache);
        Role.LabeledRadioButtonTemplate.register($templateCache);
        Role.LabeledDatepickerPopupTemplate.register($templateCache);
    }
}