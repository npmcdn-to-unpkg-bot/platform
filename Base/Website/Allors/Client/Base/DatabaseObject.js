var Allors;
(function (Allors) {
    var DatabaseObject = (function () {
        function DatabaseObject(database, loadObject) {
            _.assign(this, loadObject);
            this.database = database;
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
