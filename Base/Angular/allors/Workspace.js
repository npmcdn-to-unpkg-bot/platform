var Allors;
(function (Allors) {
    var Workspace = (function () {
        function Workspace(database) {
            this.workspaceObjectById = {};
            this.database = database;
        }
        Workspace.prototype.get = function (id) {
            var workspaceObject = this.workspaceObjectById[id];
            if (workspaceObject === undefined) {
                var databaseObject = this.database.get(id);
                var objectType = databaseObject.objectType;
                var type = Allors.Domain[objectType.name];
                workspaceObject = new type();
                workspaceObject.workspace = this;
                workspaceObject.databaseObject = databaseObject;
                this.workspaceObjectById[workspaceObject.id] = workspaceObject;
            }
            return workspaceObject;
        };
        return Workspace;
    })();
    Allors.Workspace = Workspace;
})(Allors || (Allors = {}));
//# sourceMappingURL=Workspace.js.map