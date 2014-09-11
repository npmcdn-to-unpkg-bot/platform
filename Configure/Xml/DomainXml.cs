// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryXml.cs" company="Allors bvba">
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
    using System.Linq;
    using System.Xml.Serialization;


    [Serializable]
    [XmlRoot("domain", Namespace = "")]
    public class DomainXml
    {
        public const string FileName = "domain.xml";

        [XmlAttribute("version")]
        public string Version;

        [XmlAttribute("id")]
        public string Id;

        [XmlElement("name")]
        public string Name;

        [XmlElement("super", typeof(SuperXml))]
        public List<SuperXml> Supers = new List<SuperXml>();

        protected DomainXml()
        {
        }

        internal static DomainXml Load(DirectoryInfo directoryInfo)
        {
            var fileInfo = directoryInfo.GetFiles(FileName).FirstOrDefault();

            if (fileInfo == null)
            {
                throw new Exception(FileName + " not found in " + directoryInfo.FullName);    
            }

            var xmlSerializer = new XmlSerializer(typeof(DomainXml));
            DomainXml domainXml;
            using (TextReader textReader = new StreamReader(fileInfo.FullName))
            {
                domainXml = (DomainXml)xmlSerializer.Deserialize(textReader);
                textReader.Close();
            }

            if (string.IsNullOrEmpty(domainXml.Name))
            {
                domainXml.Name = directoryInfo.Name;
            }

            return domainXml;
        }
    }
}