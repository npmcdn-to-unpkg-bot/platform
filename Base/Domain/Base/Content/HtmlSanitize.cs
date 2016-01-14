// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlSanitize.cs" company="Allors bvba">
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
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class HtmlSanitize : HtmlProfile
    {
        private static readonly HtmlRule CssAttributeFilter = (reader, writer, filters, elements) =>
            {
                var lowerValue = reader.Value.ToLower();

                // See http://stackoverflow.com/questions/3607894/cross-site-scripting-in-css-stylesheets
                if (lowerValue.Contains("expression") || lowerValue.Contains("url"))
                {
                    return;
                }

                HtmlRules.AllowAttribute(reader, writer, filters, elements);
            };

        private static readonly HtmlRule UrlAttributeFilter = (reader, writer, filters, elements) =>
        {
            Uri url;
            try
            {
                url = new Uri(reader.Value);
            }
            catch
            {
                return;
            }

            // Blacklist
            if (url.Scheme.Equals("javascript"))
            {
                return;
            }

            // Whitelist
            if (url.Scheme.Equals(Uri.UriSchemeHttp) || url.Scheme.Equals(Uri.UriSchemeHttp))
            {
                writer.WriteAttributeString(reader.LocalName, url.ToString());
            }
        };

        private static readonly HtmlRule ResolveEntityRef = (reader, writer, filters, elements) =>
            {
                var entityRef = reader.LocalName;
                var character = HtmlCharacters.GetCharacter(entityRef);
                if (!string.IsNullOrEmpty(character))
                {
                    writer.WriteString(character);
                }
            };

        public bool SkipTables { get; set; }

        public override HtmlRule LookupFilter(XmlReader reader, Stack<string> elements)
        {
            if (reader.ReadState == ReadState.Initial)
            {
                return HtmlRules.AllowInitial;
            }

            if (reader.ReadState == ReadState.Interactive)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        return this.LookupElementFilter(reader, elements);

                    case XmlNodeType.EndElement:
                        return HtmlRules.AllowEndElement;

                    case XmlNodeType.Attribute:
                        return this.LookupAttributeFilter(reader, elements);

                    case XmlNodeType.CDATA:
                    case XmlNodeType.Text:
                        return HtmlRules.AllowText;

                    case XmlNodeType.SignificantWhitespace:
                        return HtmlRules.AllowWhitespace;
                        
                    case XmlNodeType.EntityReference:
                        return ResolveEntityRef;

                    case XmlNodeType.Whitespace:
                    case XmlNodeType.Entity:
                    case XmlNodeType.EndEntity:
                    case XmlNodeType.ProcessingInstruction:
                    case XmlNodeType.Comment:
                    case XmlNodeType.Document:
                    case XmlNodeType.DocumentType:
                    case XmlNodeType.DocumentFragment:
                    case XmlNodeType.Notation:
                    case XmlNodeType.XmlDeclaration:
                        return HtmlRules.Drop;
                }
            }

            return HtmlRules.Drop;
        }

        private HtmlRule LookupElementFilter(XmlReader reader, Stack<string> elements)
        {
            var elementName = reader.LocalName.ToLowerInvariant();

            // See http://www.w3.org/html/wg/drafts/html/master/
            switch (elementName)
            {
                // Scripting
                 case "script":
                 case "noscript":
                     return HtmlRules.DropElement;

                // Root element
                // case "html":

                // Document metadata
                // case "head":
                // case "title":
                // case "base":
                // case "link":
                // case "meta":
                // case "style":

                // Sections
                // case "body":
                case "article":
                case "section":
                case "nav":
                case "aside":
                case "h1":
                case "h2":
                case "h3":
                case "h4":
                case "h5":
                case "h6":
                case "header":
                case "footer":
                case "address":

                // Grouping Content
                case "p":
                case "hr":
                case "pre":
                case "blockquote":
                case "ol":
                case "ul":
                case "li":
                case "dl":
                case "dt":
                case "dd":
                case "figure":
                case "figcaption":
                case "div":
                case "main":

                // Text-level semantics
                case "a":
                case "em":
                case "strong":
                case "small":
                case "s":
                case "cite":
                case "q":
                case "dfn":
                case "abbr":
                case "data":
                case "time":
                case "code":
                case "var":
                case "samp":
                case "kbd":
                case "sub":
                case "sup":
                case "i":
                case "b":
                case "u":
                case "mark":
                case "ruby":
                case "rt":
                case "rp":
                case "bdi":
                case "bdo":
                case "span":
                case "br":
                case "wbr":

                // Edits
                case "ins":
                case "del":

                // Embedded Content
                case "img":
                // case "iframe":
                // case "embed":
                // case "object":
                // case "param":
                case "video":
                case "audio":
                case "source":
                case "track":
                // case "canvas":
                // case "map":
                // case "area":
                case "math":
                // case "svg":
                    
                // Forms
                // case "form":
                // case "fieldset":
                // case "legend":
                // case "label":
                // case "input":
                // case "button":
                // case "select":
                // case "datalist":
                // case "optgroup":
                // case "option":
                // case "textarea":
                // case "keygen":
                // case "output":
                // case "progress":
                // case "meter":

                // Interactive Elements
                // case "details":
                // case "summary":
                // case "menuitem":
                // case "menu":
                    return HtmlRules.AllowElement;

                // Tabular data
                case "table":
                case "caption":
                case "colgroup":
                case "col":
                case "tbody":
                case "thead":
                case "tfoot":
                case "tr":
                case "td":
                case "th":
                    return this.SkipTables ? HtmlRules.SkipElement : HtmlRules.AllowElement;
            }

            return HtmlRules.SkipElement;
        }

        private HtmlRule LookupAttributeFilter(XmlReader reader, Stack<string> elements)
        {
            string parentElement = elements.Peek();

            switch (reader.LocalName.ToLowerInvariant())
            {
                // Global Attributes See http://www.w3.org/html/wg/drafts/html/master/
                // case "accesskey":
                case "class":
                // case "contenteditable":
                // case "contextmenu ":
                case "dir":
                // case "draggable":
                // case "dropzone":
                case "hidden":
                // case "id":
                // case "inert":
                // case "itemid":
                // case "itemprop":
                // case "itemref":
                // case "itemscope":
                // case "itemtype":
                case "lang":
                case "spellcheck":
                // case "tabindex":
                case "title":
                case "translate":
                    return HtmlRules.AllowAttribute;

                case "style":
                    return CssAttributeFilter;

                // Non Global Attributes    
                case "colspan":
                case "rowspan":
                    switch (parentElement)
                    {
                        case "td":
                        case "th":
                            return HtmlRules.AllowAttribute;
                    }

                    break;

                case "alt":
                    if (parentElement.Equals("img"))
                    {
                        return HtmlRules.AllowAttribute;
                    }

                    break;

                case "align":
                    switch (parentElement)
                    {
                        case "col":
                        case "colgroup":
                        case "tbody":
                        case "td":
                        case "tfoot":
                        case "th":
                        case "thead":
                            return HtmlRules.AllowAttribute;
                    }

                    break;

                case "cite":
                    switch (parentElement)
                    {
                        case "blockquote":
                        case "del":
                        case "ins":
                        case "q":
                            return HtmlRules.AllowAttribute;
                    }

                    break;

                case "compact":
                    switch (parentElement)
                    {
                        case "menu":
                        case "ol":
                        case "ul":
                            return HtmlRules.AllowAttribute;
                    }

                    break;

                case "border":
                    if (parentElement.Equals("table"))
                    {
                        return HtmlRules.AllowAttribute;
                    }

                    break;

                case "datetime":
                    switch (parentElement)
                    {
                        case "deletion":
                        case "insertion":
                            return HtmlRules.AllowAttribute;
                    }

                    break;

                case "headers":
                    if (parentElement.Equals("td"))
                    {
                        return HtmlRules.AllowAttribute;
                    }

                    break;

                case "height":
                    if (parentElement.Equals("img"))
                    {
                        return HtmlRules.AllowAttribute;
                    }

                    break;

                case "href":
                    if (parentElement.Equals("a"))
                    {
                        return UrlAttributeFilter;
                    }

                    break;

                case "hreflang":
                case "rel":
                case "rev":
                    if (parentElement.Equals("a"))
                    {
                        return HtmlRules.AllowAttribute;
                    }

                    break;

                case "face":
                case "color":
                    if (parentElement.Equals("font"))
                    {
                        return HtmlRules.AllowAttribute;
                    }

                    break;

                case "span":
                    switch (parentElement)
                    {
                        case "col":
                        case "colgroup":
                            return HtmlRules.AllowAttribute;
                    }

                    break;

                case "src":
                    if (parentElement.Equals("img"))
                    {
                        return UrlAttributeFilter;
                    }

                    break;

                case "target":
                    if (parentElement.Equals("a"))
                    {
                        return HtmlRules.AllowAttribute;
                    }

                    break;

                case "start":
                    if (parentElement.Equals("ol"))
                    {
                        return HtmlRules.AllowAttribute;
                    }

                    break;
            }

            return HtmlRules.Drop;
        }
    }
}
