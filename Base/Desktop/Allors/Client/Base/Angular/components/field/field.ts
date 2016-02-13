module App {

    class FieldController {
        object: Allors.WorkspaceObject;
        roleTypeName: string;

        static $inject = ["$log"];
        constructor(private $log: angular.ILogService) {
        }

        get role(): any {
            return this.object[this.roleTypeName];
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
        .component("aField", {
            controller: FieldController,
            templateUrl: "allors/client/base/angular/components/field/field.html",
            bindings: {
                object: "<o",
                roleTypeName: "@r"
            }
        });
}
