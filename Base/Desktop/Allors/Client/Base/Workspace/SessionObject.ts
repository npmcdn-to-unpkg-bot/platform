namespace Allors {
    export interface ISessionObject {
        id: string;
        version: string;
        objectType: ObjectType;

        session: ISession;
        workspaceObject: IWorkspaceObject;

        hasChanges: boolean;

        get(roleTypeName: string): any;
        set(roleTypeName: string, value: any) ;
        add(roleTypeName: string, value: any);
        remove(roleTypeName: string, value: any);

        save(): Data.SaveRequestObject;
        saveNew(): Data.SaveRequestNewObject;
        reset();
    }

    export interface INewSessionObject extends ISessionObject {
        newId: string;
    }

    export class SessionObject implements INewSessionObject {
        public session : ISession;
        public workspaceObject: IWorkspaceObject;
        public objectType: ObjectType;

        public newId: string;

        private changedRoleByRoleTypeName: { [id: string]: any; };
        private roleByRoleTypeName: { [id: string]: any; } = {};

        get hasChanges(): boolean {
            return this.changedRoleByRoleTypeName !== undefined;
        }
 
        get id(): string {
            return this.workspaceObject ? this.workspaceObject.id : undefined;
        }

        get version(): string {
            return this.workspaceObject ? this.workspaceObject.version : undefined;
        }

        get(roleTypeName: string): any {
            var value = this.roleByRoleTypeName[roleTypeName];

            if (value === undefined) {
                var roleType = this.objectType.roleTypeByName[roleTypeName];
                if (this.newId === undefined) {
                    if (roleType.isUnit) {
                        value = this.workspaceObject.roles[roleTypeName];
                        if (value === undefined) {
                            value = null;
                        };
                    } else {
                        if (roleType.isOne) {
                            var role: string = this.workspaceObject.roles[roleTypeName];
                            value = role ? this.session.get(role) : null;
                        } else {
                            var roles: string[] = this.workspaceObject.roles[roleTypeName];
                            value = roles ? roles.map(role => {
                                try {
                                    return this.session.get(role);
                                }
                                catch (e) {
                                    var value = "N/A";
                                    try {
                                        value = this.toString();
                                    }
                                    catch (e2) { };

                                    throw "Could not get role " + roleTypeName + " from [id: " + this.id + ", value: " + value + "]";
                                }
                            }) : [];
                        }
                    }
                } else {
                    if (roleType.isComposite && roleType.isMany) {
                        value = [];
                    } else {
                        value = null;
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
                data.roles = this.saveRoles();
                return data;
            }

            return undefined;
        }

        saveNew(): Data.SaveRequestNewObject {
            if (this.changedRoleByRoleTypeName !== undefined) {
                var data = new Data.SaveRequestNewObject();
                data.ni = this.newId;
                data.t = this.objectType.name;
                data.roles = this.saveRoles();
                return data;
            }

            return undefined;
        }
        
        reset() {
            if (this.workspaceObject) {
                this.workspaceObject = this.workspaceObject.workspace.get(this.id);
            }

            if (this.changedRoleByRoleTypeName) {
                this.changedRoleByRoleTypeName = undefined;
            }

            this.roleByRoleTypeName = {};
        }

        private saveRoles(): Data.SaveRequestRole[] {
            var saveRoles = new Array<Data.SaveRequestRole>();

            _.forEach(this.changedRoleByRoleTypeName, (role, roleTypeName) => {
                var roleType = this.objectType.roleTypeByName[roleTypeName];

                var saveRole = new Data.SaveRequestRole;
                saveRole.t = roleType.name;

                if (roleType.isUnit) {
                    saveRole.s = role;
                } else {
                    if (roleType.isOne) {
                        saveRole.s = role ? role.id || role.newId : null;
                    } else {
                        var roleIds = role.map(item => { return (<SessionObject>item).id || (<SessionObject>item).newId; });

                        if (this.newId) {
                            saveRole.a = roleIds;
                        } else {
                            var originalRoleIds = <string[]>this.workspaceObject.roles[roleTypeName];
                            if (!originalRoleIds) {
                                saveRole.a = roleIds;
                            } else {
                                saveRole.a = _.difference(roleIds, originalRoleIds);
                                saveRole.r = _.difference(originalRoleIds, roleIds);
                            }
                        }
                    }
                }

                saveRoles.push(saveRole);
            });

            return saveRoles;
        }
    }
}