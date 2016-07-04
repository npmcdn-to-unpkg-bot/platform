var Allors;
(function (Allors) {
    var Context = (function () {
        function Context(name, database, workspace) {
            this.name = name;
            this.database = database;
            this.workspace = workspace;
            this.objects = {};
            this.collections = {};
            this.values = {};
            this.$q = this.database.$q;
            this.session = new Allors.Session(this.workspace);
        }
        Context.prototype.load = function (params) {
            var _this = this;
            return this.$q(function (resolve, reject) {
                return _this.database.pull(_this.name, params)
                    .then(function (response) {
                    try {
                        var requireLoadIds = _this.workspace.diff(response);
                        if (requireLoadIds.objects.length > 0) {
                            _this.database.sync(requireLoadIds)
                                .then(function (loadResponse) {
                                _this.workspace.sync(loadResponse);
                                _this.update(response);
                                _this.session.reset();
                                resolve();
                            })
                                .catch(function (e2) {
                                reject(e2);
                            });
                        }
                        else {
                            _this.update(response);
                            _this.session.reset();
                            resolve();
                        }
                    }
                    catch (e) {
                        reject(e);
                    }
                })
                    .catch(function (e) {
                    reject(e);
                });
            });
        };
        Context.prototype.query = function (service, params) {
            var _this = this;
            return this.$q(function (resolve, reject) {
                _this.database.pull(service, params)
                    .then(function (v) {
                    try {
                        var response_1 = v;
                        var requireLoadIds = _this.workspace.diff(response_1);
                        if (requireLoadIds.objects.length > 0) {
                            _this.database.sync(requireLoadIds)
                                .then(function (u) {
                                var loadResponse = u;
                                _this.workspace.sync(loadResponse);
                                var result = new Allors.Result(_this.session, response_1);
                                resolve(result);
                            })
                                .catch(function (e2) { return reject(e2); });
                        }
                        else {
                            var result = new Allors.Result(_this.session, response_1);
                            resolve(result);
                        }
                    }
                    catch (e) {
                        reject(e);
                    }
                })
                    .catch(function (e) { return reject(e); });
            });
        };
        Context.prototype.save = function () {
            var _this = this;
            return this.$q(function (resolve, reject) {
                try {
                    var saveRequest = _this.session.pushRequest();
                    _this.database.push(saveRequest)
                        .then(function (pushResponse) {
                        _this.session.pushResponse(pushResponse);
                        resolve(pushResponse);
                    })
                        .catch(function (e) {
                        reject(e);
                    });
                }
                catch (e) {
                    reject(e);
                }
            });
        };
        Context.prototype.invoke = function (methodOrService, args) {
            if (methodOrService instanceof Allors.Method) {
                return this.database.invoke(methodOrService);
            }
            else {
                return this.database.invoke(methodOrService, args);
            }
        };
        Context.prototype.update = function (response) {
            var _this = this;
            this.objects = {};
            this.collections = {};
            this.values = {};
            _.map(response.namedObjects, function (v, k) {
                _this.objects[k] = _this.session.get(v);
            });
            _.map(response.namedCollections, function (v, k) {
                _this.collections[k] = _.map(v, function (obj) { return _this.session.get(obj); });
            });
            _.map(response.namedValues, function (v, k) {
                _this.values[k] = v;
            });
        };
        return Context;
    }());
    Allors.Context = Context;
})(Allors || (Allors = {}));
//# sourceMappingURL=Context.js.map