module Allors {
    export interface IWorkspaceObject {
        id: string;
        version: string;
        objectType: ObjectType;

        workspace: IWorkspace;
        databaseObject: IDatabaseObject;

        isChanged: boolean;

        get(roleTypeName: string): any;
        set(roleTypeName: string, value: any) ;
        add(roleTypeName: string, value: any);
        remove(roleTypeName: string, value: any);

        save(): Data.SaveRequestObject;
        sync();
    }

    export class WorkspaceObject implements IWorkspaceObject {
        public workspace : IWorkspace;
        public databaseObject: IDatabaseObject;

        private changedRoleByRoleTypeName: { [id: string]: any; };
        private roleByRoleTypeName: { [id: string]: any; } = {};

        get isChanged(): boolean {
            return this.changedRoleByRoleTypeName !== undefined;
        }
 
        get id(): string {
            return this.databaseObject.id;
        }

        get version(): string {
            return this.databaseObject.version;
        }

        get objectType(): ObjectType {
            return this.databaseObject.objectType;
        }

        get(roleTypeName: string): any {
            var value = this.roleByRoleTypeName[roleTypeName];

            if (value === undefined) {
                var roleType = this.objectType.roleTypeByName[roleTypeName];
                if (roleType.isUnit) {
                    value = this.databaseObject.roles[roleTypeName] || null;
                } else {
                    if (roleType.isOne) {
                        var role: string = this.databaseObject.roles[roleTypeName];
                        value = role ? this.workspace.get(role) : null;
                    } else {
                        var roles: string[] = this.databaseObject.roles[roleTypeName];
                        value = roles ? roles.map(role => { return this.workspace.get(role); }) : []; 
                    }
                }

                this.roleByRoleTypeName[roleTypeName] = value;
            }

            return value;
        }

        set(roleTypeName: string, value: any) {
            if (this.changedRoleByRoleTypeName === undefined) {
                this.changedRoleByRoleTypeName = {};
            }

            if (value === undefined) {
                value = null;
            }

            if (value === null) {
                var roleType = this.objectType.roleTypeByName[roleTypeName];
                if (roleType.isComposite && roleType.isMany) {
                    value = [];
                }
            }

            this.roleByRoleTypeName[roleTypeName] = value;
            this.changedRoleByRoleTypeName[roleTypeName] = value;
        }

        add(roleTypeName: string, value: any) {
            var roles = this.get(roleTypeName);
            if (roles.indexOf(value) < 0) {
                roles.push(value);
            }

            this.set(roleTypeName, roles);
        }

        remove(roleTypeName: string, value: any) {
            var roles = this.get(roleTypeName);
            var index = roles.indexOf(value);
            if (index >= 0) {
                roles.splice(index, 1);
            }

            this.set(roleTypeName, roles);
        }

        save(): Data.SaveRequestObject {
            if (this.changedRoleByRoleTypeName !== undefined) {
                var data = new Data.SaveRequestObject();
                data.i = this.id;
                data.v = this.version;
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
                            save.s = role ? role.id : null;
                        } else {
                            var roleIds = role.map(item => { return (<WorkspaceObject>item).id; });

                            if (roleIds.length === 0) {
                                save.s = [];
                            } else {
                                var originalRoleIds = <string[]>this.databaseObject.roles[roleTypeName];
                                if (!originalRoleIds) {
                                    save.s = roleIds;
                                } else {
                                    save.a = _.difference(roleIds, originalRoleIds);
                                    save.r = _.difference(originalRoleIds, roleIds);
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

        sync() {
            this.databaseObject = this.databaseObject.database.get(this.id);
            if (this.changedRoleByRoleTypeName) {
                this.changedRoleByRoleTypeName = undefined;
            }

            this.roleByRoleTypeName = {};
        }
    }
}