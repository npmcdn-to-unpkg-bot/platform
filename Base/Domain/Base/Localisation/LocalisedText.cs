// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalisedText.cs" company="Allors bvba">
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

namespace Allors.Domain
{
    using Allors.Domain;

    public partial class LocalisedText
    {
        protected override void BaseOnDelete()
        {
            base.BaseOnDelete();

            if (this.ExistSearchData)
            {
                this.SearchData.Delete();
            }
        }

        protected override void BaseOnPostBuild(IObjectBuilder objectBuilder)
        {
            base.BaseOnPostBuild(objectBuilder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        public void BaseDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            this.DisplayName = this.Text;

            this.SearchData.CharacterBoundaryText = this.Text;
            this.SearchData.RemoveWordBoundaryText();
        }
    }
}