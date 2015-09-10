module Allors {
    export class WorkspaceObject {
        private databaseObject: DatabaseObject;

        private roleByRoleTypeName: { [id: string]: any; };

        constructor(databaseObject: DatabaseObject) {
            this.databaseObject = databaseObject;
        }

        save() : Data.SaveObjectData {
            if (this.roleByRoleTypeName !== undefined) {
                var data = new Data.SaveObjectData();
                data.id = this.id;
                data.version = this.version;
                data.roles = [];

                _.forEach(this.roleByRoleTypeName, (role, roleTypeName) => {
                    var role = this.roleByRoleTypeName[roleTypeName];
                    var originalRole = this.databaseObject[roleTypeName];
                    data.roles.push(role);
                });

                return data;
            } 

            return undefined;
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
            if (this.roleByRoleTypeName === undefined) {
                this.roleByRoleTypeName = {};
            }

            this.roleByRoleTypeName[roleTypeName] = value;
        }
    }
}