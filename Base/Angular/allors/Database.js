var Allors;
(function (Allors) {
    var Database = (function () {
        function Database() {
            this.databaseObjectById = {};
        }
        Database.prototype.load = function (data) {
            var _this = this;
            _.forEach(data.objects, function (objectData) {
                var databaseObject = new Allors.DatabaseObject(_this, objectData);
                _this.databaseObjectById[databaseObject.id] = databaseObject;
            });
        };
        Database.prototype.get = function (id) {
            return this.databaseObjectById[id];
        };
        return Database;
    })();
    Allors.Database = Database;
})(Allors || (Allors = {}));
//# sourceMappingURL=Database.js.map