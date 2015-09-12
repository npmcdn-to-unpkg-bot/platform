module App.Main {
    class MainController {

        private context: Allors.Context;

        static $inject = ["$http", "allorsService"];
        constructor(private $http: ng.IHttpService, private allorsService: App.Common.Services.AllorsService) {

            this.refresh();

        }

        private refresh(): void{
            this.$http.post('/Angular/Main', {}).then(response => {
                this.allorsService.load(<Allors.Data.Response>response.data)
                    .then(context => this.context = context)
            });
        }

    }
    angular
        .module("app")
        .controller("mainController",
            MainController);

}
