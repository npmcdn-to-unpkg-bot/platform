var Allors;
(function (Allors) {
    var ObjectType = (function () {
        function ObjectType(data) {
            var _this = this;
            this.roleTypeByName = {};
            this.name = data.name;
            _.forEach(data.roleTypes, function (roleTypeData) {
                var roleType = new Allors.RoleType(roleTypeData);
                _this.roleTypeByName[roleType.name] = roleType;
            });
        }
        return ObjectType;
    })();
    Allors.ObjectType = ObjectType;
})(Allors || (Allors = {}));
//# sourceMappingURL=ObjectType.js.map