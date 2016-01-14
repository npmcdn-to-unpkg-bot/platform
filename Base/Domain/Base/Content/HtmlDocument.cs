// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlDocument.cs" company="Allors bvba">
// Copyright 2002-2016 Allors bvba.
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public static class HtmlDocument
    {
        public static readonly XmlWriterSettings DefaultXmlWriterSettings = new XmlWriterSettings { ConformanceLevel = ConformanceLevel.Fragment };

        public static string Filter(string html, HtmlProfile profile)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return string.Empty;
            }

            using (var htmlReader = new HtmlReader(html))
            {
                using (var stringWriter = new StringWriter())
                {
                    using (var xmlWriter = XmlWriter.Create(stringWriter, DefaultXmlWriterSettings))
                    {
                        Filter(htmlReader, xmlWriter, profile);
                    }

                    return stringWriter.ToString();
                }
            }
        }

        public static void Filter(XmlReader reader, XmlWriter writer, HtmlProfile profile)
        {
            var elements = new Stack<string>();
            var filter = profile.LookupFilter(reader, elements);
            filter(reader, writer, profile, elements);
        }
    }
}