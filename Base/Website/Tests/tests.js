/// <reference path="../Scripts/lodash.js" />

/// <reference path="../Allors/Client/Base/Database.js" />
/// <reference path="../Allors/Client/Base/DatabaseObject.js" />
/// <reference path="../Allors/Client/Base/WorkspaceObject.js" />
/// <reference path="../Allors/Client/Base/Workspace.js" />
/// <reference path="../Allors/Client/Base/ObjectType.js" />
/// <reference path="../Allors/Client/Base/RoleType.js" />

/// <reference path="../Allors/Client/Base/Meta/Population.js" />

/// <reference path="../Allors/Client/Base/Data/Response.js" />
/// <reference path="../Allors/Client/Base/Data/LoadRequest.js" />
/// <reference path="../Allors/Client/Base/Data/LoadResponse.js" />
/// <reference path="../Allors/Client/Base/Data/SaveRequest.js" />

/// <reference path="../Allors/Client/Generated/meta/meta.g.js" />
/// <reference path="../Allors/Client/Generated/domain/c1.g.js" />
/// <reference path="../Allors/Client/Generated/domain/i1.g.js" />
/// <reference path="../Allors/Client/Generated/domain/media.g.js" />
/// <reference path="../Allors/Client/Generated/domain/organisation.g.js" />
/// <reference path="../Allors/Client/Generated/domain/person.g.js" />

/// <reference path="./qunit.js" />
/// <reference path="./data.js" />

function arrayEqual(array1, array2)
{
    return _.isEmpty(_.difference(array1, array2)) && _.isEmpty(_.difference(array2, array1));
}

test("database load", function () {
    var database = new Allors.Database(Allors.Meta.population);
    database.load(fixture.loadData);

    var martien = database.get("3");
    
    ok(martien.id === "3");
    ok(martien.version === "1003");
    ok(martien.objectType.name === "Person");
    ok(martien.roles.FirstName === "Martien");
    ok(martien.roles.MiddleName === "van");
    ok(martien.roles.LastName === "Knippenberg");
    ok(martien.roles.IsStudent === undefined);
    ok(martien.roles.BirthDate === undefined);
});

test("workspace unit get", function () {
    var database = new Allors.Database(Allors.Meta.population);
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);

    var koen = workspace.get("1");

    ok(koen.FirstName === "Koen");
    ok(koen.MiddleName === null);
    ok(koen.LastName === "Van Exem");
    ok(koen.BirthDate.toUTCString() === new Date("1973-03-27T18:00:00Z").toUTCString());
    ok(koen.IsStudent === true);
    
    var patrick = workspace.get("2");

    ok(patrick.FirstName === "Patrick");
    ok(patrick.LastName === "De Boeck");
    ok(patrick.MiddleName === null);
    ok(patrick.BirthDate === null);
    ok(patrick.IsStudent === false);

    var martien = workspace.get("3");

    ok(martien.FirstName === "Martien");
    ok(martien.LastName === "Knippenberg");
    ok(martien.MiddleName === "van");
    ok(martien.BirthDate === null);
    ok(martien.IsStudent === null);
});

test("workspace unit set", function () {
    var database = new Allors.Database(Allors.Meta.population);
    database.load(fixture.loadData);

    var workspace1 = new Allors.Workspace(database);
    var martien1 = workspace1.get("3");

    var workspace2 = new Allors.Workspace(database);
    var martien2 = workspace2.get("3");

    martien2.FirstName = "Martinus";
    martien2.MiddleName = "X";

    ok(martien1.FirstName === "Martien");
    ok(martien1.LastName === "Knippenberg");
    ok(martien1.MiddleName === "van");

    ok(martien2.FirstName === "Martinus");
    ok(martien2.LastName === "Knippenberg");
    ok(martien2.MiddleName === "X");
});

