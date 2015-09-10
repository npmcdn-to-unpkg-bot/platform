/// <reference path="../Scripts/lodash.js" />

/// <reference path="../allors/Database.js" />
/// <reference path="../allors/DatabaseObject.js" />
/// <reference path="../allors/WorkspaceObject.js" />
/// <reference path="../allors/Workspace.js" />
/// <reference path="../allors/ObjectType.js" />
/// <reference path="../allors/RoleType.js" />

/// <reference path="../allors/Data/IdData.js" />
/// <reference path="../allors/Data/IdWithVersionData.js" />
/// <reference path="../allors/Data/MetaData.js" />
/// <reference path="../allors/Data/LoadData.js" />
/// <reference path="../allors/Data/SaveData.js" />

/// <reference path="../Generated/meta.g.js" />
/// <reference path="../Generated/organisation.g.js" />
/// <reference path="../Generated/person.g.js" />

/// <reference path="./data.js" />

function arrayEqual(array1, array2)
{
    return _.isEmpty(_.difference(array1, array2)) && _.isEmpty(_.difference(array2, array1));
}

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
    ok(martien.version === "1003");
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
    ok(martien.MiddleName === null);
});

test("workspace unit set", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var workspace1 = new Allors.Workspace(database);
    var martien1 = workspace1.get("3");

    var workspace2 = new Allors.Workspace(database);
    var martien2 = workspace2.get("3");

    martien2.FirstName = "Martinus";
    martien2.MiddleName = "X";

    ok(martien1.FirstName === "Martien");
    ok(martien1.LastName === "van Knippenberg");
    ok(martien1.MiddleName === null);

    ok(martien2.FirstName === "Martinus");
    ok(martien2.LastName === "van Knippenberg");
    ok(martien2.MiddleName === "X");
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
    martien.MiddleName = "X";

    var save = workspace.save();

    ok(save.objects.length === 2);
});

test("workspace one get", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);

    var koen = workspace.get("1");
    var patrick = workspace.get("2");
    var martien = workspace.get("3");

    var acme = workspace.get("101");
    var ocme = workspace.get("102");
    var icme = workspace.get("103");

    ok(acme.Owner === koen);
    ok(ocme.Owner === patrick);
    ok(icme.Owner === martien);

    ok(acme.Manager === null);
    ok(ocme.Manager === null);
    ok(icme.Manager === null);
});

test("workspace one set", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var workspace1 = new Allors.Workspace(database);

    var workspace2 = new Allors.Workspace(database);

    var koen1 = workspace1.get("1");
    var patrick1 = workspace1.get("2");
    var martien1 = workspace1.get("3");

    var acme1 = workspace1.get("101");
    var ocme1 = workspace1.get("102");
    var icme1 = workspace1.get("103");

    var koen2 = workspace2.get("1");
    var patrick2 = workspace2.get("2");
    var martien2 = workspace2.get("3");

    var acme2 = workspace2.get("101");
    var ocme2 = workspace2.get("102");
    var icme2 = workspace2.get("103");

    acme2.Owner = martien2;
    ocme2.Owner = null;
    acme2.Manager = patrick2;

    ok(acme1.Owner === koen1);
    ok(ocme1.Owner === patrick1);
    ok(icme1.Owner === martien1);

    ok(acme1.Manager === null);
    ok(ocme1.Manager === null);
    ok(icme1.Manager === null);

    ok(acme2.Owner === martien2);
    ok(ocme2.Owner === null);
    ok(icme2.Owner === martien2);

    ok(acme2.Manager === patrick2);
    ok(ocme2.Manager === null);
    ok(icme2.Manager === null);
});

test("workspace one save", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);
    
    var koen = workspace.get("1");
    var patrick = workspace.get("2");
    var martien = workspace.get("3");

    var acme = workspace.get("101");
    var ocme = workspace.get("102");
    var icme = workspace.get("103");
   
    acme.Owner = martien;
    ocme.Owner = null;

    acme.Manager = patrick;

    var save = workspace.save();

    ok(save.objects.length === 2);
});

test("workspace many get", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);

    var koen = workspace.get("1");
    var patrick = workspace.get("2");
    var martien = workspace.get("3");

    var acme = workspace.get("101");
    var ocme = workspace.get("102");
    var icme = workspace.get("103");

    ok(arrayEqual(acme.Employees, [koen,patrick,martien]));
    ok(arrayEqual(ocme.Employees, [koen]));
    ok(arrayEqual(icme.Employees, []));

    ok(arrayEqual(acme.Shareholders, []));
    ok(arrayEqual(ocme.Shareholders, []));
    ok(arrayEqual(icme.Shareholders, []));
});

test("workspace many set", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var workspace1 = new Allors.Workspace(database);

    var workspace2 = new Allors.Workspace(database);

    var koen1 = workspace1.get("1");
    var patrick1 = workspace1.get("2");
    var martien1 = workspace1.get("3");

    var acme1 = workspace1.get("101");
    var ocme1 = workspace1.get("102");
    var icme1 = workspace1.get("103");

    var koen2 = workspace2.get("1");
    var patrick2 = workspace2.get("2");
    var martien2 = workspace2.get("3");

    var acme2 = workspace2.get("101");
    var ocme2 = workspace2.get("102");
    var icme2 = workspace2.get("103");

    acme2.Employees = null;
    icme2.Employees = [koen2,patrick2,martien2];

    ok(arrayEqual(acme1.Employees, [koen1, patrick1, martien1]));
    ok(arrayEqual(ocme1.Employees, [koen1]));
    ok(arrayEqual(icme1.Employees, []));

    ok(arrayEqual(acme2.Employees, null));
    ok(arrayEqual(ocme2.Employees, [koen2]));
    ok(arrayEqual(icme2.Employees, [koen2, patrick2, martien2]));
});

test("workspace many save", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);

    var koen = workspace.get("1");
    var patrick = workspace.get("2");
    var martien = workspace.get("3");

    var acme = workspace.get("101");
    var ocme = workspace.get("102");
    var icme = workspace.get("103");

    acme.Employees = null;
    icme.Employees = [koen, patrick, martien];

    var save = workspace.save();

    ok(save.objects.length === 2);
});

test("database check versions", function () {
    var database = new Allors.Database(Allors.Data.meta);
    database.load(fixture.loadData);

    var required = 
    {
        idsWithVersion: [
            ["1", "1001"],
            ["2", "1002"],
            ["3", "1004"]
        ]
    }

    var requireLoad = database.check(required);

    ok(requireLoad.ids.length === 1);
});
