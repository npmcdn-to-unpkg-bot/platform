module Allors {
    export class WorkspaceObject {
        public workspace : IWorkspace;
        public databaseObject: DatabaseObject;

        private roleByRoleTypeName: { [id: string]: any; };

        get hasChanges(): boolean {
            return this.roleByRoleTypeName !== undefined;
        }

        save() : Data.SaveRequestObject {
            if (this.roleByRoleTypeName !== undefined) {
                var data = new Data.SaveRequestObject();
                data.i = this.id;
                data.v  = this.version;
                data.roles = [];

                var objectType = this.databaseObject.objectType;

                _.forEach(this.roleByRoleTypeName, (role, roleTypeName) => {
                    var roleType = objectType.roleTypeByName[roleTypeName];

                    var save = new Data.SaveRequestRole;
                    save.t = roleType.name;

                    if (roleType.isUnit) {
                        save.s = role;
                    } else {
                        if (roleType.isOne) {
                            save.s = role;
                        } else {
                            var originalRoleIds = <string[]>this.databaseObject.roles[roleTypeName];

                            if (originalRoleIds === undefined || originalRoleIds === null || originalRoleIds.length === 0) {
                                save.s = role;
                            } else {
                                if (role === undefined || role === null || role.length === 0) {
                                    save.s = [];
                                } else {
                                    save.a = _.difference(role, originalRoleIds);
                                    save.r = _.difference(originalRoleIds, role);
                                }
                            }
                        }
                    }

                    data.roles.push(save);
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
                value = this.databaseObject.roles[roleTypeName];
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
                value = this.databaseObject.roles[roleTypeName];
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
                value = this.databaseObject.roles[roleTypeName];
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

        reset() {
            this.databaseObject = this.databaseObject.database.get(this.id);
            if (this.roleByRoleTypeName)
            {
                this.roleByRoleTypeName = undefined;
            }
        }
    }
}