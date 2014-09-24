// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostalAddress.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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
    using System.Text;
    using System.Text.RegularExpressions;

    using Allors.Domain;
    

    public partial class PostalAddress
    {
        private const string Break = "<br />";

        public bool IsPostalAddress
        {
            get
            {
                return true;
            }
        }

        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsPrepareDerivation(IDerivation derivation)
        {
            base.AppsPrepareDerivation(derivation);

            foreach (PartyContactMechanism partyContactMechanism in this.PartyContactMechanismsWhereContactMechanism)
            {
                derivation.AddDependency(partyContactMechanism, this);
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, PostalAddresses.Meta.Address1);
            derivation.Log.AssertAtLeastOne(this, PostalAddresses.Meta.GeographicBoundary, PostalAddresses.Meta.PostalBoundary);
            derivation.Log.AssertExistsAtMostOne(this, PostalAddresses.Meta.GeographicBoundary, PostalAddresses.Meta.PostalBoundary);

            this.DeriveFormattedfullAddress();
            this.AppsDerivePostalCode();
            this.AppsDeriveCity();
            this.AppsDeriveCountry();

            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();
        }

        private static void AppendNextLine(StringBuilder fullAddress)
        {
            if (fullAddress.Length > 0)
            {
                fullAddress.Append(Break);
            }
        }

        private void AppsDerivePostalCode()
        {
            foreach (GeographicBoundary geographicBoundary in this.GeographicBoundaries)
            {
                if (geographicBoundary is PostalCode)
                {
                    this.PostalCode = (PostalCode)geographicBoundary;
                    break;
                }
            }
        }

        private void AppsDeriveCity()
        {
            foreach (GeographicBoundary geographicBoundary in this.GeographicBoundaries)
            {
                if (geographicBoundary is City)
                {
                    this.City = (City)geographicBoundary;
                    break;
                }
            }
        }

        private void AppsDeriveCountry()
        {
            foreach (GeographicBoundary geographicBoundary in this.GeographicBoundaries)
            {
                if (geographicBoundary is Country)
                {
                    this.Country = (Country)geographicBoundary;
                    break;
                }
            }

            if (this.ExistPostalBoundary)
            {
                this.Country = this.PostalBoundary.Country;
            }
        }

        private void DeriveFormattedfullAddress()
        {            
            var fullAddress = new StringBuilder();

            if (!string.IsNullOrEmpty(this.Address1))
            {
                fullAddress.Append(this.Address1);
            }

            if (!string.IsNullOrEmpty(this.Address2))
            {
                AppendNextLine(fullAddress);
                fullAddress.Append(this.Address2);
            }

            if (!string.IsNullOrEmpty(this.Address3))
            {
                AppendNextLine(fullAddress);
                fullAddress.Append(this.Address3);
            }

            if (this.ExistGeographicBoundaries)
            {
                AppendNextLine(fullAddress);

                foreach (GeographicBoundary geographicBoundary in this.GeographicBoundaries)
                {
                    var postalCode = geographicBoundary as PostalCode;
                    if (postalCode != null)
                    {
                        fullAddress.Append(postalCode.ComposeDisplayName());
                        fullAddress.Append(" ");
                    }
                }

                foreach (GeographicBoundary geographicBoundary in this.GeographicBoundaries)
                {
                    var city = geographicBoundary as City;
                    if (city != null)
                    {
                        fullAddress.Append(city.ComposeDisplayName());
                    }
                }

                foreach (GeographicBoundary geographicBoundary in this.GeographicBoundaries)
                {
                    var country = geographicBoundary as Country;
                    if (country != null)
                    {
                        AppendNextLine(fullAddress);
                        fullAddress.Append(country.ComposeDisplayName());
                    }
                }
            }

            if (this.ExistPostalBoundary)
            {
                AppendNextLine(fullAddress);
                if (this.PostalBoundary.ExistPostalCode)
                {
                    fullAddress.Append(this.PostalBoundary.PostalCode);
                    if (this.PostalBoundary.ExistLocality)
                    {
                        fullAddress.Append(" ");
                    }
                    else
                    {
                        AppendNextLine(fullAddress);
                    }
                }

                if (this.PostalBoundary.ExistLocality)
                {
                    fullAddress.Append(this.PostalBoundary.Locality);
                    AppendNextLine(fullAddress);
                }
            
                if (this.PostalBoundary.ExistRegion)
                {
                    fullAddress.Append(this.PostalBoundary.Region);
                    AppendNextLine(fullAddress);
                }

                if (this.PostalBoundary.ExistCountry)
                {
                    fullAddress.Append(this.PostalBoundary.Country.ComposeDisplayName());
                }
            }

            this.FormattedFullAddress = fullAddress.ToString();
        }

        private void AppsDeriveDisplayName()
        {
            this.DisplayName = this.ComposeDisplayName();
        }

        private void AppsDeriveSearchDataCharacterBoundaryText()
        {
            this.SearchData.CharacterBoundaryText = this.AppsComposeSearchDataCharacterBoundaryText();
        }

        private void AppsDeriveSearchDataWordBoundaryText()
        {
            this.SearchData.WordBoundaryText = this.AppsComposeSearchDataWordBoundaryText();
        }

        private string AppsComposeDisplayName()
        {
            this.DeriveFormattedfullAddress();
            const string Pattern = "<br />";
            const string Replacement = ", ";
            return Regex.Replace(this.FormattedFullAddress, Pattern, Replacement);
        }

        private string AppsComposeSearchDataCharacterBoundaryText()
        {
            return this.ComposeDisplayName();
        }

        private string AppsComposeSearchDataWordBoundaryText()
        {
            return null;
        }
    }
}