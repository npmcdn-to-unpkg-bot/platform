using System.Collections.Generic;

namespace Allors.Web.Workspace
{
    public class SaveRequestNewObject
    {
        /// <summary>
        /// The new id.
        /// </summary>
        public string NI { get; set; }

        /// <summary>
        /// The object type.
        /// </summary>
        public string T { get; set; }

        public IList<SaveRequestRole> Roles { get; set; }
    }
}