namespace App {

    templates.$inject = ["templateService"];
    function templates(templateService: Allors.TemplateService): void {

        const templateByName = templateService.templateByName;

        templateByName["a-field"] =
            `<div class="form-group">
<label>{{$ctrl.label }}</label> <input placeholder="{{$ctrl.placeholder}}" class="form-control" ng-model="$ctrl.role">
</div>`;

    }

    angular
        .module("app")
        .run(templates);
}