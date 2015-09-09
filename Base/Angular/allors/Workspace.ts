module Allors {
    export class Workspace {
        private database: Database;
        private workspaceObjectById: { [id: string]: any; } = {};

        constructor(database: Database) {
            this.database = database;
        }

        get(id: string): any {
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
        }

        diff() : Diff {
            var diff = new Diff();

            for (var id in this.workspaceObjectById) {
                var object = <WorkspaceObject>this.workspaceObjectById[id];
                object.diff(diff);
            }

            return diff;
        }
    }
}