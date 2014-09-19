// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectBase.v.cs" company="Allors bvba">
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
        public virtual void OnPostBuild(IObjectBuilder builder)
        {
            this.CoreOnPostBuild(builder);
            this.CustomOnPostBuild(builder);
            
            this.ApplySecurityOnPostBuild();
        }

        public virtual void ApplySecurityOnPostBuild()
        {
            this.CoreApplySecurityOnPostBuild();
            this.CustomApplySecurityOnPostBuild();
        }

        public virtual void PrepareDerivation(IDerivation derivation)
        {
            this.CorePrepareDerivation(derivation);
            this.CustomPrepareDerivation(derivation);
        }

        public virtual void Derive(IDerivation derivation)
        {
            this.CoreDerive(derivation);
            this.CustomDerive(derivation);

            this.ApplySecurityOnDerive();
        }

        public virtual void ApplySecurityOnDerive()
        {
            this.CoreApplySecurityOnDerive();
            this.CustomApplySecurityOnDerive();
        }

        public virtual void Delete()
        {
            this.CoreOnDelete();
            this.CustomOnDelete();

            this.AllorsDelete();
        }
    }
}
