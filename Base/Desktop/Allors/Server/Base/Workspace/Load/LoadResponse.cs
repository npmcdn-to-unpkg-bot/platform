namespace Allors.Web.Workspace
{
    public class LoadResponse
    {
        public string UserSecurityHash { get; set; }

        public LoadResponseObject[] Objects { get; set; }
    }
}