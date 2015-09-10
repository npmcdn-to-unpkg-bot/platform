module Allors {
    export class WorkspaceObject {
        private workspace : Workspace;
        private databaseObject: DatabaseObject;

        private roleByRoleTypeName: { [id: string]: any; };

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

        getUnit(roleTypeName: string): any {
            var value;

            if (this.roleByRoleTypeName !== undefined) {
                value = this.roleByRoleTypeName[roleTypeName];
            }

            if (value === undefined) {
                value = this.databaseObject[roleTypeName];
            }

            return value === undefined ? null : value;
        }

        setUnit(roleTypeName: string, value: any) {
            if (this.roleByRoleTypeName === undefined) {
                this.roleByRoleTypeName = {};
            }

            this.roleByRoleTypeName[roleTypeName] = value;
        }

        getOne(roleTypeName: string): any {
            var value;

            if (this.roleByRoleTypeName !== undefined) {
                value = this.roleByRoleTypeName[roleTypeName];
            }

            if (value === undefined) {
                value = this.databaseObject[roleTypeName];
            }

            if (value === null || value === undefined) {
                return null;
            }

            return this.workspace.get(value);
        }

        setOne(roleTypeName: string, value: any) {
            if (this.roleByRoleTypeName === undefined) {
                this.roleByRoleTypeName = {};
            }

            if (value === undefined) {
                throw "Setter does not allow undefined, use null instead";
            }

            this.roleByRoleTypeName[roleTypeName] = value===null ? null : value.id;
        }

        getMany(roleTypeName: string): any {
            var value;

            if (this.roleByRoleTypeName !== undefined) {
                value = this.roleByRoleTypeName[roleTypeName];
            }

            if (value === undefined) {
                value = this.databaseObject[roleTypeName];
            }

            if (value === undefined || value === null) {
                return [];
            }

            return _.map(value, item => {
                return this.workspace.get(<string>item);
            });
        }

        setMany(roleTypeName: string, value: any) {
            if (this.roleByRoleTypeName === undefined) {
                this.roleByRoleTypeName = {};
            }

            if (_.isUndefined(value) || _.isNull(value) || _.isEmpty(value)) {
                this.roleByRoleTypeName[roleTypeName] = null;
            }

            this.roleByRoleTypeName[roleTypeName] = _.map(value, item => {
                return (<WorkspaceObject>item).id;
            });
        }
    }
}