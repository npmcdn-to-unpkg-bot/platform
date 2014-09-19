// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectBase.cs" company="Allors bvba">
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

namespace Allors
{
    using Allors.Domain;

    public abstract partial class ObjectBase
    {
        protected virtual void CustomOnPostBuild(IObjectBuilder builder)
        {
        }

        protected virtual void CustomApplySecurityOnPostBuild()
        {
        }

        protected virtual void CustomPrepareDerivation(IDerivation derivation)
        {
        }

        protected virtual void CustomDerive(IDerivation derivation)
        {
        }

        protected virtual void CustomApplySecurityOnDerive()
        {
        }

        protected virtual void CustomOnDelete()
        {
        }
    }
}
