module Allors {
    export class Context {
        constructor(
            public workspace: Allors.IWorkspace,
            public objects: { [name: string]: Allors.WorkspaceObject; } ,
            public collections: { [name: string]: Allors.WorkspaceObject[]; },
            public values: { [name: string]: Allors.WorkspaceObject; }) {
        }
    }
}