module Allors {
    export class RoleType {
        name: string;
        objectType: string;

        diff(role: any, originalRole: any) : RoleDiff {
            var roleDiff = new RoleDiff();
            roleDiff.set = role;
            return roleDiff;
        }
    }
}