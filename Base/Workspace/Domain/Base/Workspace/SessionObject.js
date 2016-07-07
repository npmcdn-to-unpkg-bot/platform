var Allors;
(function (Allors) {
    var SessionObject = (function () {
        function SessionObject() {
            this.roleByRoleTypeName = {};
        }
        Object.defineProperty(SessionObject.prototype, "hasChanges", {
            get: function () {
                if (this.newId) {
                    return true;
                }
                return this.changedRoleByRoleTypeName !== undefined;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(SessionObject.prototype, "id", {
            get: function () {
                return this.workspaceObject ? this.workspaceObject.id : undefined;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(SessionObject.prototype, "version", {
            get: function () {
                return this.workspaceObject ? this.workspaceObject.version : undefined;
            },
            enumerable: true,
            configurable: true
        });
        SessionObject.prototype.canRead = function (roleTypeName) {
            if (this.newId) {
                return true;
            }
            return this.workspaceObject.canRead(roleTypeName);
        };
        SessionObject.prototype.canWrite = function (roleTypeName) {
            if (this.newId) {
                return true;
            }
            return this.workspaceObject.canWrite(roleTypeName);
        };
        SessionObject.prototype.get = function (roleTypeName) {
            var _this = this;
            var value = this.roleByRoleTypeName[roleTypeName];
            if (value === undefined) {
                var roleType = this.objectType.roleTypeByName[roleTypeName];
                if (this.newId === undefined) {
                    if (roleType.isUnit) {
                        value = this.workspaceObject.roles[roleTypeName];
                        if (value === undefined) {
                            value = null;
                        }
                        ;
                    }
                    else {
                        try {
                            if (roleType.isOne) {
                                var role = this.workspaceObject.roles[roleTypeName];
                                value = role ? this.session.get(role) : null;
                            }
                            else {
                                var roles = this.workspaceObject.roles[roleTypeName];
                                value = roles ? roles.map(function (role) {
                                    return _this.session.get(role);
                                }) : [];
                            }
                        }
                        catch (e) {
                            var value_1 = "N/A";
                            try {
                                value_1 = this.toString();
                            }
                            catch (e2) { }
                            ;
                            throw new Error("Could not get role " + roleTypeName + " from [objectType: " + this.objectType.name + ", id: " + this.id + ", value: '" + value_1 + "']");
                        }
                    }
                }
                else {
                    if (roleType.isComposite && roleType.isMany) {
                        value = [];
                    }
                    else {
                        value = null;
                    }
                }
                this.roleByRoleTypeName[roleTypeName] = value;
            }
            return value;
        };
        SessionObject.prototype.set = function (roleTypeName, value) {
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
        };
        SessionObject.prototype.add = function (roleTypeName, value) {
            var roles = this.get(roleTypeName);
            if (roles.indexOf(value) < 0) {
                roles.push(value);
            }
            this.set(roleTypeName, roles);
        };
        SessionObject.prototype.remove = function (roleTypeName, value) {
            var roles = this.get(roleTypeName);
            var index = roles.indexOf(value);
            if (index >= 0) {
                roles.splice(index, 1);
            }
            this.set(roleTypeName, roles);
        };
        SessionObject.prototype.save = function () {
            if (this.changedRoleByRoleTypeName !== undefined) {
                var data = new Allors.Data.PushRequestObject();
                data.i = this.id;
                data.v = this.version;
                data.roles = this.saveRoles();
                return data;
            }
            return undefined;
        };
        SessionObject.prototype.saveNew = function () {
            var data = new Allors.Data.PushRequestNewObject();
            data.ni = this.newId;
            data.t = this.objectType.name;
            if (this.changedRoleByRoleTypeName !== undefined) {
                data.roles = this.saveRoles();
            }
            return data;
        };
        SessionObject.prototype.reset = function () {
            if (this.workspaceObject) {
                this.workspaceObject = this.workspaceObject.workspace.get(this.id);
            }
            if (this.changedRoleByRoleTypeName) {
                this.changedRoleByRoleTypeName = undefined;
            }
            this.roleByRoleTypeName = {};
        };
        SessionObject.prototype.saveRoles = function () {
            var _this = this;
            var saveRoles = new Array();
            _.forEach(this.changedRoleByRoleTypeName, function (role, roleTypeName) {
                var roleType = _this.objectType.roleTypeByName[roleTypeName];
                var saveRole = new Allors.Data.PushRequestRole;
                saveRole.t = roleType.name;
                if (roleType.isUnit) {
                    saveRole.s = role;
                }
                else {
                    if (roleType.isOne) {
                        saveRole.s = role ? role.id || role.newId : null;
                    }
                    else {
                        var roleIds = role.map(function (item) { return item.id || item.newId; });
                        if (_this.newId) {
                            saveRole.a = roleIds;
                        }
                        else {
                            var originalRoleIds = _this.workspaceObject.roles[roleTypeName];
                            if (!originalRoleIds) {
                                saveRole.a = roleIds;
                            }
                            else {
                                saveRole.a = _.difference(roleIds, originalRoleIds);
                                saveRole.r = _.difference(originalRoleIds, roleIds);
                            }
                        }
                    }
                }
                saveRoles.push(saveRole);
            });
            return saveRoles;
        };
        return SessionObject;
    }());
    Allors.SessionObject = SessionObject;
})(Allors || (Allors = {}));
//# sourceMappingURL=SessionObject.js.map