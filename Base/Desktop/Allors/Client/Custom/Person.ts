module Allors.Domain.Custom
{
	export class Person extends Allors.Domain.Person
    {
        get FullName(): string {
            if (this.FirstName) {
                return this.LastName ? this.FirstName + " " + this.LastName : this.FirstName;
            } else {
                return this.LastName ? this.LastName : "N/A";
            }
        }
    }
}
