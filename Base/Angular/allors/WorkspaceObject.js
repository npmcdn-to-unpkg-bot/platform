var Allors;
(function (Allors) {
    var WorkspaceObject = (function () {
        function WorkspaceObject() {
        }
        WorkspaceObject.prototype.save = function () {
            var _this = this;
            if (this.roleByRoleTypeName !== undefined) {
                var data = new Allors.Data.SaveObjectData();
                data.id = this.id;
                data.version = this.version;
                data.roles = [];
                _.forEach(this.roleByRoleTypeName, function (role, roleTypeName) {
                    var role = _this.roleByRoleTypeName[roleTypeName];
                    var originalRole = _this.databaseObject[roleTypeName];
                    data.roles.push(role);
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
                value = this.databaseObject[roleTypeName];
            }
            return value;
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
                value = this.databaseObject[roleTypeName];
            }
            if (value === null) {
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
                value = this.databaseObject[roleTypeName];
            }
            if (_.isUndefined(value) || _.isNull(value)) {
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
        return WorkspaceObject;
    })();
    Allors.WorkspaceObject = WorkspaceObject;
})(Allors || (Allors = {}));
//# sourceMappingURL=WorkspaceObject.js.map