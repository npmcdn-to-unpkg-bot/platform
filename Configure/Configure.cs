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

namespace Allors.Configure
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Allors.Configure.Xml;

    public class Configure
    {
        private readonly DirectoryInfo directoryInfo;

        private readonly DomainXml leafDomainXml;
        private readonly Dictionary<string, DomainXml> domainXmlByName; 
        

        public Configure(string path)
        {
            this.directoryInfo = new DirectoryInfo(path);
            this.leafDomainXml = DomainXml.Load(this.DirectoryInfo);
            this.domainXmlByName = new Dictionary<string, DomainXml>();

            this.LeafDomainXml.ResolveSupers(this);
        }

        public DirectoryInfo DirectoryInfo
        {
            get
            {
                return this.directoryInfo;
            }
        }

        public DomainXml LeafDomainXml
        {
            get
            {
                return this.leafDomainXml;
            }
        }

        public Dictionary<string, DomainXml> DomainXmlByName
        {
            get
            {
                return this.domainXmlByName;
            }
        }

        public void Execute()
        {
            Console.WriteLine(this.LeafDomainXml.Name);
        }
    }
}
