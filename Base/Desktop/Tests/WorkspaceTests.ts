namespace Tests {

    export class WorkspaceTests extends tsUnit.TestClass {

        load() {
            const workspace = new Allors.Workspace(Allors.Data.metaPopulation);
            workspace.load(Fixture.loadData);

            var martien = workspace.get("3");

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
            const workspace = new Allors.Workspace(Allors.Data.metaPopulation);
            workspace.userSecurityHash = "#";
            workspace.load(Fixture.loadData);

            var required =
                {
                    userSecurityHash: "#",
                    objects: [
                        ["1", "1001"],
                        ["2", "1002"],
                        ["3", "1004"]
                    ]
                }

            var requireLoad = workspace.diff(required);

            this.areIdentical(1, requireLoad.objects.length);
        }

        checkVersionsUserSecurityHash() {
            const workspace = new Allors.Workspace(Allors.Data.metaPopulation);
            workspace.userSecurityHash = "abc";
            workspace.load(Fixture.loadData);

            var required =
                {
                    userSecurityHash: "def",
                    objects: [
                        ["1", "1001"],
                        ["2", "1002"],
                        ["3", "1004"]
                    ]
                }

            var requireLoad = workspace.diff(required);

            this.areIdentical(3, requireLoad.objects.length);
        }
    }
}