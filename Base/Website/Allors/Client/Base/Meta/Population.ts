module Allors.Meta {
    export class RoleType {
        name: string;
        isUnit: boolean;
        isOne: boolean;
    }

    export class ObjectType {
        name: string;
        roleTypes: RoleType[];
    }

    export class Population {
        domains: string[];
        classes: ObjectType[];
    }
}