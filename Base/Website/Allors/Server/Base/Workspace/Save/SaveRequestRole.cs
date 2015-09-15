﻿namespace Allors.Web.Workspace
{
    public class SaveRequestRole
    {
        /// <summary>
        /// The role type.
        /// </summary>
        public string T { get; set; }

        /// <summary>
        /// The set role.
        /// </summary>
        public object S { get; set; }

        /// <summary>
        /// The add roles.
        /// </summary>
        public string[] A { get; set; }

        /// <summary>
        /// The remove roles.
        /// </summary>
        public string[] R { get; set; }
    }
}