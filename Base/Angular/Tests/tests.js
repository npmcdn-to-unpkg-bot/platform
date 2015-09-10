/// <reference path="/allors/DatabaseObject.js" />
/// <reference path="/allors/Database.js" />
/// <reference path="/allors/ObjectType.js" />
/// <reference path="/allors/RoleType.js" />
/// <reference path="/allors/WorkspaceObject.js" />
/// <reference path="/allors/Workspace.js" />
/// <reference path="/Generated/meta.g.js" />
/// <reference path="data.js" />

test("database load", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var martien = database.get("3");
    
    ok(martien.id === "3");
    ok(martien.version === "12");
    ok(martien.FirstName === "Martien");
    ok(martien.LastName === "van Knippenberg");
});

test("workspace unit get", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);

    var martien = workspace.get("3");

    ok(martien.FirstName === "Martien");
    ok(martien.LastName === "van Knippenberg");
});

test("workspace unit set", function () {
    var database = new Allors.Database(Allors.Data.meta);
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

test("workspace unit save", function () {
    var database = new Allors.Database(Allors.Data.meta);
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

test("workspace composite get", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);

    var koen = workspace.get("1");
    var patrick = workspace.get("2");
    var martien = workspace.get("3");

    var acme = workspace.get("4");
    var ocme = workspace.get("5");
    var icme = workspace.get("6");

    ok(acme.Owner === koen);
    ok(ocme.Owner === patrick);
    ok(icme.Owner === martien);
});

test("workspace composite set", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var workspace1 = new Allors.Workspace(database);

    var workspace2 = new Allors.Workspace(database);

    var koen1 = workspace1.get("1");
    var patrick1 = workspace1.get("2");
    var martien1 = workspace1.get("3");

    var acme1 = workspace1.get("4");
    var ocme1 = workspace1.get("5");
    var icme1 = workspace1.get("6");

    var koen2 = workspace2.get("1");
    var patrick2 = workspace2.get("2");
    var martien2 = workspace2.get("3");

    var acme2 = workspace2.get("4");
    var ocme2 = workspace2.get("5");
    var icme2 = workspace2.get("6");

    acme2.Owner = martien2;
    ocme2.Owner = null;

    ok(acme1.Owner === koen1);
    ok(ocme1.Owner === patrick1);
    ok(icme1.Owner === martien1);

    ok(acme2.Owner === martien2);
    ok(ocme2.Owner === null);
    ok(icme2.Owner === martien2);
});
