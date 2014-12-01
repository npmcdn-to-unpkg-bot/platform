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
namespace Allors.Meta.Tasks
{
    using System.IO;

    using Allors.Meta;

    using Microsoft.Build.Framework;

    public class Generate : ITask
    {
        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        [Required]
        public string Repository { get; set; }
        
        public string Template { get; set; }

        public int Phase { get; set; }

        public Generate()
        {
            this.Repository = null;
            this.Template = null;
            this.Phase = 1;
        }

        public bool Execute()
        {
            var directoryInfo = new DirectoryInfo(this.Repository);
            var repository = new XmlRepository(directoryInfo);

            var log = new GenerateLog();

            repository.Generate(this.Template, log);

            return !log.ErrorOccured;
        }
    }
}