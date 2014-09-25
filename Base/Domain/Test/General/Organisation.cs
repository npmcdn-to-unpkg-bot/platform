//------------------------------------------------------------------------------------------------- 
// <copyright file="Organisation.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the Organisation type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using Allors;

    public partial class Organisation
    {
        protected override void TestOnPostBuild(IObjectBuilder builder)
        {
            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void TestDerive(IDerivation derivation)
        {
            base.TestDerive(derivation);

            derivation.Log.AssertExists(this, Organisations.Meta.Name);

            this.DisplayName = this.CustomComposeDisplayName();

            this.SearchData.CharacterBoundaryText = this.DisplayName;
            this.SearchData.RemoveWordBoundaryText();
        }

        private string CustomComposeDisplayName()
        {
            if (this.ExistName)
            {
                return this.Name;
            }

            if (this.ExistUniqueId)
            {
                return this.UniqueId.ToString();
            }

            return this.GetType() + "[" + this.Id + "]";
        }
    }
}
