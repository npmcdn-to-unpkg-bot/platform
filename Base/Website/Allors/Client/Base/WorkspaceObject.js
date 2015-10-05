var Allors;
(function (Allors) {
    var WorkspaceObject = (function () {
        function WorkspaceObject() {
        }
        Object.defineProperty(WorkspaceObject.prototype, "hasChanges", {
            get: function () {
                return this.roleByRoleTypeName !== undefined;
            },
            enumerable: true,
            configurable: true
        });
        WorkspaceObject.prototype.save = function () {
            var _this = this;
            if (this.roleByRoleTypeName !== undefined) {
                var data = new Allors.Data.SaveRequestObject();
                data.i = this.id;
                data.v = this.version;
                data.roles = [];
                var objectType = this.databaseObject.objectType;
                _.forEach(this.roleByRoleTypeName, function (role, roleTypeName) {
                    var roleType = objectType.roleTypeByName[roleTypeName];
                    var save = new Allors.Data.SaveRequestRole;
                    save.t = roleType.name;
                    if (roleType.isUnit) {
                        save.s = role;
                    }
                    else {
                        if (roleType.isOne) {
                            save.s = role;
                        }
                        else {
                            var originalRoleIds = _this.databaseObject.roles[roleTypeName];
                            if (originalRoleIds === undefined || originalRoleIds === null || originalRoleIds.length === 0) {
                                save.s = role;
                            }
                            else {
                                if (role === undefined || role === null || role.length === 0) {
                                    save.s = [];
                                }
                                else {
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
        };
        Object.defineProperty(WorkspaceObject.prototype, "id", {
            get: function () {
                return this.databaseObject.id;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(WorkspaceObject.prototype, "version", {
            get: function () {
                return this.databaseObject.version;
            },
            enumerable: true,
            configurable: true
        });
        WorkspaceObject.prototype.getUnit = function (roleTypeName) {
            var value;
            if (this.roleByRoleTypeName !== undefined) {
                value = this.roleByRoleTypeName[roleTypeName];
            }
            if (value === undefined) {
                value = this.databaseObject.roles[roleTypeName];
            }
            return value === undefined ? null : value;
        };
        WorkspaceObject.prototype.setUnit = function (roleTypeName, value) {
            if (this.roleByRoleTypeName === undefined) {
                this.roleByRoleTypeName = {};
            }
            this.roleByRoleTypeName[roleTypeName] = value;
        };
        WorkspaceObject.prototype.getOne = function (roleTypeName) {
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
        };
        WorkspaceObject.prototype.setOne = function (roleTypeName, value) {
            if (this.roleByRoleTypeName === undefined) {
                this.roleByRoleTypeName = {};
            }
            if (value === undefined) {
                throw "Setter does not allow undefined, use null instead";
            }
            this.roleByRoleTypeName[roleTypeName] = value === null ? null : value.id;
        };
        WorkspaceObject.prototype.getMany = function (roleTypeName) {
            var _this = this;
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
            return _.map(value, function (item) {
                return _this.workspace.get(item);
            });
        };
        WorkspaceObject.prototype.setMany = function (roleTypeName, value) {
            if (this.roleByRoleTypeName === undefined) {
                this.roleByRoleTypeName = {};
            }
            if (_.isUndefined(value) || _.isNull(value) || _.isEmpty(value)) {
                this.roleByRoleTypeName[roleTypeName] = null;
            }
            this.roleByRoleTypeName[roleTypeName] = _.map(value, function (item) {
                return item.id;
            });
        };
        WorkspaceObject.prototype.reset = function () {
            this.databaseObject = this.databaseObject.database.get(this.id);
            if (this.roleByRoleTypeName) {
                this.roleByRoleTypeName = undefined;
            }
        };
        return WorkspaceObject;
    })();
    Allors.WorkspaceObject = WorkspaceObject;
})(Allors || (Allors = {}));
//# sourceMappingURL=WorkspaceObject.js.map