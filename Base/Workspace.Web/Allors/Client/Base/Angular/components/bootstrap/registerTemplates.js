var Allors;
(function (Allors) {
    var Bootstrap;
    (function (Bootstrap) {
        function registerTemplates($templateCache) {
            // Form
            // ----
            Bootstrap.FormTemplate.register($templateCache);
            // Object
            // ------
            // Shared
            // ------
            Bootstrap.ImageModalTemplate.register($templateCache);
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
            Bootstrap.LabeledTemplate.register($templateCache);
            Bootstrap.LabelTemplate.register($templateCache);
            Bootstrap.LabeledInputTemplate.register($templateCache);
            // Controls
            Bootstrap.LabeledStaticTemplate.register($templateCache);
            Bootstrap.StaticEnumGroupTemplate.register($templateCache);
            Bootstrap.LabeledTextTemplate.register($templateCache);
            Bootstrap.LabeledTextareaTemplate.register($templateCache);
            Bootstrap.LabeledTextAngularTemplate.register($templateCache);
            Bootstrap.LabeledEnumTemplate.register($templateCache);
            Bootstrap.LabeledSelectTemplate.register($templateCache);
            Bootstrap.LabeledImageTemplate.register($templateCache);
            Bootstrap.LabeledRadioButtonTemplate.register($templateCache);
            Bootstrap.LabeledDatepickerPopupTemplate.register($templateCache);
            // Model
            // -----
            Bootstrap.SelectOneTemplate.register($templateCache);
            Bootstrap.SelectManyTemplate.register($templateCache);
        }
        Bootstrap.registerTemplates = registerTemplates;
    })(Bootstrap = Allors.Bootstrap || (Allors.Bootstrap = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=registerTemplates.js.map