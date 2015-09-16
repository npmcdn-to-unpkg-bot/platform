using System.Collections.Generic;

namespace Allors.Web.Workspace
{
    public class SaveResponseRole
    {
        /// <summary>
        /// The role type;
        /// </summary>
        public string T { get; set; }

        public List<string> Messages { get; set; }
    }
}