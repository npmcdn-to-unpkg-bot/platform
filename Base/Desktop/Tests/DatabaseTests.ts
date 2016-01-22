module Tests {

    export class DatabaseTests extends tsUnit.TestClass {

        load() {
            var database = new Allors.Database(Allors.Meta.population);
            database.load(Fixture.loadData);

            var martien = database.get("3");

            this.areIdentical("3", martien.id);
            this.areIdentical("1003", martien.version);
            this.areIdentical("Person", martien.objectType.name);
            this.areIdentical("Martien", martien.roles.FirstName);
            this.areIdentical("van", martien.roles.MiddleName);
            this.areIdentical("Knippenberg", martien.roles.LastName);
            this.areIdentical(undefined, martien.roles.IsStudent);
            this.areIdentical(undefined, martien.roles.BirthDate);
        }

        checkVersions() {
            var database = new Allors.Database(Allors.Meta.population);
            database.load(Fixture.loadData);

            var required =
                {
                    userSecurityHash: "#",
                    objects: [
                        ["1", "1001"],
                        ["2", "1002"],
                        ["3", "1004"]
                    ]
                }

            var requireLoad = database.check(required);

            this.areIdentical(1, requireLoad.objects.length);
        }
    }
}