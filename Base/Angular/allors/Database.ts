module Allors {
    export class Database {
        objectTypeByName: { [name: string]: ObjectType; } = {};
        private databaseObjectById: { [id: string]: DatabaseObject; } = {};

        constructor(data: Data.MetaData) {
            _.forEach(data.classes, objectTypeData => {
                var objectType = new ObjectType(objectTypeData);
                this.objectTypeByName[objectType.name] = objectType;
            });
        }

        load(data: Data.LoadData): void {
            _.forEach(data.objects, objectData => {
                var databaseObject = new DatabaseObject(this, objectData);
                this.databaseObjectById[databaseObject.id] = databaseObject;
            });
        }

        check(data: Data.IdWithVersionData) {

            var requireLoadIdsWithVersion = _.filter(data.idsWithVersion, idAndVersion => {
                var id = idAndVersion[0];
                var version = idAndVersion[1];

                var databaseObject = this.databaseObjectById[id];

                return (databaseObject === undefined) || (databaseObject === null) || (databaseObject.version !== version);
            });

            var requireLoadIds = new Data.IdData();
            requireLoadIds.ids = _.map(requireLoadIdsWithVersion, idWithVersion =>
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