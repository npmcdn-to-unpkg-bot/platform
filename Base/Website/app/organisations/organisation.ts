module App.Organisation {
    interface IOrganisationModel {
        title: string;
    }

    interface IOrganisationState extends ng.ui.IState {
    }

    class OrganisationController implements IOrganisationModel {
        title: string;

        static $inject = ["$http", "$state", "allorsService"];
        constructor(private $http: ng.IHttpService, private $state: IOrganisationState, private allorsService: Allors.Service) {
            this.title = "Organisation";
        }
     
    }
    angular
        .module("app")
        .controller("organisationController",
            OrganisationController);

}
