var Allors;
(function (Allors) {
    var WorkspaceObject = (function () {
        function WorkspaceObject(databaseObject) {
            this.databaseObject = databaseObject;
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
        WorkspaceObject.prototype.get = function (roleTypeName) {
            var value;
            if (this.roleByRoleTypeName !== undefined) {
                value = this.roleByRoleTypeName[roleTypeName];
            }
            if (value === undefined) {
                value = this.databaseObject[roleTypeName];
            }
            return value;
        };
        WorkspaceObject.prototype.set = function (roleTypeName, value) {
            if (this.roleByRoleTypeName === undefined) {
                this.roleByRoleTypeName = {};
            }
            this.roleByRoleTypeName[roleTypeName] = value;
        };
        return WorkspaceObject;
    })();
    Allors.WorkspaceObject = WorkspaceObject;
})(Allors || (Allors = {}));
//# sourceMappingURL=WorkspaceObject.js.map