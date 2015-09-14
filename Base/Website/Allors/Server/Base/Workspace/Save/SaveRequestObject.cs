using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Http.Validation.Providers;
using Newtonsoft.Json;

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