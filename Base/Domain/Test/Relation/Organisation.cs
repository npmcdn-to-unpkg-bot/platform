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
// <summary>Defines the Person type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    public partial class Organisation
    {
        public void TestsOnPostBuild(ObjectOnPostBuild method)
        {
            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Strategy.Session).Build();
            }
        }

        public void TestsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, Organisations.Meta.Name);
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
