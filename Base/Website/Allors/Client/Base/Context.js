var Allors;
(function (Allors) {
    var Context = (function () {
        function Context(workspace, objectByName) {
            this.workspace = workspace;
            this.objectByName = objectByName;
        }
        return Context;
    })();
    Allors.Context = Context;
})(Allors || (Allors = {}));
