namespace Allors.Domain
{
    export interface Person {
        hello();

        displayName;
    }

    extend(Person, {
        hello() {
            return `Hello ${this.displayName}`;
        }
        ,
        get displayName() {
                if (this.FirstName || this.LastName) {
                    if (this.FirstName && this.LastName) {
                        return this.FirstName + " " + this.LastName;
                    } else if (this.LastName) {
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
    });
}
