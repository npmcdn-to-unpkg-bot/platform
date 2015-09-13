var Allors;
(function (Allors) {
    var DatabaseObject = (function () {
        function DatabaseObject(database, loadObject) {
            var _this = this;
            this.database = database;
            this.i = loadObject.i;
            this.v = loadObject.v;
            this.t = loadObject.t;
            this.roles = {};
            _.forEach(loadObject.roles, function (role) {
                _this.roles[role[0]] = role[1];
            });
        }
        Object.defineProperty(DatabaseObject.prototype, "id", {
            get: function () {
                return this.i;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(DatabaseObject.prototype, "version", {
            get: function () {
                return this.v;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(DatabaseObject.prototype, "objectType", {
            get: function () {
                return this.database.objectTypeByName[this.t];
            },
            enumerable: true,
            configurable: true
        });
        return DatabaseObject;
    })();
    Allors.DatabaseObject = DatabaseObject;
})(Allors || (Allors = {}));
//# sourceMappingURL=DatabaseObject.js.map