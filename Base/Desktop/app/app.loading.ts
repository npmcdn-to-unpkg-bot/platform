namespace App
{
    config.$inject = ["cfpLoadingBarProvider"];
    function config(loadingBar: any): void {
        loadingBar.includeSpinner = true;
    }
    angular
        .module("app")
        .config(config);
}
