// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlSanitizerTests.cs" company="Allors bvba">
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

namespace Domain
{
    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class HtmlSanitizerTests : DomainTest
    {
        [Test]
        public void Null()
        {
            string input = null;

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(string.Empty, output);
        }

        [Test]
        public void EmptyString()
        {
            string input = string.Empty;

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(string.Empty, output);
        }

        [Test]
        public void Whitespace()
        {
            string input = " ";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(string.Empty, output);
        }


        [Test]
        public void Quote()
        {
            var input = @" <div class=""&quot;""></div> ";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<div class=""&quot;""></div>", output);
        }

        [Test]
        public void Euro()
        {
            var input = @" <div>&euro;</div> ";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual("<div>\u20AC</div>", output);
        }

        [Test]
        public void CData()
        {
            var input = @" <div> <![CDATA[This is <CDATA> ...]]> </div> ";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<div>This is &lt;CDATA&gt; ...</div>", output);
        }

        [Test]
        public void XmlDeclaration()
        {
            var input = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<div>Hello</div>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<div>Hello</div>", output);
        }

        [Test]
        public void ProcessinInstruction()
        {
            var input = @"<?xml-stylesheet type=""text/xsl"" href=""style.xsl""?>
<div>Hello</div>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<div>Hello</div>", output);
        }

        [Test]
        public void Attribute()
        {
            var input = @"<div Class=""Class"">Hello</div>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<div class=""Class"">Hello</div>", output);
        }


        [Test]
        public void Style()
        {
            var input = @"<div class=""action"" style=""position: relative; "">hello</div>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<div class=""action"" style=""position: relative; "">hello</div>", output);
        }

        [Test]
        public void ExpressionStyle()
        {
            var input = @"<div class=""action"" style=""position: relative; width:expression(100)"">hello</div>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<div class=""action"">hello</div>", output);
        }

        [Test]
        public void UrlStyle()
        {
            var input = @"<div class=""action"" style=""behavior:url(script.htc)"">hello</div>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<div class=""action"">hello</div>", output);
        }

        [Test]
        public void Href()
        {
            var input = @"<a href=""http://www.allors.com"">Link</a>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<a href=""http://www.allors.com/"">Link</a>", output);
        }

        [Test]
        public void JavascriptHref()
        {
            var input = @"<a href=""javascript:alert('Xss')"">Link</a>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<a>Link</a>", output);
        }

        [Test]
        public void RemoveScript()
        {
            var input = @"<script>alert(""Ouch"");</script>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(string.Empty, output);
        }

        [Test]
        public void Fragment()
        {
            var input = @"<div>1</div><div>2</div>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize());

            Assert.AreEqual(@"<div>1</div><div>2</div>", output);
        }

        [Test]
        public void AllowTables()
        {
            var input = @"<table><tr><td>Hello</td></tr></table>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize { SkipTables = false });

            Assert.AreEqual(@"<table><tr><td>Hello</td></tr></table>", output);
        }

        [Test]
        public void SkipTables()
        {
            var input = @"<table><tr><td>Hello</td></tr></table>";

            var output = HtmlDocument.Filter(input, new HtmlSanitize { SkipTables = true });

            Assert.AreEqual(@"Hello", output);
        }
    }
}