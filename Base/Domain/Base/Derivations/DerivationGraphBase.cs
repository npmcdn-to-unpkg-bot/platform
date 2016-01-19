// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DerivationGraph.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
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
    using System.Collections.Generic;

    public abstract class DerivationGraphBase
    {
        private readonly DerivationBase derivation;
        private readonly Dictionary<Object, DerivationNodeBase> derivationNodeByDerivable = new Dictionary<Object, DerivationNodeBase>();

        protected DerivationGraphBase(DerivationBase derivation)
        {
            this.derivation = derivation;
        }

        public Dictionary<Object, DerivationNodeBase> DerivationNodeByDerivable => this.derivationNodeByDerivable;

        public int Count => this.derivationNodeByDerivable.Count;

        public void Derive()
        {
            foreach (var dictionaryEntry in this.DerivationNodeByDerivable)
            {
                var derivationNode = dictionaryEntry.Value;
                derivationNode.TopologicalDerive(this.derivation);
            }
        }

        public DerivationNodeBase Add(Object derivable)
        {
            DerivationNodeBase derivationNode;
            if (!this.DerivationNodeByDerivable.TryGetValue(derivable, out derivationNode))
            {
                derivationNode = this.CreateDerivationNode(derivable);
                this.DerivationNodeByDerivable.Add(derivable, derivationNode);
            }

            return derivationNode;
        }

        public void AddDependency(Object dependent, Object dependee)
        {
            var derivationNode = this.Add(dependent);
            var dependencyNode = this.Add(dependee);
            derivationNode.AddDependency(dependencyNode);
        }

        protected abstract DerivationNodeBase CreateDerivationNode(Object derivable);
    }
}