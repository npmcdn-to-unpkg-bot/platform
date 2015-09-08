/// <reference path="./RoleType.ts"/>

module Allors {
    export class Class {
        public name: string;
        public roles: { [id: string]: RoleType; } = {};

        constructor() {
        }

    }
}