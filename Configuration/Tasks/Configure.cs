// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Configure.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
//
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
//
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
//
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Configure.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Allors.Configure.Domain;

    using Microsoft.Build.Framework;

    public class Configure : ITask
    {
        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        [Required]
        public FileInfo Solution { get; set; }

        public IList<FileInfo> ExternalSolutions { get; set; }

        public Guid Id { get; set; }

        public bool Execute()
        {
            var solution = new Solution(this.Solution, this.ExternalSolutions, this.Id);
            try
            {
                solution.Configure();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException != null ? e.InnerException.Message : e.Message);
                return false;
            }
        }
    }
}
