var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var App;
(function (App) {
    var Method = Allors.Method;
    var Page = (function (_super) {
        __extends(Page, _super);
        function Page(name, allors, $scope) {
            _super.call(this, name, allors.database, allors.workspace, allors.$rootScope, $scope, allors.$q, allors.$log);
            this.toastr = allors.toastr;
        }
        Page.prototype.save = function () {
            var _this = this;
            return this.$q(function (resolve, reject) {
                _super.prototype.save.call(_this)
                    .then(function (saveResponse) {
                    _this.toastr.info("Successfully saved.");
                    resolve(saveResponse);
                })
                    .catch(function (e) {
                    _this.errorResponse(e.reponseError);
                    reject(e);
                });
            });
        };
        Page.prototype.invoke = function (methodOrService, args) {
            var _this = this;
            return this.$q(function (resolve, reject) {
                var superInvoke;
                if (methodOrService instanceof Method) {
                    superInvoke = _super.prototype.invoke.call(_this, methodOrService);
                }
                else {
                    superInvoke = _super.prototype.invoke.call(_this, methodOrService, args);
                }
                superInvoke.then(function (invokeResponse) {
                    _this.toastr.info("Successfully executed.");
                    resolve(invokeResponse);
                })
                    .catch(function (e) {
                    _this.errorResponse(e.reponseError);
                    reject(e);
                });
            });
        };
        Page.prototype.saveAndInvoke = function (methodOrService, args) {
            var _this = this;
            return this.$q(function (resolve, reject) {
                var superSaveAndInvoke;
                if (methodOrService instanceof Method) {
                    superSaveAndInvoke = _super.prototype.saveAndInvoke.call(_this, methodOrService);
                }
                else {
                    superSaveAndInvoke = _super.prototype.saveAndInvoke.call(_this, methodOrService, args);
                }
                superSaveAndInvoke.then(function (invokeResponse) {
                    _this.toastr.info("Successfully executed.");
                    resolve(invokeResponse);
                })
                    .catch(function (e) {
                    _this.errorResponse(e.reponseError);
                    reject(e);
                });
            });
        };
        Page.prototype.errorResponse = function (error) {
            var title;
            var message = "<div class=\"response-errors\">";
            if (error.errorMessage && error.errorMessage.length > 0) {
                title = "General Error";
                message += "<p>" + error.errorMessage + "</p>";
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
                error.derivationErrors.map(function (derivationError) {
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
        };
        return Page;
    }(Allors.Control));
    App.Page = Page;
})(App || (App = {}));
//# sourceMappingURL=Page.js.map