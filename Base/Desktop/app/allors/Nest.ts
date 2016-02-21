namespace Allors.Domain.Custom
{
    export function nest(collection: any, iteratees: any)
    {
        if (!iteratees.length) {
            return collection;
        }
        const first = iteratees[0];
        var rest = iteratees.slice(1);
        const group = _.groupBy(collection, first);
        return _.mapValues(group, value => nest(value, rest));
    };

}
