﻿module Allors {
    export class ObjectType {
        roleTypeByName: { [name: string]: RoleType; } = {};
        name: string;

        constructor(data: Data.MetaObjectTypeData) {
            this.name = data.name;
            _.forEach(data.roleTypes, roleTypeData => {
                var roleType = new RoleType(roleTypeData);
                this.roleTypeByName[roleType.name] = roleType;
            });
        }
    }
}