// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DerivationGraph.cs" company="Allors bvba">
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
    using System.Collections.Generic;

    public class DerivationGraph
    {
        private readonly Derivation derivation;
        private readonly Dictionary<Derivable, DerivationNode> derivationNodeByDerivable = new Dictionary<Derivable, DerivationNode>();

        public DerivationGraph(Derivation derivation)
        {
            this.derivation = derivation;
        }

        public Dictionary<Derivable, DerivationNode> DerivationNodeByDerivable
        {
            get
            {
                return this.derivationNodeByDerivable;
            }
        }

        public int Count
        {
            get
            {
                return this.derivationNodeByDerivable.Count;
            }
        }

        public void Derive()
        {
            foreach (var dictionaryEntry in this.DerivationNodeByDerivable)
            {
                var derivationNode = dictionaryEntry.Value;
                derivationNode.TopologicalDerive(this.derivation);
            }
        }

        public DerivationNode Add(Derivable derivable)
        {
            DerivationNode derivationNode;
            if (!this.DerivationNodeByDerivable.TryGetValue(derivable, out derivationNode))
            {
                derivationNode = new DerivationNode(derivable);
                this.DerivationNodeByDerivable.Add(derivable, derivationNode);
            }

            return derivationNode;
        }

        public void AddDependency(Derivable dependent, Derivable dependee)
        {
            var derivationNode = this.Add(dependent);
            var dependencyNode = this.Add(dependee);
            derivationNode.AddDependency(dependencyNode);
        }
    }
}