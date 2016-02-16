namespace Allors.Domain.Custom
{
	export class Person extends Allors.Domain.Person
    {
        get DisplayName(): string {
            if (this.FirstName || this.LastName) {
                if (this.FirstName && this.LastName) {
                    return this.FirstName + " " + this.LastName;
                } else if(this.LastName) {
                    return this.LastName;
                } else {
                    return this.FirstName;
                }
            }

            if (this.UserName) {
                return this.UserName;
            }

            return "N/A";
        }
    }
}
