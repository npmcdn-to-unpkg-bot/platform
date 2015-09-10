var Allors;
(function (Allors) {
    var DatabaseObject = (function () {
        function DatabaseObject(database, loadObject) {
            _.assign(this, loadObject);
            this.database = database;
        }
        return DatabaseObject;
    })();
    Allors.DatabaseObject = DatabaseObject;
})(Allors || (Allors = {}));
//# sourceMappingURL=DatabaseObject.js.map