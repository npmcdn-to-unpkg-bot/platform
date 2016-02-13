module App
{
    export class NotificationService {

        static $inject = ["toastr"];
        constructor(private toastr) {
        }

        info(message: string, title?: string) {
            this.toastr.info(message, title);
        }

        error(message: string, title?: string) {
            this.toastr.error(message, title);
        }

        responseError(error: Allors.Data.ResponseError) {
            var title;
            var message = "<div class=\"response-errors\">";

            if (error.errorMessage && error.errorMessage.length > 0) {
                title = "General Error";
                message += "<p>"+ error.errorMessage +"</p>";
            }

            if ((error.versionErrors && error.versionErrors.length > 0) ||
                (error.missingErrors && error.missingErrors.length > 0)) {
                title = "Concurrency Error";
                message += "<p>Modifications were detected since last access.</p>";
            }

            if (error.accessErrors && error.accessErrors.length > 0) {
                title = "Access Error";
                message += "<p>You do not have the required rights.</p>";
            }

            if (error.derivationErrors && error.derivationErrors.length > 0) {
                title = "Derivation Errors";

                message += "<ul>";
                error.derivationErrors.map(derivationError => {
                    message += "<li>" + derivationError.m + "</li>";
                });

                message += "</ul>";
            }

            message += "<div>";

            this.toastr.error(message, title, {
                allowHtml: true,
                closeButton: true,
                timeOut: 0
        });
        }

    }

    angular
        .module("app")
        .service("notificationService",
        NotificationService);
}