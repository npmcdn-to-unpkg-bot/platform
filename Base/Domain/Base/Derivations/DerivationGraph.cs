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

    public class DerivationGraph
    {
        private readonly DerivationBase derivation;
        private readonly Dictionary<Object, DerivationNode> derivationNodeByDerivable = new Dictionary<Object, DerivationNode>();

        public DerivationGraph(DerivationBase derivation)
        {
            this.derivation = derivation;
        }

        public Dictionary<Object, DerivationNode> DerivationNodeByDerivable => this.derivationNodeByDerivable;

        public int Count => this.derivationNodeByDerivable.Count;

        public void Derive()
        {
            foreach (var dictionaryEntry in this.DerivationNodeByDerivable)
            {
                var derivationNode = dictionaryEntry.Value;
                derivationNode.TopologicalDerive(this.derivation);
            }
        }

        public DerivationNode Add(Object derivable)
        {
            DerivationNode derivationNode;
            if (!this.DerivationNodeByDerivable.TryGetValue(derivable, out derivationNode))
            {
                derivationNode = new DerivationNode(derivable);
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
    }
}