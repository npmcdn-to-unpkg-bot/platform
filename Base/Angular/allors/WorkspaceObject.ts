module Allors {
    export class WorkspaceObject {
        private databaseObject: DatabaseObject;

        private roleByRoleTypeName: { [id: string]: any; };

        constructor(databaseObject: DatabaseObject) {
            this.databaseObject = databaseObject;
        }

        diff(diff: Diff) {
            if (this.roleByRoleTypeName !== undefined) {
                var objectDiff = new ObjectDiff(this.id, this.version);

                for (var roleTypeName in this.roleByRoleTypeName) {
                    var role = this.roleByRoleTypeName[roleTypeName];
                    var originalRole = this.databaseObject[roleTypeName];

                    var objectType = this.databaseObject.database.objectTypeByName[this.databaseObject.objectType.name];
                    var roleType = objectType.roleTypeByName[roleTypeName];

                    var roleDiff = roleType.diff(role, originalRole);
                    objectDiff.roleDiffByRoleTypeName[roleTypeName] = roleDiff;
                }
            }
        }
 
        get id(): string {
            return this.databaseObject.id;
        }

        get version(): string {
            return this.databaseObject.version;
        }

        get(roleTypeName: string): any {
            var value;

            if (this.roleByRoleTypeName !== undefined) {
                value = this.roleByRoleTypeName[roleTypeName];
            }

            if (value === undefined) {
                value = this.databaseObject[roleTypeName];
            }

            return value;
        }

        set(roleTypeName: string, value: any) {
            if (this.roleByRoleTypeName !== undefined) {
                this.roleByRoleTypeName = {};
            }

            this.roleByRoleTypeName[roleTypeName] = value;
        }
    }
}