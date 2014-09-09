//------------------------------------------------------------------------------------------------- 
// <copyright file="MailboxAddress.cs" company="Allors bvba">
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
// <summary>Defines the MailboxAddress type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using global::System.Text;

    using Allors.Domain;

    /// <summary>
    /// A MailboxAddress is a address in a mailbox in the postoffice
    /// </summary>
    public partial class MailboxAddress
    {
        public override string ToString()
        {
            return DisplayName;
        }

        protected override void CoreDerive(IDerivation derivation)
        {
            derivation.Log.AssertExists(this, MailboxAddresses.Meta.PoBox);
            derivation.Log.AssertNonEmptyString(this, MailboxAddresses.Meta.PoBox);

            derivation.Log.AssertExists(this, MailboxAddresses.Meta.Place);

            var sb = new StringBuilder();
            sb.Append("PO Box ");
            if (this.ExistPoBox)
            {
                sb.Append(this.PoBox);
            }

            if (this.ExistPlace)
            {
                sb.AppendFormat(" {0}", this.Place.DisplayName);
            }

            this.DisplayName = sb.ToString();
        }
    }
}
