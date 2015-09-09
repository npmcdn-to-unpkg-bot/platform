module Allors {

    export class ObjectType {
        public name: string;
        public roleTypeByName: { [id: string]: RoleType; } = {};
    }
}