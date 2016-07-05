var Allors;
(function (Allors) {
    var Session = (function () {
        function Session(workspace) {
            this.sessionObjectById = {};
            this.newSessionObjectById = {};
            this.workspace = workspace;
        }
        Object.defineProperty(Session.prototype, "hasChanges", {
            get: function () {
                for (var newSessionObject in this.newSessionObjectById) {
                    if (this.newSessionObjectById.hasOwnProperty(newSessionObject)) {
                        return true;
                    }
                }
                var hasChanges = false;
                _.forEach(this.sessionObjectById, function (sessionObject) {
                    if (sessionObject.hasChanges) {
                        hasChanges = true;
                        return;
                    }
                });
                return hasChanges;
            },
            enumerable: true,
            configurable: true
        });
        Session.prototype.get = function (id) {
            if (id === undefined || id === null) {
                return undefined;
            }
            var sessionObject = this.sessionObjectById[id];
            if (sessionObject === undefined) {
                var workspaceObject = this.workspace.get(id);
                var type = Allors.Domain[workspaceObject.objectType.name];
                sessionObject = new type();
                sessionObject.session = this;
                sessionObject.workspaceObject = workspaceObject;
                sessionObject.objectType = workspaceObject.objectType;
                this.sessionObjectById[sessionObject.id] = sessionObject;
            }
            return sessionObject;
        };
        Session.prototype.create = function (objectTypeName) {
            var type = Allors.Domain[objectTypeName];
            var newSessionObject = new type();
            newSessionObject.session = this;
            newSessionObject.objectType = this.workspace.objectTypeByName[objectTypeName];
            newSessionObject.newId = (--Session.idCounter).toString();
            this.newSessionObjectById[newSessionObject.newId] = newSessionObject;
            return newSessionObject;
        };
        Session.prototype.reset = function () {
            _.forEach(this.newSessionObjectById, function (v) {
                v.reset();
            });
            _.forEach(this.sessionObjectById, function (v) {
                v.reset();
            });
        };
        Session.prototype.pushRequest = function () {
            var data = new Allors.Data.PushRequest();
            data.newObjects = [];
            data.objects = [];
            if (this.newSessionObjectById) {
                _.forEach(this.newSessionObjectById, function (newSessionObject) {
                    var objectData = newSessionObject.saveNew();
                    if (objectData !== undefined) {
                        data.newObjects.push(objectData);
                    }
                });
            }
            _.forEach(this.sessionObjectById, function (sessionObject) {
                var objectData = sessionObject.save();
                if (objectData !== undefined) {
                    data.objects.push(objectData);
                }
            });
            return data;
        };
        Session.prototype.pushResponse = function (pushResponse) {
            var _this = this;
            if (pushResponse.newObjects) {
                _.forEach(pushResponse.newObjects, function (saveResponseNewObject) {
                    var newId = saveResponseNewObject.ni;
                    var id = saveResponseNewObject.i;
                    var newSessionObject = _this.newSessionObjectById[newId];
                    var loadResponse = {
                        userSecurityHash: "#",
                        objects: [
                            {
                                i: id,
                                v: "",
                                t: newSessionObject.objectType.name,
                                roles: [],
                                methods: []
                            }
                        ]
                    };
                    delete (_this.newSessionObjectById[newId]);
                    delete (newSessionObject.newId);
                    _this.workspace.sync(loadResponse);
                    var workspaceObject = _this.workspace.get(id);
                    newSessionObject.workspaceObject = workspaceObject;
                    _this.sessionObjectById[id] = newSessionObject;
                });
            }
            if (Object.getOwnPropertyNames(this.newSessionObjectById).length !== 0) {
                throw new Error("Not all new objects received ids");
            }
        };
        Session.idCounter = 0;
        return Session;
    }());
    Allors.Session = Session;
})(Allors || (Allors = {}));
//# sourceMappingURL=Session.js.map