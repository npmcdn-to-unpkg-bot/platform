using System.Collections.Generic;

namespace Allors.Web.Workspace
{
    public class SaveRequestObject
    {
        /// <summary>
        /// The id.
        /// </summary>
        public string I { get; set; }

        /// <summary>
        /// The version.
        /// </summary>
        public string V { get; set; }

        public IList<SaveRequestRole> Roles { get; set; }
    }
}