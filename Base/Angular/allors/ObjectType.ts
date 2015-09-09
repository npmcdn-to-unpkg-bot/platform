module Allors {
    export class ObjectType {
        public name: string;
        public roles: { [id: string]: RoleType; } = {};

        constructor() {
        }
    }
}