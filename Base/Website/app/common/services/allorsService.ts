module App.Common.Services
{
    export class AllorsService {

        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) {

        }

    }

    angular
        .module("app")
        .service("allorsService",
            AllorsService);

}