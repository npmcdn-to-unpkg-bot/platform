// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlRules.cs" company="Allors bvba">
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
    using System.Xml;

    public delegate void HtmlRule(XmlReader reader, XmlWriter writer, HtmlProfile profile, Stack<string> elements);

    public static class HtmlRules
    {
        public static readonly HtmlRule Drop = delegate { };

        public static readonly HtmlRule AllowInitial = (reader, writer, filters, elements) =>
            {
                while (reader.Read())
                {
                    var filter = filters.LookupFilter(reader, elements);
                    filter(reader, writer, filters, elements);
                }
            };

        public static readonly HtmlRule AllowEndElement = (reader, writer, filters, elements) => writer.WriteFullEndElement();

        public static readonly HtmlRule AllowAttribute = (reader, writer, filters, elements) => writer.WriteAttributeString(reader.LocalName.ToLowerInvariant(), reader.Value);

        public static readonly HtmlRule AllowText = (reader, writer, filters, elements) => writer.WriteString(reader.Value);

        public static readonly HtmlRule AllowWhitespace = (reader, writer, filters, elements) => writer.WriteWhitespace(reader.Value);

        public static readonly HtmlRule AllowCData = (reader, writer, filters, elements) => writer.WriteCData(reader.Value);

        public static readonly HtmlRule AllowEntityRef = (reader, writer, filters, elements) => writer.WriteEntityRef(reader.LocalName);
        
        public static readonly HtmlRule AllowElement = (reader, writer, filters, elements) =>
            {
                elements.Push(reader.LocalName.ToLowerInvariant());
                try
                {
                    writer.WriteStartElement(reader.LocalName.ToLowerInvariant());

                    if (reader.IsEmptyElement)
                    {
                        return;
                    }

                    if (reader.AttributeCount > 0)
                    {
                        for (var i = 0; i < reader.AttributeCount; i++)
                        {
                            reader.MoveToAttribute(i);

                            var filter = filters.LookupFilter(reader, elements);
                            filter(reader, writer, filters, elements);
                        }

                        reader.MoveToElement();
                    }

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            var filter = filters.LookupFilter(reader, elements);
                            filter(reader, writer, filters, elements);
                            break;
                        }
                        else
                        {
                            var filter = filters.LookupFilter(reader, elements);
                            filter(reader, writer, filters, elements);
                        }
                    }
                }
                finally
                {
                    elements.Pop();
                }
            };

        public static readonly HtmlRule SkipElement = (reader, writer, filters, elements) =>
        {
            elements.Push(reader.LocalName.ToLowerInvariant());
            try
            {
                if (reader.IsEmptyElement)
                {
                    return;
                }

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }
                    
                    var filter = filters.LookupFilter(reader, elements);
                    filter(reader, writer, filters, elements);
                }
            }
            finally
            {
                elements.Pop();
            }
        };

        public static readonly HtmlRule DropElement = (reader, writer, filters, elements) =>
        {
            elements.Push(reader.LocalName.ToLowerInvariant());
            try
            {
                if (reader.IsEmptyElement)
                {
                    return;
                }

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        DropElement(reader, writer, filters, elements);
                    }

                    Drop(reader, writer, filters, elements);
                }
            }
            finally
            {
                elements.Pop();
            }
        };
    }
}