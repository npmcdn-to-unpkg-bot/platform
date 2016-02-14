module App {

    class FieldController {
        form: ng.IFormController;
        object: Allors.WorkspaceObject;
        roleTypeName: string;

        static $inject = ["$log"];
        constructor(private $log: angular.ILogService) {
        }

        get role(): any {
            return this.object && this.object[this.roleTypeName];
        }

        set role(value: any) {
            this.object[this.roleTypeName] = value;
        }

        get label(): string {
            return this.roleTypeName;
        }

        get placeholder(): string {
            return this.roleTypeName;
        }
    }

    angular
        .module("allors")
        .component("allorsField", {
            controller: FieldController,
            template: ($element, $attrs) => 
`<div class="form-group">
<label>{{$ctrl.label }}</label> <input placeholder="{{$ctrl.placeholder}}" class="form-control" ng-model="$ctrl.role">
</div>`,
            require: {
                 form: "^form"
            },
            bindings: {
                object: "<o",
                roleTypeName: "@r"
            }
        } as any);
}
