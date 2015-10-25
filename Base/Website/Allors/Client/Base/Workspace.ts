﻿module Allors {
    export interface IWorkspace {
        isChanged: boolean;

        get(id: string): any;
        save(): Data.SaveRequest;
        sync(): void;
    }

    export class Workspace {
        private database: IDatabase;
        private workspaceObjectById: { [id: string]: WorkspaceObject; } = {};

        constructor(database: IDatabase) {
            this.database = database;
        }

        get isChanged(): boolean {
            var hasChanges = false;
            _.forEach(this.workspaceObjectById, workspaceObject => {
                if (workspaceObject.isChanged) {
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
        
        sync(): void {
            _.forEach(this.workspaceObjectById, v => {
                v.sync();
            });
        }
    }
}