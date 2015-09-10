var Allors;
(function (Allors) {
    var Database = (function () {
        function Database(data) {
            var _this = this;
            this.objectTypeByName = {};
            this.databaseObjectById = {};
            _.forEach(data.classes, function (objectTypeData) {
                var objectType = new Allors.ObjectType(objectTypeData);
                _this.objectTypeByName[objectType.name] = objectType;
            });
        }
        Database.prototype.load = function (data) {
            var _this = this;
            _.forEach(data.objects, function (objectData) {
                var databaseObject = new Allors.DatabaseObject(_this, objectData);
                _this.databaseObjectById[databaseObject.id] = databaseObject;
            });
        };
        Database.prototype.check = function (data) {
            var _this = this;
            var requireLoadIdsWithVersion = _.filter(data.idsWithVersion, function (idAndVersion) {
                var id = idAndVersion[0];
                var version = idAndVersion[1];
                var databaseObject = _this.databaseObjectById[id];
                return (databaseObject === undefined) || (databaseObject === null) || (databaseObject.version !== version);
            });
            var requireLoadIds = new Allors.Data.IdData();
            requireLoadIds.ids = _.map(requireLoadIdsWithVersion, function (idWithVersion) {
                return idWithVersion[0];
            });
            return requireLoadIds;
        };
        Database.prototype.get = function (id) {
            return this.databaseObjectById[id];
        };
        return Database;
    })();
    Allors.Database = Database;
})(Allors || (Allors = {}));
//# sourceMappingURL=Database.js.map