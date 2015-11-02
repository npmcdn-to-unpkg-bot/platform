module Allors.Meta {
    export class RoleType {
        name: string;
        objectType: string;
        isUnit: boolean;
        isOne: boolean;
    }

    export class MethodType {
        name: string;
    }

    export class ObjectType {
        name: string;
        interfaces: string[];
        roleTypes: RoleType[];
        methodTypes: MethodType[];
    }

    export class Population {
        domains: string[];
        classes: ObjectType[];
    }
}