﻿namespace Allors {
    export class Result {

        objects: { [name: string]: ISessionObject; } = {};
        collections: { [name: string]: ISessionObject[]; } = {};
        values: { [name: string]: any; } = {};

        constructor(session: ISession, response: Data.Response) {
            _.map(response.namedObjects, (v, k) => {
                this.objects[k] = session.get(v);
            });

            _.map(response.namedCollections, (v, k) => {
                this.collections[k] = _.map(v, (obj) => { return session.get(obj) });
            });

            _.map(response.namedValues, (v, k) => {
                this.values[k] = v;
            });
        }
    }
}