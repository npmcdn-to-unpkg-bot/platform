var Allors;
(function (Allors) {
    var RoleType = (function () {
        function RoleType() {
        }
        RoleType.prototype.diff = function (role, originalRole) {
            var roleDiff = new Allors.RoleDiff();
            roleDiff.set = role;
            return roleDiff;
        };
        return RoleType;
    })();
    Allors.RoleType = RoleType;
})(Allors || (Allors = {}));
//# sourceMappingURL=RoleType.js.map