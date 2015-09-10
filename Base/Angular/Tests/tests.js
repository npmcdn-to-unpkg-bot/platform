/// <reference path="/allors/DatabaseObject.js" />
/// <reference path="/allors/Database.js" />
/// <reference path="/allors/ObjectType.js" />
/// <reference path="/allors/RoleType.js" />
/// <reference path="/allors/WorkspaceObject.js" />
/// <reference path="/allors/Workspace.js" />
/// <reference path="/Generated/meta.g.js" />
/// <reference path="data.js" />

test("database update", function () {
    var database = new Allors.Database();
    database.load(fixture.loadData);

    var martien = database.get("3");
    
    ok(martien.id === "3");
    ok(martien.version === "12");
    ok(martien.FirstName === "Martien");
    ok(martien.LastName === "van Knippenberg");
});

test("workspace get", function () {
    var database = new Allors.Database();
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);

    var martien = workspace.get("3");

    ok(martien.FirstName === "Martien");
    ok(martien.LastName === "van Knippenberg");
});

test("workspace set", function () {
    var database = new Allors.Database();
    database.load(fixture.loadData);

    var workspace1 = new Allors.Workspace(database);
    var martien1 = workspace1.get("3");

    var workspace2 = new Allors.Workspace(database);
    var martien2 = workspace2.get("3");

    martien2.FirstName = "Martinus";

    ok(martien1.FirstName === "Martien");
    ok(martien1.LastName === "van Knippenberg");

    ok(martien2.FirstName === "Martinus");
    ok(martien2.LastName === "van Knippenberg");
});

test("workspace diff", function () {
    var database = new Allors.Database();
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);
    var koen = workspace.get("1");
    var patrick = workspace.get("2");
    var martien = workspace.get("3");

    koen.FirstName = "K";
    koen.LastName = "VE";
    martien.FirstName = "Martinus";

    var save = workspace.save();

    ok(save.objects.length === 2);
});

