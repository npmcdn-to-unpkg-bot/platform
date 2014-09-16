// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Project.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.IO;

    using Allors.Configure.Xml;

    public class Project
    {
        private readonly Solution solution;
        private readonly string lowerCaseName;
        private readonly FileInfo fileInfo;

        private readonly Dictionary<string, DomainXml> domainXmlByLowerCaseName; 

        public Project(Solution solution, string name, FileInfo fileInfo)
        {
            this.solution = solution;
            this.lowerCaseName = name.ToLowerInvariant();
            this.fileInfo = fileInfo;

            this.domainXmlByLowerCaseName = new Dictionary<string, DomainXml>();
            foreach (var domainXmlFileInfo in this.fileInfo.Directory.EnumerateFiles("domain.xml", SearchOption.AllDirectories))
            {
                var domainXml = DomainXml.Load(domainXmlFileInfo);
                this.DomainXmlByLowerCaseName.Add(domainXml.LowerCaseName, domainXml);
            }
        }

        public Solution Solution
        {
            get
            {
                return this.solution;
            }
        }

        public string LowerCaseName
        {
            get
            {
                return this.lowerCaseName;
            }
        }

        public FileInfo FileInfo
        {
            get
            {
                return this.fileInfo;
            }
        }

        public Dictionary<string, DomainXml> DomainXmlByLowerCaseName
        {
            get
            {
                return this.domainXmlByLowerCaseName;
            }
        }

        public override string ToString()
        {
            return this.lowerCaseName;
        }
    }
}
