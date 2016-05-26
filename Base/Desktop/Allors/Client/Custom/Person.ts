namespace Allors.Domain
{
    export interface Person {
        hello();

        displayName;
    }

    Person.prototype.hello = function() {
        return `Hello ${this.displayName}`;
    }

    Object.defineProperty(Person.prototype, "displayName", {
        get: function() {
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
    });}
