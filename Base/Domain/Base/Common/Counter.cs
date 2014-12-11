// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Counters.cs" company="Allors bvba">
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
    using System;

    using Allors;

    public partial class Counter
    {
        protected override void BaseOnPostBuild(IObjectBuilder builder)
        {
            base.BaseOnPostBuild(builder);

            if (!this.ExistUniqueId)
            {
                this.UniqueId = Guid.NewGuid();
            }

            if (!this.ExistValue)
            {
                this.Value = 0;
            }
        }

        protected override void BaseDerive(IDerivation derivation)
        {
            base.BaseDerive(derivation);

            derivation.Log.AssertExists(this, Meta.UniqueId);
            derivation.Log.AssertExists(this, Meta.Value);
        }
    }
}