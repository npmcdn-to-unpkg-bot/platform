module Allors.Domain.Custom
{
    export function nest(collection: any, iteratees: any)
    {
        if (!iteratees.length) {
            return collection;
        }

        var first = iteratees[0];
        var rest = iteratees.slice(1);

        var group = _.groupBy(collection, first);

        return _.mapValues(group, value => nest(value, rest));
    };

}
