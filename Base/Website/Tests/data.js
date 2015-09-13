fixture = {};
(function (fixture) {
    fixture.loadData = {
        objects: [
            {
                i: "1",
                v: "1001",
                t: "Person",
                roles: [
                    ["FirstName", "Koen"],
                    ["LastName", "Van Exem"]
                ]
            },
            {
                i: "2",
                v: "1002",
                t: "Person",
                roles: [
                    ["FirstName", "Patrick"],
                    ["LastName", "De Boeck"]
                ]
            },
            {
                i: "3",
                v: "1003",
                t: "Person",
                roles: [
                    ["FirstName", "Martien"],
                    ["LastName", "van Knippenberg"]
                ]
            },
            {
                i: "101",
                v: "1101",
                t: "Organisation",
                roles: [
                    ["Name", "Acme"],
                    ["Owner", "1"],
                    ["Employees", ["1","2","3"]]
                ]
            },
            {
                i: "102",
                v: "1102",
                t: "Organisation",
                roles: [
                    ["Name", "Ocme"],
                    ["Owner", "2"],
                    ["Employees", ["1"]]
                ]
            },
            {
                i: "103",
                v: "1103",
                t: "Organisation",
                roles: [
                    ["Name", "icme"],
                    ["Owner", "3"]
                ]
            }
        ]
    };
})(fixture);


