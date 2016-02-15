namespace App.Relation.Person {
    class AddPersonController {
     
        person: Allors.Domain.Person;

        private context: Allors.Context;
      
        static $inject = ["$rootScope", "$scope", "$http", "$state", "notificationService", "profileService", "databaseService", "workspaceService"];
        constructor(
            private $rootScope: ng.IRootScopeService,
            private $scope: ng.IScope,
            private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            private notificationService: NotificationService,
            databaseService: Allors.DatabaseService,
            workspaceService: Allors.WorkspaceService) {

            this.context = new Allors.Context("AddPerson", databaseService, workspaceService);

            this.person = this.context.session.create("Person");
            
            this.refresh()
                .then(() => {
                    this.$scope.$on("refresh", () => {
                        this.refresh();
                    });
                });
        }
        
        getTopicTypeAhead(criteria: string) {
            //return this.context.queryResults("TopicTypeAhead", { criteria: criteria });
        }

        getLanguageTypeAhead(criteria: string) {
            //return this.context.queryResults("LanguageTypeAhead", { criteria: criteria });
        }

        cancel(): void {
            this.return();
        }

        save(): void {
            this.context.save()
                .then(() => {
                    this.notificationService.info("Successfully saved.");
                    this.$rootScope.$broadcast("refresh", this.context.name);
                    this.return();
                });
        }

        private refresh(): ng.IPromise<any> {
            return this.context.refresh()
                .then(() => {
                });
        }
        
        private return() {
            this.$state.go("persons");
        }

        private error(responseError: Allors.Data.ResponseError): void {
            this.notificationService.responseError(responseError);
        }
    }
    angular
        .module("app")
        .controller("relationAddPersonController",
            AddPersonController);

}
