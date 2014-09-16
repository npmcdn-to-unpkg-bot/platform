// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainXml.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Configure.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("domain", Namespace = "")]
    public class DomainXml
    {
        public const string FileName = "domain.xml";

        [XmlAttribute("version")]
        public string Version;

        [XmlAttribute("id")]
        public string IdString;

        public Guid Id
        {
            get
            {
                return new Guid(this.IdString);
            }
        }

        [XmlElement("super", typeof(SuperXml))]
        public List<SuperXml> SuperXmls = new List<SuperXml>();

        private string lowerCaseName;

        protected DomainXml()
        {
        }

        public string LowerCaseName
        {
            get
            {
                return this.lowerCaseName;
            }
        }

        internal static DomainXml Load(FileInfo fileInfo)
        {
            var xmlSerializer = new XmlSerializer(typeof(DomainXml));
            DomainXml domainXml;
            using (TextReader textReader = new StreamReader(fileInfo.FullName))
            {
                domainXml = (DomainXml)xmlSerializer.Deserialize(textReader);
                domainXml.lowerCaseName = fileInfo.Directory.Name.ToLowerInvariant();
                textReader.Close();
            }

            return domainXml;
        }

        public override string ToString()
        {
            return this.LowerCaseName;
        }
    }
}