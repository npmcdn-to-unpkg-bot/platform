namespace Allors {
    export class WorkspaceService {
        workspace: Workspace;

        static $inject = [];
        constructor() {
            this.workspace = new Workspace(Data.metaPopulation);
        }
    }

    angular.module("allors")
        .service("workspaceService",
        WorkspaceService);
}