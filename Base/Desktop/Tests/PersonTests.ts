namespace Tests {
    import Person = Allors.Domain.Custom.Person;

    export class PersonTests extends tsUnit.TestClass {

        displayName() {
            const workspace = new Allors.Workspace(Allors.Data.metaPopulation);
            const session = new Allors.Session(workspace);

            const person = session.create("Person") as Person;

            this.areIdentical("N/A", person.DisplayName);

            person.UserName = "john@doe.com";

            this.areIdentical("john@doe.com", person.DisplayName);

            person.LastName = "Doe";

            this.areIdentical("Doe", person.DisplayName);

            person.FirstName = "John";

            this.areIdentical("John Doe", person.DisplayName);
        }
    }
}