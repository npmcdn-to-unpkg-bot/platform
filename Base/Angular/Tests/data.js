fixture = {};
(function (fixture) {
    fixture.loadData = {
        objects: [
            {
                id: "1",
                version: "1001",
                type: "Person",
                FirstName: "Koen",
                LastName: "Van Exem"
            },
            {
                id: "2",
                version: "1002",
                type: "Person",
                FirstName: "Patrick",
                LastName: "De Boeck"
            },
            {
                id: "3",
                version: "1003",
                type: "Person",
                FirstName: "Martien",
                LastName: "van Knippenberg"
            },
            {
                id: "101",
                version: "1101",
                type: "Organisation",
                Name: "Acme",
                Owner: "1",
                Employees: ["1","2","3"]
            },
            {
                id: "102",
                version: "1102",
                type: "Organisation",
                Name: "Ocme",
                Owner: "2",
                Employees: ["1"]
            },
            {
                id: "103",
                version: "1103",
                type: "Organisation",
                Name: "Icme",
                Owner: "3"
            }
        ]
    };
})(fixture);


