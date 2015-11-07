var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Tests;
(function (Tests) {
    var PersonTests = (function (_super) {
        __extends(PersonTests, _super);
        function PersonTests() {
            _super.apply(this, arguments);
        }
        PersonTests.prototype.fullName = function () {
            var database = new Allors.Database(Allors.Meta.population);
            var workspace = new Allors.Workspace(database);
            var person = workspace.create("Person");
            this.areIdentical("N/A", person.FullName);
            person.LastName = "Doe";
            this.areIdentical("Doe", person.FullName);
            person.FirstName = "John";
            this.areIdentical("John Doe", person.FullName);
        };
        return PersonTests;
    })(tsUnit.TestClass);
    Tests.PersonTests = PersonTests;
})(Tests || (Tests = {}));
