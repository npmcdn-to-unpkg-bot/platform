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
                var type = Allors.Domain[databaseObject.objectType.name];
                workspaceObject = new type();
                workspaceObject.workspace = this;
                workspaceObject.databaseObject = databaseObject;
                this.workspaceObjectById[workspaceObject.id] = workspaceObject;
            }
            return workspaceObject;
        };
        Workspace.prototype.save = function () {
            var data = new Allors.Data.SaveRequest();
            data.objects = [];
            _.forEach(this.workspaceObjectById, function (workspaceObject) {
                var objectData = workspaceObject.save();
                if (objectData !== undefined) {
                    data.objects.push(objectData);
                }
            });
            return data;
        };
        Workspace.prototype.reset = function () {
            _.forEach(this.workspaceObjectById, function (v) {
                v.reset();
            });
        };
        return Workspace;
    })();
    Allors.Workspace = Workspace;
})(Allors || (Allors = {}));
