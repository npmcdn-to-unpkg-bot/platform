var Allors;
(function (Allors) {
    var Bootstrap;
    (function (Bootstrap) {
        var Field = (function () {
            function Field($log, $translate) {
                this.$log = $log;
                this.$translate = $translate;
            }
            Object.defineProperty(Field.prototype, "form", {
                get: function () {
                    return this.f;
                },
                set: function (value) {
                    this.f = value;
                    this.onBind();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Field.prototype, "object", {
                get: function () {
                    return this.o;
                },
                set: function (value) {
                    this.o = value;
                    this.onBind();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Field.prototype, "relation", {
                get: function () {
                    return this.r;
                },
                set: function (value) {
                    this.r = value;
                    this.onBind();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Field.prototype, "display", {
                get: function () {
                    return this.d;
                },
                set: function (value) {
                    this.d = value;
                    this.onBind();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Field.prototype, "lookup", {
                get: function () {
                    return this.l;
                },
                set: function (value) {
                    this.l = value;
                    this.onBind();
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Field.prototype, "objectType", {
                get: function () {
                    return this.object && this.object.objectType;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Field.prototype, "roleType", {
                get: function () {
                    return this.object && this.object.objectType.roleTypeByName[this.relation];
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Field.prototype, "canRead", {
                get: function () {
                    var canRead = false;
                    if (this.object) {
                        canRead = this.object.canRead(this.relation);
                    }
                    return canRead;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Field.prototype, "canWrite", {
                get: function () {
                    var canWrite = false;
                    if (this.object) {
                        canWrite = this.object.canWrite(this.relation);
                    }
                    return canWrite;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Field.prototype, "role", {
                get: function () {
                    return this.object && this.object[this.relation];
                },
                set: function (value) {
                    this.object[this.relation] = value;
                },
                enumerable: true,
                configurable: true
            });
            Object.defineProperty(Field.prototype, "displayValue", {
                get: function () {
                    return this.role && this.role[this.display];
                },
                enumerable: true,
                configurable: true
            });
            Field.prototype.$onInit = function () {
                this.derive();
            };
            Field.prototype.onBind = function () {
                this.derive();
            };
            ;
            Field.prototype.derive = function () {
                var _this = this;
                if (this.roleType && this.$translate) {
                    if (this.label === undefined) {
                        this.label = null;
                        var key1 = "meta_" + this.objectType.name + "_" + this.roleType.name + "_Label";
                        var key2 = "meta_" + this.roleType.objectType + "_" + this.roleType.name + "_Label";
                        this.translate(key1, key2, function (value) { return _this.label = value; });
                        if (this.label === undefined || this.label === null) {
                            this.label = Allors.Filters.Humanize.filter(this.relation);
                            var suffix = "Enum";
                            if (this.label.indexOf(suffix, this.label.length - suffix.length) !== -1) {
                                this.label = this.label.substring(0, this.label.length - suffix.length);
                            }
                        }
                    }
                    if (this.placeholder === undefined) {
                        this.placeholder = null;
                        var key1 = "meta_" + this.objectType.name + "_" + this.roleType.name + "_Placeholder";
                        var key2 = "meta_" + this.roleType.objectType + "_" + this.roleType.name + "_Placeholder";
                        this.translate(key1, key2, function (value) { return _this.placeholder = value; });
                    }
                    if (this.help === undefined) {
                        this.help = null;
                        var key1 = "meta_" + this.objectType.name + "_" + this.roleType.name + "_Help";
                        var key2 = "meta_" + this.roleType.objectType + "_" + this.roleType.name + "_Help";
                        this.translate(key1, key2, function (value) { return _this.help = value; });
                    }
                }
            };
            Field.prototype.translate = function (key1, key2, set, setDefault) {
                var _this = this;
                this.$translate(key1)
                    .then(function (translation) {
                    if (key1 !== translation) {
                        set(translation);
                    }
                    else {
                        _this.$translate(key2)
                            .then(function (translation) {
                            if (key2 !== translation) {
                                set(translation);
                            }
                            else {
                                if (setDefault) {
                                    setDefault();
                                }
                            }
                        });
                    }
                });
            };
            return Field;
        }());
        Bootstrap.Field = Field;
    })(Bootstrap = Allors.Bootstrap || (Allors.Bootstrap = {}));
})(Allors || (Allors = {}));
//# sourceMappingURL=Field.js.map