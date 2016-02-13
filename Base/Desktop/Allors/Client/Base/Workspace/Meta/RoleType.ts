namespace Allors.Meta {
    export class RoleType {
        name: string;
        objectType: string;
        isUnit: boolean;
        isOne: boolean; 
        
        constructor(roleTypeData: Data.RoleType) {
            this.name = roleTypeData.name;
            this.objectType = roleTypeData.objectType;
            this.isUnit = roleTypeData.isUnit;
            this.isOne = roleTypeData.isOne;
        }

        get isComposite() { return !this.isUnit}

        get isMany() { return this.isComposite && !this.isOne; }
    }
}