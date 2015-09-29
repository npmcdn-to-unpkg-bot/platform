var Allors;
(function (Allors) {
    var Domain;
    (function (Domain) {
        var Test;
        (function (Test) {
            var User = (function () {
                function User() {
                }
                Object.defineProperty(User.prototype, "Mail", {
                    get: function () {
                        return this.UserEmail;
                    },
                    enumerable: true,
                    configurable: true
                });
                return User;
            })();
            Test.User = User;
        })(Test = Domain.Test || (Domain.Test = {}));
    })(Domain = Allors.Domain || (Allors.Domain = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=User.js.map