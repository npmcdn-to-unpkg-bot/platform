module Allors {
    export class RoleType {
        name: string;
        isUnit: boolean;
        isOne: boolean; 
        
        constructor(data: Meta.RoleType) {
            this.name = data.name;
            this.isUnit = data.isUnit;
            this.isOne = data.isOne;
        }

        get isComposite() { return !this.isUnit}

        get isMany() { return this.isComposite && !this.isOne; }
    }
}