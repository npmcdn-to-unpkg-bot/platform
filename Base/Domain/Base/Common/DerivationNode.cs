// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DerivationNode.cs" company="Allors bvba">
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
    using System.Collections.Generic;

    public class DerivationNode : IEquatable<DerivationNode>
    {
        private readonly Object derivable;

        private bool visited;
        private DerivationNode currentRoot;
        private HashSet<DerivationNode> dependencies;

        public DerivationNode(Object derivable)
        {
            this.derivable = derivable;
        }

        public void TopologicalDerive(Derivation derivation)
        {
            this.TopologicalDerive(derivation, this);
        }

        public void AddDependency(DerivationNode derivationNode)
        {
            if (this.dependencies == null)
            {
                this.dependencies = new HashSet<DerivationNode>();
            }

            this.dependencies.Add(derivationNode);
        }

        public bool Equals(DerivationNode other)
        {
            return other != null && this.derivable.Equals(other.derivable);
        }

        public override bool Equals(object obj)
        {
            return this.Equals((DerivationNode)obj);
        }

        public override int GetHashCode()
        {
            return this.derivable.GetHashCode();
        }

        public override string ToString()
        {
            return this.derivable.ToString();
        }

        private void TopologicalDerive(Derivation derivation, DerivationNode root)
        {
            if (this.visited)
            {
                if (root.Equals(this.currentRoot))
                {
                    throw new Exception("This derivation has a cycle. (" + this.currentRoot + " -> " + this + ")");
                }

                return;
            }

            this.visited = true;
            this.currentRoot = root;

            if (this.dependencies != null)
            {
                foreach (var dependency in this.dependencies)
                {
                    dependency.TopologicalDerive(derivation, root);
                }
            }

            if (!this.derivable.Strategy.IsDeleted)
            {
                var derive = this.derivable.Derive();
                derive.Derivation = derivation;
                derive.Execute();
            }

            derivation.AddDerivedObject(this.derivable);

            this.currentRoot = null;
        }
    }
}