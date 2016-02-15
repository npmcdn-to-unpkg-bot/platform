namespace Allors {
    export class EventService {
        static $inject = ["$rootScope", "$q"];
        constructor(public $rootScope: ng.IRootScopeService, public $q: ng.IQService) {
        }
    }

    angular.module("allors")
        .service("eventService",
        EventService);
}