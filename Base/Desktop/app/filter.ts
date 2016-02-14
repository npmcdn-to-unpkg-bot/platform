namespace App {
    export class Filter {
        get year(): number {
            return this.profile.filterYear;
        }
        set year(value: number) {
             this.profile.filterYear = value;
        }

        get month(): number {
            return this.profile.filterMonth;
        }
        set month(value: number) {
            this.profile.filterMonth = value;
        }
        
        enabled: boolean;

        constructor(private context: Allors.Context, private profile: Profile) {
        }
    }
}
