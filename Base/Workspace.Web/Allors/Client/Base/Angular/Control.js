var Allors;
(function (Allors) {
    var Control = (function () {
        function Control(name, database, workspace, $rootScope, $scope, $q, $log) {
            var _this = this;
            this.$scope = $scope;
            this.$q = $q;
            this.$log = $log;
            this.context = new Allors.Context(name, database, workspace);
            this.events = new Allors.Events(this.context, $rootScope, $scope);
            this.session = this.context.session;
            this.events.onRefresh(function () { return _this.refresh(); });
        }
        Object.defineProperty(Control.prototype, "objects", {
            // Context
            get: function () {
                return this.context.objects;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Control.prototype, "collections", {
            get: function () {
                return this.context.collections;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Control.prototype, "values", {
            get: function () {
                return this.context.values;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Control.prototype, "hasChanges", {
            get: function () {
                return this.context.session.hasChanges;
            },
            enumerable: true,
            configurable: true
        });
        // Commands
        Control.prototype.load = function (params) {
            return this.context.load(params)
                .catch(function (e) { throw e; });
        };
        Control.prototype.save = function () {
            var _this = this;
            return this.$q(function (resolve, reject) {
                _this.context
                    .save()
                    .then(function (saveResponse) {
                    if (saveResponse.hasErrors) {
                        reject(new Allors.SaveError(_this.context, saveResponse));
                    }
                    else {
                        resolve(saveResponse);
                    }
                }, function (e) {
                    throw e;
                })
                    .finally(function () { return _this.events.broadcastRefresh(); });
            });
        };
        Control.prototype.invoke = function (methodOrService, args) {
            var _this = this;
            return this.$q(function (resolve, reject) {
                if (methodOrService instanceof Allors.Method) {
                    return _this.context
                        .invoke(methodOrService)
                        .then(function (invokeResponse) {
                        if (invokeResponse.hasErrors) {
                            reject(new Allors.InvokeError(_this.context, invokeResponse));
                        }
                        else {
                            resolve(invokeResponse);
                        }
                    }, function (e) {
                        throw e;
                    })
                        .finally(function () { return _this.events.broadcastRefresh(); });
                }
                else {
                    return _this.context
                        .invoke(methodOrService, args)
                        .then(function (invokeResponse) {
                        if (invokeResponse.hasErrors) {
                            reject(new Allors.InvokeError(_this.context, invokeResponse));
                        }
                        else {
                            resolve(invokeResponse);
                        }
                    }, function (e) {
                        throw e;
                    })
                        .finally(function () { return _this.events.broadcastRefresh(); });
                }
            });
        };
        Control.prototype.saveAndInvoke = function (methodOrService, args) {
            var _this = this;
            return this.$q(function (resolve, reject) {
                return _this.context
                    .save()
                    .then(function (saveResponse) {
                    if (saveResponse.hasErrors) {
                        reject(new Allors.SaveError(_this.context, saveResponse));
                    }
                    else {
                        _this.refresh()
                            .then(function () {
                            if (methodOrService instanceof Allors.Method) {
                                _this.context.invoke(methodOrService)
                                    .then(function (invokeResponse) {
                                    if (invokeResponse.hasErrors) {
                                        reject(new Allors.InvokeError(_this.context, invokeResponse));
                                    }
                                    else {
                                        resolve(invokeResponse);
                                    }
                                }, function (e) {
                                    throw e;
                                })
                                    .finally(function () { return _this.events.broadcastRefresh(); });
                            }
                            else {
                                _this.context.invoke(methodOrService, args)
                                    .then(function (invokeResponse) {
                                    if (invokeResponse.hasErrors) {
                                        reject(new Allors.InvokeError(_this.context, invokeResponse));
                                    }
                                    else {
                                        resolve(invokeResponse);
                                    }
                                }, function (e) {
                                    throw e;
                                })
                                    .finally(function () { return _this.events.broadcastRefresh(); });
                            }
                        }, function (e) {
                            throw e;
                        });
                    }
                }, function (e) {
                    throw e;
                });
            });
        };
        Control.prototype.query = function (query, args) {
            return this.context
                .query(query, args)
                .catch(function (e) { throw e; });
        };
        Control.prototype.queryResults = function (query, args) {
            return this.context
                .query(query, args)
                .then(function (result) {
                var results = result.collections["results"];
                return results;
            })
                .catch(function (e) { throw e; });
        };
        Control.prototype.handleError = function (e) {
            if (e.hasErrors) {
                return e;
            }
            else {
                throw e;
            }
        };
        return Control;
    }());
    Allors.Control = Control;
})(Allors || (Allors = {}));
//# sourceMappingURL=Control.js.map