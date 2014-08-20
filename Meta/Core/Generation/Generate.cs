// ------------------------------------------------------------------------------------------------- 
// <copyright file="Generate.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// -------------------------------------------------------------------------------------------------
namespace Allors.R1.Development.Repository.Tasks
{
    using System.IO;

    using Allors.R1.Development.Repository.Generation;

    using Microsoft.Build.Framework;

    public class Generate : ITask
    {
        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        [Required]
        public string Template { get; set; }

        public string Output { get; set; }

        public bool Execute()
        {
            var log = new GenerateLog();

            var templateFileInfo = new FileInfo(this.Template);
            var template = new StringTemplate(templateFileInfo);
            var outputDirectoryInfo = new DirectoryInfo(this.Output);
            template.Generate(Domain.M.D, outputDirectoryInfo, log);

            return !log.ErrorOccured;
        }
    }
}