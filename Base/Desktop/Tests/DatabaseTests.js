var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Tests;
(function (Tests) {
    var DatabaseTests = (function (_super) {
        __extends(DatabaseTests, _super);
        function DatabaseTests() {
            _super.apply(this, arguments);
        }
        DatabaseTests.prototype.load = function () {
            var database = new Allors.Database(Allors.Meta.population);
            database.load(Tests.Fixture.loadData);
            var martien = database.get("3");
            this.areIdentical("3", martien.id);
            this.areIdentical("1003", martien.version);
            this.areIdentical("Person", martien.objectType.name);
            this.areIdentical("Martien", martien.roles.FirstName);
            this.areIdentical("van", martien.roles.MiddleName);
            this.areIdentical("Knippenberg", martien.roles.LastName);
            this.areIdentical(undefined, martien.roles.IsStudent);
            this.areIdentical(undefined, martien.roles.BirthDate);
        };
        DatabaseTests.prototype.checkVersions = function () {
            var database = new Allors.Database(Allors.Meta.population);
            database.load(Tests.Fixture.loadData);
            var required = {
                objects: [
                    ["1", "1001"],
                    ["2", "1002"],
                    ["3", "1004"]
                ]
            };
            var requireLoad = database.check(required);
            this.areIdentical(1, requireLoad.objects.length);
        };
        return DatabaseTests;
    })(tsUnit.TestClass);
    Tests.DatabaseTests = DatabaseTests;
})(Tests || (Tests = {}));
//# sourceMappingURL=DatabaseTests.js.map