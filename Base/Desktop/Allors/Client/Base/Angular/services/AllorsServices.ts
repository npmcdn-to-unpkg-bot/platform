namespace Allors {
    export class AllorsServices {
        static $inject = ["databaseService", "workspaceService", "eventService"];
        constructor(
            public databaseService: DatabaseService,
            public workspaceService: WorkspaceService,
            public eventService: EventService) {
        }
    }

    angular.module("allors")
        .service("allorsService",
        AllorsServices);
}