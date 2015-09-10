fixture = {};
(function (fixture) {
    fixture.loadData = {
        objects: [
            {
                id: "1",
                version: "10",
                type: "Person",
                FirstName: "Koen",
                LastName: "Van Exem"
            },
            {
                id: "2",
                version: "11",
                type: "Person",
                FirstName: "Patrick",
                LastName: "De Boeck"
            },
            {
                id: "3",
                version: "12",
                type: "Person",
                FirstName: "Martien",
                LastName: "van Knippenberg"
            },
            {
                id: "4",
                version: "20",
                type: "Organisation",
                Name: "Acme",
                Owner: "1"
            },
            {
                id: "5",
                version: "21",
                type: "Organisation",
                Name: "Ocme",
                Owner: "2"
            },
            {
                id: "6",
                version: "22",
                type: "Organisation",
                Name: "Icme",
                Owner: "3"
            }
        ]
    };
})(fixture);


