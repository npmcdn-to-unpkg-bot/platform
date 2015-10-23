module Allors {
    export class WorkspaceObject {
        public workspace : IWorkspace;
        public databaseObject: DatabaseObject;

        private changedRoleByRoleTypeName: { [id: string]: any; };
        private cachedRoleByRoleTypeName: { [id: string]: any; } = {};

        get hasChanges(): boolean {
            return this.changedRoleByRoleTypeName !== undefined;
        }

        save() : Data.SaveRequestObject {
            if (this.changedRoleByRoleTypeName !== undefined) {
                var data = new Data.SaveRequestObject();
                data.i = this.id;
                data.v  = this.version;
                data.roles = [];

                var objectType = this.databaseObject.objectType;

                _.forEach(this.changedRoleByRoleTypeName, (role, roleTypeName) => {
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
                                if (role.length === 0) {
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
            var value = this.cachedRoleByRoleTypeName[roleTypeName];

            if (value === undefined) {
                value = this.databaseObject.roles[roleTypeName] || null;
                this.cachedRoleByRoleTypeName[roleTypeName] = value;
            }

            return value;
        }

        setUnit(roleTypeName: string, value: any) {
            if (this.changedRoleByRoleTypeName === undefined) {
                this.changedRoleByRoleTypeName = {};
            }

            this.cachedRoleByRoleTypeName[roleTypeName] = value;
            this.changedRoleByRoleTypeName[roleTypeName] = value;
        }

        getOne(roleTypeName: string): any {
            var value = this.cachedRoleByRoleTypeName[roleTypeName];

            if (value === undefined) {
                value = this.databaseObject.roles[roleTypeName] || null;

                if (value) {
                    value = this.workspace.get(value);
                }

                this.cachedRoleByRoleTypeName[roleTypeName] = value;
            }

            return value;
        }

        setOne(roleTypeName: string, value: any) {
            if (this.changedRoleByRoleTypeName === undefined) {
                this.changedRoleByRoleTypeName = {};
            }

            if (value === undefined) {
                value = null;
            }

            this.cachedRoleByRoleTypeName[roleTypeName] = value;
            this.changedRoleByRoleTypeName[roleTypeName] = value ? value.id : null;
        }

        getMany(roleTypeName: string): any {
            var value = this.cachedRoleByRoleTypeName[roleTypeName];

            if (value === undefined) {
                value = this.databaseObject.roles[roleTypeName] || [];

                if (value.length > 0) {
                    value = _.map(value, item => {
                        return this.workspace.get(<string>item);
                    });
                }
            }

            return value;
        }

        setMany(roleTypeName: string, value: any) {
            if (this.changedRoleByRoleTypeName === undefined) {
                this.changedRoleByRoleTypeName = {};
            }

            if (value === undefined || value === null ) {
                value = [];
            }

            this.cachedRoleByRoleTypeName[roleTypeName] = value;
            this.changedRoleByRoleTypeName[roleTypeName] = _.map(value, item => {
                return (<WorkspaceObject>item).id;
            });
        }

        reset() {
            this.databaseObject = this.databaseObject.database.get(this.id);
            if (this.changedRoleByRoleTypeName)
            {
                this.changedRoleByRoleTypeName = undefined;
            }

            this.cachedRoleByRoleTypeName = {};
        }
    }
}