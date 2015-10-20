module Allors {
    export class Result {
        objects: { [name: string]: Allors.WorkspaceObject; } = {};
        collections: { [name: string]: Allors.WorkspaceObject[]; } = {};
        values: { [name: string]: any; } = {};
    }
}