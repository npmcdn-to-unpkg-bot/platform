namespace App {
    export abstract class Control extends Allors.Control {

        toastr: angular.toastr.IToastrService;

        constructor(name: string, allors: AllorsService, $scope: ng.IScope) {
            super(name, allors.database, allors.workspace, allors.$rootScope, $scope);

            this.toastr = allors.toastr;
        }

        save(): ng.IPromise<any> {
            return super.save()
                .then(saveResponse => {
                    this.toastr.info("successfully saved");
                    return saveResponse;
                })
                .catch(saveResponse => {
                    this.responseError(saveResponse);
                    return saveResponse;
                });
        }

        private responseError(error: Allors.Data.ResponseError) {
            let title: string;
            var message = "<div class=\"response-errors\">";

            if (error.errorMessage && error.errorMessage.length > 0) {
                title = "General Error";
                message += `<p>${error.errorMessage}</p>`;
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
                    message += `<li>${derivationError.m}</li>`;
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
}