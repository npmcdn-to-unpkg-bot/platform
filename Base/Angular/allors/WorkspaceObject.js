var Allors;
(function (Allors) {
    var WorkspaceObject = (function () {
        function WorkspaceObject(databaseObject) {
            this.databaseObject = databaseObject;
        }
        WorkspaceObject.prototype.diff = function (diff) {
            if (this.roleByRoleTypeName !== undefined) {
                var objectDiff = new Allors.ObjectDiff(this.id, this.version);
                for (var roleTypeName in this.roleByRoleTypeName) {
                    var role = this.roleByRoleTypeName[roleTypeName];
                    var objectType = this.databaseObject.database.objectTypeByName[this.databaseObject.objectType.name];
                    var roleType = objectType.roleTypeByName[roleTypeName];
                }
            }
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
            if (this.roleByRoleTypeName !== undefined) {
                this.roleByRoleTypeName = {};
            }
            this.roleByRoleTypeName[roleTypeName] = value;
        };
        return WorkspaceObject;
    })();
    Allors.WorkspaceObject = WorkspaceObject;
})(Allors || (Allors = {}));
//# sourceMappingURL=WorkspaceObject.js.map