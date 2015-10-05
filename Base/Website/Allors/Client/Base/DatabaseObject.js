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
            this.methods = {};
            _.forEach(loadObject.roles, function (role) {
                var name = role[0];
                var access = role[1];
                var canRead = access.indexOf('r') !== -1;
                var canWrite = access.indexOf('w') !== -1;
                _this.roles["CanRead" + name] = canRead;
                _this.roles["CanWrite" + name] = canWrite;
                if (canRead) {
                    var value = role[2];
                    _this.roles[name] = value;
                }
            });
            _.forEach(loadObject.methods, function (method) {
                var name = method[0];
                var access = method[1];
                var canExecute = access.indexOf('x') !== -1;
                _this.methods["CanExecute" + name] = canExecute;
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