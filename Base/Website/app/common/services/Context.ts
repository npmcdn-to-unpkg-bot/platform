module Allors {
    export class Context {
        constructor(
            public workspace: Allors.Workspace,
            public objects: { [name: string]: Allors.WorkspaceObject; } ,
            public collections: { [name: string]: Allors.WorkspaceObject[]; },
            public values: { [name: string]: Allors.WorkspaceObject; }) {
        }
    }
}