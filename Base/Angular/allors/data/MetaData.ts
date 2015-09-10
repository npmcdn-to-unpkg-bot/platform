module Allors.Data {
    export class MetaRoleTypeData {
        name: string;
    }

    export class MetaObjectTypeData {
        name: string;
        roleTypes: MetaRoleTypeData[];
    }

    export class MetaData {
        classes: MetaObjectTypeData[];
    }
}