test("workspace unit save", function() {
    var database = new Allors.Database(Allors.Meta.population);
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

    var savedKoen = _.find(save.objects, function(v) { return v.i === "1" });

    ok(savedKoen.v === "1001");
    ok(savedKoen.roles.length === 2);

    var savedKoenFirstName = _.find(savedKoen.roles, function(v) { return v.t === "FirstName" });
    var savedKoenLastName = _.find(savedKoen.roles, function (v) { return v.t === "LastName" });

    ok(savedKoenFirstName.s === "K");
    ok(savedKoenFirstName.a === undefined);
    ok(savedKoenFirstName.r === undefined);
    ok(savedKoenLastName.s === "VE");
    ok(savedKoenLastName.a === undefined);
    ok(savedKoenLastName.r === undefined);

    var savedMartien = _.find(save.objects, function(v) { return v.i === "3" });

    ok(savedMartien.v === "1003");
    ok(savedMartien.roles.length === 2);

    var savedMartienFirstName = _.find(savedMartien.roles, function (v) { return v.t === "FirstName" });
    var savedMartienMiddleName = _.find(savedMartien.roles, function (v) { return v.t === "MiddleName" });

    ok(savedMartienFirstName.s === "Martinus");
    ok(savedMartienFirstName.a === undefined);
    ok(savedMartienFirstName.r === undefined);
    ok(savedMartienMiddleName.s === "X");
    ok(savedMartienMiddleName.a === undefined);
    ok(savedMartienMiddleName.r === undefined);
});

test("workspace one get", function () {
    var database = new Allors.Database(Allors.Meta.population);
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
    var database = new Allors.Database(Allors.Meta.population);
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

    ok(acme2.Owner === martien2); //x
    ok(ocme2.Owner === null);
    ok(icme2.Owner === martien2);

    ok(acme2.Manager === patrick2); //x
    ok(ocme2.Manager === null);
    ok(icme2.Manager === null);
});

test("workspace one save", function () {
    var database = new Allors.Database(Allors.Meta.population);
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

    var savedAcme = _.find(save.objects, function (v) { return v.i === "101" });

    ok(savedAcme.v === "1101");
    ok(savedAcme.roles.length === 2);

    var savedAcmeOwner = _.find(savedAcme.roles, function (v) { return v.t === "Owner" });
    var savedAcmeManager = _.find(savedAcme.roles, function (v) { return v.t === "Manager" });

    ok(savedAcmeOwner.s === "3");
    ok(savedAcmeOwner.a === undefined);
    ok(savedAcmeOwner.r === undefined);
    ok(savedAcmeManager.s === "2");
    ok(savedAcmeManager.a === undefined);
    ok(savedAcmeManager.r === undefined);

    var savedOcme = _.find(save.objects, function (v) { return v.i === "102" });

    ok(savedOcme.v === "1102");
    ok(savedOcme.roles.length === 1);

    var savedOcmeOwner = _.find(savedOcme.roles, function (v) { return v.t === "Owner" });

    ok(savedOcmeOwner.s === null);
    ok(savedOcmeOwner.a === undefined);
    ok(savedOcmeOwner.r === undefined);
});

