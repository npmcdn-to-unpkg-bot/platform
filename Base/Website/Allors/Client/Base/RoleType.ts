module Allors {
    export class RoleType {
        name: string;
        objectType: string;
        isUnit: boolean;
        isOne: boolean; 
        
        constructor(data: Meta.RoleType) {
            this.name = data.name;
            this.objectType = data.objectType;
            this.isUnit = data.isUnit;
            this.isOne = data.isOne;
        }

        get isComposite() { return !this.isUnit}

        get isMany() { return this.isComposite && !this.isOne; }
    }
}