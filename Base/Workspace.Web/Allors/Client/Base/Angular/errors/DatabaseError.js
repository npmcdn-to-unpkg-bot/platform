var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Allors;
(function (Allors) {
    var DatabaseError = (function (_super) {
        __extends(DatabaseError, _super);
        function DatabaseError(context, reponseError) {
            _super.call(this, context);
            this.reponseError = reponseError;
        }
        return DatabaseError;
    }(Allors.AllorsError));
    Allors.DatabaseError = DatabaseError;
})(Allors || (Allors = {}));
//# sourceMappingURL=DatabaseError.js.map