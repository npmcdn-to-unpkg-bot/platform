module Allors {
    export class Context {
        constructor(
            public workspace: Allors.Workspace,
            public objectByName: { [name: string]: Allors.WorkspaceObject; }) {
        }
    }
}