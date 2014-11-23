//------------------------------------------------------------------------------------------------- 
// <copyright file="MergeTest.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the AssociationTest type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta.Static
{
    using System.Web.UI.WebControls;

    using Allors.Testing.Webforms;
    using Allors.Testing.Webforms.Extensions;

    using NUnit.Framework;

    using WebApplication;

    [WebformsTest("TextBoxPage.aspx")]
    public class TextBoxTest : WebformsTest<Default>
    {
        [PreRender(1)]
        public void FillInTextBox()
        {
            this.MyPage.Select<TextBox>("TextBox").Text = "Hello";
            this.Browser.Click(this.MyPage.Select<Button>("Button"));
        }

        [PreRender(2)]
        public void CheckLabel()
        {
            Assert.AreEqual("Hello", this.MyPage.Select<Label>("Label").Text);
        }
    }
}