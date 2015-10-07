fixture = {};
(function (fixture) {
    fixture.loadData = {
        objects: [
            {
                i: "1",
                v: "1001",
                t: "Person",
                roles: [
                    ["FirstName","rw" , "Koen"],
                    ["LastName", "rw", "Van Exem"],
                    ["BirthDate", "rw", "1973-03-27T18:00:00Z"]
                ]
            },
            {
                i: "2",
                v: "1002",
                t: "Person",
                roles: [
                    ["FirstName", "rw", "Patrick"],
                    ["LastName", "rw", "De Boeck"]
                ]
            },
            {
                i: "3",
                v: "1003",
                t: "Person",
                roles: [
                    ["FirstName", "rw", "Martien"],
                    ["LastName", "rw", "van Knippenberg"]
                ]
            },
            {
                i: "101",
                v: "1101",
                t: "Organisation",
                roles: [
                    ["Name", "rw", "Acme"],
                    ["Owner", "rw", "1"],
                    ["Employee", "rw", ["1", "2", "3"]]
                ],
                methods: [
                    ["JustDoIt", "x"]
                ]
                },
            {
                i: "102",
                v: "1102",
                t: "Organisation",
                roles: [
                    ["Name", "rw", "Ocme"],
                    ["Owner", "rw", "2"],
                    ["Employee", "rw", ["1"]]
                ],
                methods: [
                    ["JustDoIt", ""]
                ]
            },
            {
                i: "103",
                v: "1103",
                t: "Organisation",
                roles: [
                    ["Name", "rw", "icme"],
                    ["Owner", "rw", "3"]
                ],
                methods: [
                    ["JustDoIt", ""]
                ]
            }
        ]
    };
})(fixture);


