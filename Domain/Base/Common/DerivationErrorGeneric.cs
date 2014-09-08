// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DerivationErrorGeneric.cs" company="Allors bvba">
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
    using Allors.R1;
    using Allors.R1.Meta;

    public class DerivationErrorGeneric : DerivationError
    {
        public DerivationErrorGeneric(DerivationLog derivationLog, DerivationRelation[] relations, string message, params object[] messageParam)
            : base(derivationLog, relations, message, messageParam)
        {
        }

        public DerivationErrorGeneric(DerivationLog derivationLog, DerivationRelation relation, string message, params object[] messageParam)
            : this(derivationLog, new[] { relation }, message, messageParam)
        {
        }

        public DerivationErrorGeneric(DerivationLog derivationLog, IObject association, RoleType roleType, string message, params object[] messageParam)
            : this(derivationLog, new DerivationRelation(association, roleType), message, messageParam)
        {
        }
    }
}