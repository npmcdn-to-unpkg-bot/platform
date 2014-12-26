// --------------------------------------------------------------------------------------------------------------------
// <copyright file="A1.cs" company="Allors bvba">
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
    using global::System.Collections.Generic;

    using Allors;

    public partial class C1
    {
        public void TestOnPostBuild(ObjectOnPostBuild method)
        {
            this.DisplayName = this.Name;
        }

        public void TestPrepareDerivation(DerivablePrepareDerivation method)
        {
            var derivation = method.Derivation;
            foreach (Derivable dependency in this.Dependencies)
            {
                derivation.AddDependency(this, dependency);
            }
        }

        public void TestDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;
            var sequence = (IList<IObject>)derivation["sequence"];
            if (sequence != null)
            {
                sequence.Add(this);
            }
        }
    }
}
