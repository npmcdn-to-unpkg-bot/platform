namespace Allors.Web.Workspace
{
    public class SyncResponse
    {
        public string UserSecurityHash { get; set; }

        public SyncResponseObject[] Objects { get; set; }
    }
}