test("workspace many get", function () {
    var database = new Allors.Database(Allors.Meta.population);
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
    var database = new Allors.Database(Allors.Meta.population);
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

test("workspace many save with existing objects", function () {
    var database = new Allors.Database(Allors.Meta.population);
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);

    var koen = workspace.get("1");
    var patrick = workspace.get("2");
    var martien = workspace.get("3");

    var acme = workspace.get("101");
    var ocme = workspace.get("102");
    var icme = workspace.get("103");

    acme.Employees = null;
    ocme.Employees = [martien, patrick];
    icme.Employees = [koen, patrick, martien];

    var save = workspace.save();

    ok(save.newObjects.length === 0);
    ok(save.objects.length === 3);

    var savedAcme = _.find(save.objects, function (v) { return v.i === "101" });

    ok(savedAcme.v === "1101");
    ok(savedAcme.roles.length === 1);

    var savedAcmeEmployees = _.find(savedAcme.roles, function (v) { return v.t === "Employees" });

    ok(arrayEqual(savedAcmeEmployees.s, []));
    ok(savedAcmeEmployees.a === undefined);
    ok(savedAcmeEmployees.r === undefined);

    var savedOcme = _.find(save.objects, function (v) { return v.i === "102" });

    ok(savedOcme.v === "1102");
    ok(savedOcme.roles.length === 1);

    var savedOcmeEmployees = _.find(savedOcme.roles, function (v) { return v.t === "Employees" });

    ok(savedOcmeEmployees.s === undefined);
    ok(arrayEqual(savedOcmeEmployees.a, ["2", "3"]));
    ok(arrayEqual(savedOcmeEmployees.r, ["1"]));

    var savedIcme = _.find(save.objects, function (v) { return v.i === "103" });

    ok(savedIcme.v === "1103");
    ok(savedIcme.roles.length === 1);

    var savedIcmeEmployees = _.find(savedIcme.roles, function (v) { return v.t === "Employees" });

    ok(arrayEqual(savedIcmeEmployees.s, ["1", "2", "3"]));
    ok(savedIcmeEmployees.a === undefined);
    ok(savedIcmeEmployees.r === undefined);
});

test("workspace many save with new objects", function () {
    var database = new Allors.Database(Allors.Meta.population);
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);

    var martien = workspace.get("3");

    var mathijs = workspace.create("Person");
    mathijs.FirstName = "Mathijs";
    mathijs.LastName = "Verwer";

    var acme2 = workspace.create("Organisation");
    acme2.Name = "Acme 2";
    acme2.Manager = mathijs;
    acme2.AddEmployee(mathijs);

    var acme3 = workspace.create("Organisation");
    acme3.Name = "Acme 3";
    acme3.Manager = martien;
    acme3.AddEmployee(martien);

    var save = workspace.save();

    ok(save.newObjects.length === 3);
    ok(save.objects.length === 0);
    {
        var savedMathijs = _.find(save.newObjects, function(v) { return v.ni === "-1" });

        ok(savedMathijs.t === "Person");
        ok(savedMathijs.roles.length === 2);

        var savedMathijsFirstName = _.find(savedMathijs.roles, function(v) { return v.t === "FirstName" });
        ok(savedMathijsFirstName.s === "Mathijs");

        var savedMathijsLastName = _.find(savedMathijs.roles, function(v) { return v.t === "LastName" });
        ok(savedMathijsLastName.s === "Verwer");
    }

    {
        var savedAcme2 = _.find(save.newObjects, function(v) { return v.ni === "-2" });

        ok(savedAcme2.t === "Organisation");
        ok(savedAcme2.roles.length === 3);

        var savedAcme2Manager = _.find(savedAcme2.roles, function(v) { return v.t === "Manager" });

        ok(savedAcme2Manager.s === "-1");

        var savedAcme2Employees = _.find(savedAcme2.roles, function(v) { return v.t === "Employees" });

        ok(arrayEqual(savedAcme2Employees.s, ["-1"]));
        ok(savedAcme2Employees.a === undefined);
        ok(savedAcme2Employees.r === undefined);
    }

    {
        var savedAcme3 = _.find(save.newObjects, function (v) { return v.ni === "-3" });

        ok(savedAcme3.t === "Organisation");
        ok(savedAcme3.roles.length === 3);

        var savedAcme3Manager = _.find(savedAcme3.roles, function (v) { return v.t === "Manager" });

        ok(savedAcme3Manager.s === "3");

        var savedAcme3Employees = _.find(savedAcme3.roles, function (v) { return v.t === "Employees" });

        ok(arrayEqual(savedAcme3Employees.s, ["3"]));
        ok(savedAcme3Employees.a === undefined);
        ok(savedAcme3Employees.r === undefined);
    }

});


test("workspace method canExecute", function () {
    var database = new Allors.Database(Allors.Meta.population);
    database.load(fixture.loadData);

    var workspace = new Allors.Workspace(database);

    var acme = workspace.get("101");
    var ocme = workspace.get("102");
    var icme = workspace.get("102");

    ok(acme.CanExecuteJustDoIt === true);
    ok(ocme.CanExecuteJustDoIt === false);
    ok(icme.CanExecuteJustDoIt === false);
});

test("database check versions", function () {
    var database = new Allors.Database(Allors.Meta.population);
    database.load(fixture.loadData);

    var required = 
    {
        objects: [
            ["1", "1001"],
            ["2", "1002"],
            ["3", "1004"]
        ]
    }

    var requireLoad = database.check(required);

    ok(requireLoad.objects.length === 1);
});
