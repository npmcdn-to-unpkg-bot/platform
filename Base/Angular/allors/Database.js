var Allors;
(function (Allors) {
    var Database = (function () {
        function Database(objectTypeByName) {
            this.databaseObjectById = {};
            this.objectTypeByName = objectTypeByName;
        }
        Database.prototype.update = function (updates) {
            for (var i = 0; i < updates.length; i++) {
                var update = updates[i];
                var objectType = this.objectTypeByName[update.objectType];
                var objects = update.objects;
                for (var j = 0; j < objects.length; j++) {
                    var object = objects[j];
                    object.database = this;
                    object.objectType = objectType;
                    this.databaseObjectById[object.id] = object;
                }
            }
        };
        Database.prototype.get = function (id) {
            return this.databaseObjectById[id];
        };
        return Database;
    })();
    Allors.Database = Database;
})(Allors || (Allors = {}));
//# sourceMappingURL=Database.js.map