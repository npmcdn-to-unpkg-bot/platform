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
namespace Allors.Tools.Repository.Tasks
{
    using System.IO;
    using System.Linq;

    using Generation;

    using Microsoft.CodeAnalysis.MSBuild;

    public class Generate
    {
        public static void Execute(string solutionPath, string projectName, string template, string output)
        {
            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(solutionPath).Result;
            var project = solution.Projects.First(v => v.Name.ToLowerInvariant().Equals(projectName.ToLowerInvariant()));
            var repository = new Repository(project);
            
            var templateFileInfo = new FileInfo(template);
            var stringTemplate = new StringTemplate(templateFileInfo);
            var outputDirectoryInfo = new DirectoryInfo(output);

            stringTemplate.Generate(repository, outputDirectoryInfo);
        }
    }
}