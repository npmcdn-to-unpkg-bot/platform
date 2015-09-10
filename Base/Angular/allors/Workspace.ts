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

                var type = Domain[databaseObject.type];
                workspaceObject = new type();
                workspaceObject.workspace = this;
                workspaceObject.databaseObject = databaseObject;

                this.workspaceObjectById[workspaceObject.id] = workspaceObject;
            }

            return workspaceObject;
        }

        save() : Data.SaveData {
            var data = new Data.SaveData();
            data.objects = [];

            _.forEach(this.workspaceObjectById, workspaceObject => {
                var objectData = workspaceObject.save();
                if (objectData !== undefined) {
                    data.objects.push(objectData);
                }
            });

            return data;
        }
    }
}