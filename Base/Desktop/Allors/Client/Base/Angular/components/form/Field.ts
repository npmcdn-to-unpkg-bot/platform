namespace Allors.Form {
    export abstract class FieldController {
        formManager: Form.FormManagerController;
        object: WorkspaceObject;
        roleTypeName: string;
        
        constructor(public $log: angular.ILogService) {
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
}
