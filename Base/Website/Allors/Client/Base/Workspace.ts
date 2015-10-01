module Allors {
    export interface IWorkspace {
        get(id: string): any;
        save(): Data.SaveRequest;
        reset(): void;
    }

    export class Workspace {
        private database: IDatabase;
        private workspaceObjectById: { [id: string]: WorkspaceObject; } = {};

        constructor(database: IDatabase) {
            this.database = database;
        }

        get hasChanges(): boolean {
            var hasChanges = false;
            _.forEach(this.workspaceObjectById, workspaceObject => {
                if (workspaceObject.hasChanges) {
                    hasChanges = true;
                    return;
                }
            });

            return hasChanges;
        }

        get(id: string): any {
            var workspaceObject = this.workspaceObjectById[id];
            if (workspaceObject === undefined) {
                var databaseObject = this.database.get(id);

                var type = Domain[databaseObject.objectType.name];
                workspaceObject = new type();
                workspaceObject.workspace = this;
                workspaceObject.databaseObject = databaseObject;

                this.workspaceObjectById[workspaceObject.id] = workspaceObject;
            }

            return workspaceObject;
        }

        save() : Data.SaveRequest {
            var data = new Data.SaveRequest();
            data.objects = [];

            _.forEach(this.workspaceObjectById, workspaceObject => {
                var objectData = workspaceObject.save();
                if (objectData !== undefined) {
                    data.objects.push(objectData);
                }
            });

            return data;
        }
        
        reset(): void {
            _.forEach(this.workspaceObjectById, v => {
                v.reset();
            });
        }
    }
}