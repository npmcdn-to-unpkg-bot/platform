namespace App {
    var app = angular.module("app");

    app.config(config);

    config.$inject = ["$translateProvider"];
    function config($translateProvider: angular.translate.ITranslateProvider): void {

        $translateProvider.useUrlLoader("/angular/translate");

        $translateProvider.preferredLanguage("en");
        $translateProvider.fallbackLanguage("en");

        $translateProvider.registerAvailableLanguageKeys(["en", "fr", "nl"], {
            'en_*': "en",
            'fr_*': "fr",
            'nl_*': "nl"
        });

        $translateProvider.determinePreferredLanguage();
    }
}
