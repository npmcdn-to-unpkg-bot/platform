var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Allors;
(function (Allors) {
    var Domain;
    (function (Domain) {
        var Tests;
        (function (Tests) {
            var Person = (function (_super) {
                __extends(Person, _super);
                function Person() {
                    _super.apply(this, arguments);
                }
                Object.defineProperty(Person.prototype, "FullName", {
                    get: function () {
                        return this.FirstName + " " + this.LastName;
                    },
                    enumerable: true,
                    configurable: true
                });
                Person.prototype.greeting = function () {
                    this.field = "I'm a field";
                    return "Hello " + this.FirstName;
                };
                return Person;
            })(Allors.Domain.Person);
            Tests.Person = Person;
        })(Tests = Domain.Tests || (Domain.Tests = {}));
    })(Domain = Allors.Domain || (Allors.Domain = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=Person.js.map