var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Allors;
(function (Allors) {
    var SaveError = (function (_super) {
        __extends(SaveError, _super);
        function SaveError(context, saveResponse) {
            _super.call(this, context, saveResponse);
            this.saveResponse = saveResponse;
        }
        return SaveError;
    }(Allors.DatabaseError));
    Allors.SaveError = SaveError;
})(Allors || (Allors = {}));
//# sourceMappingURL=SaveError.js.map