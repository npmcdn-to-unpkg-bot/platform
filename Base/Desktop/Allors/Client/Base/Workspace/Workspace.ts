namespace Allors {

    function applyMixins(derivedConstructor: any, baseConstructor: any) {
        Object.getOwnPropertyNames(baseConstructor.prototype).forEach(name => {
            if (name !== "constructor") {
                const propertyDescriptor = Object.getOwnPropertyDescriptor(baseConstructor.prototype, name);
                Object.defineProperty(derivedConstructor.prototype, name, propertyDescriptor);
            }
        });
    }

    export interface IWorkspace {
        objectTypeByName: { [name: string]: Meta.ObjectType; };

        diff(data: Data.Response): Data.LoadRequest;
        load(data: Data.LoadResponse): void;
        get(id: string): WorkspaceObject;
    }

    export class Workspace implements IWorkspace {
        objectTypeByName: { [name: string]: Meta.ObjectType; } = {};
        userSecurityHash: string;

        private workspaceObjectById: { [id: string]: WorkspaceObject; } = {};

        constructor(metaPopulationData: Data.MetaPopulation) {
            _.forEach(metaPopulationData.classes, classData => {
                var objectType = new Meta.ObjectType(classData);
                this.objectTypeByName[objectType.name] = objectType;
            });
            
            // Workspace Inheritance
            metaPopulationData.domains.map(domainName => {
                metaPopulationData.classes.map(cls => {
                    var className = cls.name;
                    var derivedType = Domain[className];
                    var baseNamespace = Domain[domainName];

                    if (baseNamespace) {
                        // Interfaces
                        cls.interfaces.map(iface => {
                            var baseInterface = baseNamespace[iface];
                            if (baseInterface) {
                                applyMixins(derivedType, baseInterface);
                            }
                        });

                        // Class
                        const baseType = baseNamespace[className];
                        if (baseType) {
                            applyMixins(derivedType, baseType);
                        }
                    }
                });
            });
        }

        diff(response: Data.Response): Data.LoadRequest {
            var userSecurityHash = response.userSecurityHash;

            const requireLoadIdsWithVersion = _.filter(response.objects, idAndVersion => {

                var [id, version] = idAndVersion;
                var workspaceObject = this.workspaceObjectById[id];

                return (workspaceObject === undefined) || (workspaceObject === null) || (workspaceObject.version !== version) || (workspaceObject.userSecurityHash !== userSecurityHash);
            });

            const requireLoadIds = new Data.LoadRequest();
            requireLoadIds.objects = _.map(requireLoadIdsWithVersion, idWithVersion => {
                return idWithVersion[0];
            });

            return requireLoadIds;
        }

        load(loadResponse: Data.LoadResponse): void {
            _.forEach(loadResponse.objects, objectData => {
                var workspaceObject = new WorkspaceObject(this, loadResponse, objectData);
                this.workspaceObjectById[workspaceObject.id] = workspaceObject;
            });
        }

        get(id: string): WorkspaceObject {
            const workspaceObject = this.workspaceObjectById[id];
            if (workspaceObject === undefined) {
                throw `Object with id ${id} is not present.`;
            }

            return workspaceObject;
        }
    }
}