var App;
(function (App) {
    config.$inject = ["cfpLoadingBarProvider"];
    function config(loadingBar) {
        loadingBar.includeSpinner = false;
    }
    angular
        .module("app")
        .config(config);
})(App || (App = {}));
//# sourceMappingURL=app.loading.js.map