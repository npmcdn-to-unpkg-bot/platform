var Allors;
(function (Allors) {
    function applyMixins(derivedConstructor, baseConstructor) {
        Object.getOwnPropertyNames(baseConstructor.prototype).forEach(function (name) {
            if (name !== 'constructor') {
                var propertyDescriptor = Object.getOwnPropertyDescriptor(baseConstructor.prototype, name);
                Object.defineProperty(derivedConstructor.prototype, name, propertyDescriptor);
            }
        });
    }
    var Database = (function () {
        function Database(data) {
            var _this = this;
            this.objectTypeByName = {};
            this.databaseObjectById = {};
            _.forEach(data.classes, function (objectTypeData) {
                var objectType = new Allors.ObjectType(objectTypeData);
                _this.objectTypeByName[objectType.name] = objectType;
            });
            // Workspace Inheritance
            data.domains.map(function (domainName) {
                data.classes.map(function (cls) {
                    var className = cls.name;
                    var derivedType = Allors.Domain[className];
                    var baseNamespace = Allors.Domain[domainName];
                    if (baseNamespace) {
                        // Interfaces
                        cls.interfaces.map(function (iface) {
                            var baseInterface = baseNamespace[iface];
                            if (baseInterface) {
                                applyMixins(derivedType, baseInterface);
                            }
                        });
                        // Class
                        var baseType = baseNamespace[className];
                        if (baseType) {
                            applyMixins(derivedType, baseType);
                        }
                    }
                });
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
            var requireLoadIdsWithVersion = _.filter(data.objects, function (idAndVersion) {
                var id = idAndVersion[0];
                var version = idAndVersion[1];
                var databaseObject = _this.databaseObjectById[id];
                return (databaseObject === undefined) || (databaseObject === null) || (databaseObject.version !== version);
            });
            var requireLoadIds = new Allors.Data.LoadRequest();
            requireLoadIds.objects = _.map(requireLoadIdsWithVersion, function (idWithVersion) {
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