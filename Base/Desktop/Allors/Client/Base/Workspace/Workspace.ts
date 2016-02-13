namespace Allors {

    function applyMixins(derivedConstructor: any, baseConstructor: any) {
        Object.getOwnPropertyNames(baseConstructor.prototype).forEach(name => {
            if (name !== 'constructor') {
                const propertyDescriptor = Object.getOwnPropertyDescriptor(baseConstructor.prototype, name);
                Object.defineProperty(derivedConstructor.prototype, name, propertyDescriptor);
            }
        });
    }

    export interface IWorkspace {
        objectTypeByName: { [name: string]: ObjectType; };

        load(data: Data.LoadResponse): void;
        check(data: Data.Response): Data.LoadRequest;
        get(id: string): WorkspaceObject;
    }

    export class Workspace implements IWorkspace {
        objectTypeByName: { [name: string]: ObjectType; } = {};
        userSecurityHash: string;

        private workspaceObjectById: { [id: string]: WorkspaceObject; } = {};

        constructor(data: Meta.Population) {
            _.forEach(data.classes, objectTypeData => {
                var objectType = new ObjectType(objectTypeData);
                this.objectTypeByName[objectType.name] = objectType;
            });

            // Workspace Inheritance
            data.domains.map(domainName => {
                data.classes.map(cls => {
                    var className = cls.name;
                    var derivedType = Allors.Domain[className];
                    var baseNamespace = Allors.Domain[domainName];

                    if (baseNamespace) {
                        // Interfaces
                        cls.interfaces.map(iface => {
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

        load(data: Data.LoadResponse): void {
            _.forEach(data.objects, objectData => {
                var workspaceObject = new WorkspaceObject(this, data, objectData);
                this.workspaceObjectById[workspaceObject.id] = workspaceObject;
            });
        }

        check(data: Data.Response): Data.LoadRequest {
            var userSecurityHash = data.userSecurityHash;

            var requireLoadIdsWithVersion = _.filter(data.objects, idAndVersion => {
                var id = idAndVersion[0];
                var version = idAndVersion[1];

                var workspaceObject = this.workspaceObjectById[id];

                return (workspaceObject === undefined) || (workspaceObject === null) || (workspaceObject.version !== version) || (workspaceObject.userSecurityHash !== userSecurityHash);
            });

            var requireLoadIds = new Data.LoadRequest();
            requireLoadIds.objects = _.map(requireLoadIdsWithVersion, idWithVersion =>
            {
                return idWithVersion[0];
            });

            return requireLoadIds;
        }

        get(id: string): WorkspaceObject {
            var workspaceObject = this.workspaceObjectById[id];
            if (workspaceObject === undefined) {
                throw "Object with id " + id + " is not present.";
            }

            return workspaceObject;
        }
    }
}