// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dependee.cs" company="Allors bvba">
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
    public partial class Dependee
    {
        public override void OnPostBuild(Allors.IObjectBuilder builder)
        {
            base.OnPostBuild(builder);

            if (!this.ExistCounter)
            {
                this.Counter = 0;
            }

            if (!this.ExistSubcounter)
            {
                this.Subcounter = 0;
            }

            if (!this.ExistDeleteDependent)
            {
                this.DeleteDependent = false;
            }
        }

        public override void PrepareDerivation(IDerivation derivation)
        {
            base.PrepareDerivation(derivation);

            if (this.ExistDependentWhereDependee)
            {
                derivation.AddDependency(this.DependentWhereDependee, this);
            }
        }

        public override void Derive(IDerivation derivation)
        {
            base.Derive(derivation);

            this.Counter = this.Counter + 1;

            if (this.ExistSubdependee)
            {
                this.Subcounter = this.Subdependee.Subcounter;
            }

            if (this.DeleteDependent.HasValue && this.DeleteDependent.Value)
            {
                this.DependentWhereDependee.Delete();
            }
        }
    }
}
