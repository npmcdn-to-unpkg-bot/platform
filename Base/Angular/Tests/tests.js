/// <reference path="/allors/Class.js" />
/// <reference path="/allors/RoleType.js" />
/// <reference path="/allors/DatabaseObject.js" />
/// <reference path="/allors/Database.js" />
/// <reference path="/allors/WorkspaceObject.js" />
/// <reference path="/allors/Workspace.js" />
/// <reference path="/Generated/meta.g.js" />
/// <reference path="data.js" />

test("meta classes", function () {
    var classes = Allors.Meta.classes;

    var c1 = classes["C1"];

    ok(c1.name === "C1");
});


test("database update", function () {
    var objectTypeByName = {
        "Person": {
            name: "Person",
            roles: {
                "FirstName": {
                    name: "FirstName",
                    type: "AllorsString"
                },
                "FirstName": {
                    name: "FirstName",
                    type: "AllorsString"
                }
            }
        }
    };

    var database = new Allors.Database(objectTypeByName);

    var updates = [
    {
        objectType : "Person", 
        objects : [
            {
                id: "1",
                version: "10",
                FirstName: "Koen",
                LastName: "Van Exem"
            },
            {
                id: "2",
                version: "11",
                FirstName: "Patrick",
                LastName: "De Boeck"
            },
            {
                id: "3",
                version: "12",
                FirstName: "Martien",
                LastName: "van Knippenberg"
            }
        ]
        }
    ];

    database.update(updates);

    var martien = database.get("3");
    
    ok(martien.id === "3");
    ok(martien.version === "12");
    ok(martien.FirstName === "Martien");
    ok(martien.LastName === "van Knippenberg");
});

