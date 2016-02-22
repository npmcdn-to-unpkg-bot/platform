namespace Allors {
    export class SaveError extends DatabaseError {
        constructor(context: Context, public saveResponse: Data.SaveResponse) {
            super(context, saveResponse);
        }
    }
}