namespace Allors.Bootstrap {
    export abstract class Field {
        form: Bootstrap.FormController;
        object: SessionObject;
        roleTypeName: string;
        displayName: string;
        
        constructor(public $log: angular.ILogService) {
        }

        get canRead(): boolean {
            let canRead = false;
            if (this.object) {
                canRead = this.object.canRead(this.roleTypeName);
            }

            return canRead;
        }

        get canWrite(): boolean {
            let canWrite = false;
            if (this.object) {
                canWrite = this.object.canWrite(this.roleTypeName);
            }

            return canWrite;
        }

        get roleType(): Meta.RoleType {
            return this.object && this.object.objectType.roleTypeByName[this.roleTypeName];
        }
        
        get role(): any {
            return this.object && this.object[this.roleTypeName];
        }

        set role(value: any) {
            this.object[this.roleTypeName] = value;
        }

        get display(): any {
            return this.role && this.role[this.displayName];
        }

        get label(): string {
            return this.roleTypeName;
        }

        get placeholder(): string {
            return this.roleTypeName;
        }
    }
}
