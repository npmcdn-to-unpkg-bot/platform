var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Allors;
(function (Allors) {
    var InvokeError = (function (_super) {
        __extends(InvokeError, _super);
        function InvokeError(context, invokeResponse) {
            _super.call(this, context, invokeResponse);
            this.invokeResponse = invokeResponse;
        }
        return InvokeError;
    }(Allors.DatabaseError));
    Allors.InvokeError = InvokeError;
})(Allors || (Allors = {}));
//# sourceMappingURL=InvokeError.js.map