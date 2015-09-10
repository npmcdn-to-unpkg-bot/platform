module Allors {
    export class RoleType {
        name: string;

        constructor(data: Data.MetaRoleTypeData) {
            this.name = data.name;
        }

    }
}