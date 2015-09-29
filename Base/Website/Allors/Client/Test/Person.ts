module Allors.Domain.Test
{
	export class Person extends Allors.Domain.Person
    {
        public field;

        get FullName(): string {
            return this.FirstName + " " + this.LastName;
        }

        public greeting() {
            this.field = "I'm a field";
            return "Hello " + this.FirstName;
        }
	}
}
