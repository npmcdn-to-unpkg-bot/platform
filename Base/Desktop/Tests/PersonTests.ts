namespace Tests {

    export class PersonTests extends tsUnit.TestClass {

        fullName() {
            var workspace = new Allors.Workspace(Allors.Meta.population);
            var session = new Allors.Session(workspace);

            var person = <Allors.Domain.Custom.Person>session.create("Person");

            this.areIdentical("N/A", person.FullName);

            person.LastName = "Doe";

            this.areIdentical("Doe", person.FullName);

            person.FirstName = "John";

            this.areIdentical("John Doe", person.FullName);
        }
    }
}