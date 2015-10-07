var Allors;
(function (Allors) {
    var RoleType = (function () {
        function RoleType(data) {
            this.name = data.name;
            this.objectType = data.objectType;
            this.isUnit = data.isUnit;
            this.isOne = data.isOne;
        }
        Object.defineProperty(RoleType.prototype, "isComposite", {
            get: function () { return !this.isUnit; },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(RoleType.prototype, "isMany", {
            get: function () { return this.isComposite && !this.isOne; },
            enumerable: true,
            configurable: true
        });
        return RoleType;
    })();
    Allors.RoleType = RoleType;
})(Allors || (Allors = {}));
//# sourceMappingURL=RoleType.js.map