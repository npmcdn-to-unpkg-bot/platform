module Tests {

    export class PersonTests extends tsUnit.TestClass {

        fullName() {
            var database = new Allors.Database(Allors.Meta.population);
            var workspace = new Allors.Workspace(database);

            var person = <Allors.Domain.Custom.Person>workspace.create("Person");

            this.areIdentical("N/A", person.FullName);

            person.LastName = "Doe";

            this.areIdentical("Doe", person.FullName);

            person.FirstName = "John";

            this.areIdentical("John Doe", person.FullName);
        }
    }
}