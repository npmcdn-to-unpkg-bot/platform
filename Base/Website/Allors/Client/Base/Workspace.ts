module Allors {
    export interface IWorkspace {
        hasChanges: boolean;

        get(id: string): any;
        create(objectTypeName: string): any;
        save(): Data.SaveRequest;
        onSaved(saveResponse: Data.SaveResponse): void;
        sync(): void;
    }

    export class Workspace implements IWorkspace {
        private static idCounter = 0;

        private database: IDatabase;
        private workspaceObjectById: { [id: string]: IWorkspaceObject; } = {};
        private newWorkspaceObjectById: { [id: string]: INewWorkspaceObject; } = {};

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
            if (id === undefined) {
                return undefined;
            }

            var workspaceObject = this.workspaceObjectById[id];
            if (workspaceObject === undefined) {
                var databaseObject = this.database.get(id);

                var type = Domain[databaseObject.objectType.name];
                workspaceObject = new type();
                workspaceObject.workspace = this;
                workspaceObject.databaseObject = databaseObject;
                workspaceObject.objectType = databaseObject.objectType;

                this.workspaceObjectById[workspaceObject.id] = workspaceObject;
            }

            return workspaceObject;
        }

        create(objectTypeName: string): any {
            var type = Domain[objectTypeName];

            var workspaceObject: INewWorkspaceObject = new type();
            workspaceObject.workspace = this;
            workspaceObject.objectType = this.database.objectTypeByName[objectTypeName];
            workspaceObject.newId = (--Workspace.idCounter).toString();

            this.newWorkspaceObjectById[workspaceObject.newId] = workspaceObject;

            return workspaceObject;
        }

        save() : Data.SaveRequest {
            var data = new Data.SaveRequest();
            data.newObjects = [];
            data.objects = [];

            if (this.newWorkspaceObjectById) {
                _.forEach(this.newWorkspaceObjectById, newWorkspaceObject => {
                    var objectData = newWorkspaceObject.saveNew();
                    if (objectData !== undefined) {
                        data.newObjects.push(objectData);
                    }
                });
            }

            _.forEach(this.workspaceObjectById, workspaceObject => {
                var objectData = workspaceObject.save();
                if (objectData !== undefined) {
                    data.objects.push(objectData);
                }
            });

            return data;
        }
        
        onSaved(saveResponse: Data.SaveResponse): void {
            if (saveResponse.newObjects) {
                _.forEach(saveResponse.newObjects, saveResponseNewObject => {
                    var newId = saveResponseNewObject.ni;
                    var id = saveResponseNewObject.i;

                    var newWorkspaceObject = this.newWorkspaceObjectById[newId];
                    delete (this.newWorkspaceObjectById[newId]);

                    newWorkspaceObject.id = id;
                    delete(newWorkspaceObject.newId);

                    this.workspaceObjectById[id] = newWorkspaceObject;
                });
            }

            if (Object.getOwnPropertyNames(this.newWorkspaceObjectById).length !== 0) {
                throw "Not all new objects received ids";
            }
        }

        sync(): void {
            _.forEach(this.workspaceObjectById, v => {
                v.sync();
            });
        }
    }
}