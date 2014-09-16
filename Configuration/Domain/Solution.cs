// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Solution.cs" company="Allors bvba">
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

namespace Allors.Configure.Domain
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    using Allors.Configure.Xml;

    public class Solution
    {
        private readonly FileInfo fileInfo;
        
        private readonly Dictionary<string, Project> projectByLowerCaseName;
        
        private readonly List<Solution> externals;

        public Solution(FileInfo fileInfo, IList<FileInfo> externalSolutions, Guid? id)
            : this(fileInfo)
        {
            if (externalSolutions != null)
            {
                foreach (var externalSolution in externalSolutions)
                {
                    this.externals.Add(new Solution(externalSolution));
                }
            }


        }

        private Solution(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
            this.projectByLowerCaseName = new Dictionary<string, Project>();
            this.externals = new List<Solution>();

            var projectRegex = new Regex(@"^Project\(""\{[\w-]*\}""\)\s*=\s*""([^""]*)""\s*,\s*""([^""]*)""");
            foreach (var line in File.ReadAllLines(fileInfo.FullName))
            {
                var match = projectRegex.Match(line);
                if (match.Success)
                {
                    var projectName = match.Groups[1].Value;
                    var projectPath = match.Groups[2].Value;

                    if (projectPath.ToLowerInvariant().EndsWith(".csproj") && !Path.IsPathRooted(projectPath))
                    {
                        var projectFileInfo = new FileInfo(Path.Combine(this.fileInfo.DirectoryName, projectPath));
                        var project = new Project(this, projectName, projectFileInfo);
                        this.ProjectByLowerCaseName.Add(projectName.ToLowerInvariant(), project);
                    }
                }
            }
        }

        public FileInfo FileInfo
        {
            get
            {
                return this.fileInfo;
            }
        }

        public Dictionary<string, Project> ProjectByLowerCaseName
        {
            get
            {
                return this.projectByLowerCaseName;
            }
        }

        public override string ToString()
        {
            return this.fileInfo.Name;
        }

        public void Configure()
        {
        }
    }
}
