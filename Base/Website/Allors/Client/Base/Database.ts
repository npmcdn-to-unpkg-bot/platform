﻿module Allors {
    export class Database {
        objectTypeByName: { [name: string]: ObjectType; } = {};
        private databaseObjectById: { [id: string]: DatabaseObject; } = {};

        constructor(data: Meta.Population) {
            _.forEach(data.classes, objectTypeData => {
                var objectType = new ObjectType(objectTypeData);
                this.objectTypeByName[objectType.name] = objectType;
            });
        }

        load(data: Data.LoadResponse): void {
            _.forEach(data.objects, objectData => {
                var databaseObject = new DatabaseObject(this, objectData);
                this.databaseObjectById[databaseObject.id] = databaseObject;
            });
        }

        check(data: Data.Response): Data.LoadRequest {

            var requireLoadIdsWithVersion = _.filter(data.objects, idAndVersion => {
                var id = idAndVersion[0];
                var version = idAndVersion[1];

                var databaseObject = this.databaseObjectById[id];

                return (databaseObject === undefined) || (databaseObject === null) || (databaseObject.version !== version);
            });

            var requireLoadIds = new Data.LoadRequest();
            requireLoadIds.objects = _.map(requireLoadIdsWithVersion, idWithVersion =>
            {
                return idWithVersion[0];
            });

            return requireLoadIds;
        }

        get(id: string): DatabaseObject {
            return this.databaseObjectById[id];
        }
}
}