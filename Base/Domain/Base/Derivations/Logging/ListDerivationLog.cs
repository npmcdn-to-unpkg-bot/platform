// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugLog.cs" company="Allors bvba">
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

namespace Allors.Domain.Logging
{
    using System.Collections.Generic;

    using Object = Allors.Domain.Object;

    public sealed class ListDerivationLog : IDerivationLog
    {
        public List<string> List { get; }

        public ListDerivationLog()
        {
            this.List = new List<string>();
        }

        public override string ToString()
        {
            return string.Join("\n", this.List);
        }

        // Derivation
        public void StartedGeneration(int generation)
        {
            this.List.Add($"Started Generation #{generation}");
        }

        public void StartedPreparation(int preparationRun)
        {
            this.List.Add($"Started Preparation #{preparationRun}");
        }

        public void PreDeriving(Object derivable)
        {
            this.List.Add($"Preparing #{derivable}");
        }

        public void PreDerived(Object derivable)
        {
            this.List.Add($"PreDerived [{derivable.Id}] {derivable}");
        }

        public void AddedDerivable(Object derivable)
        {
            this.List.Add($"Added Derivable [{derivable.Id}] {derivable}");
        }

        public void AddedDependency(Object dependent, Object dependee)
        {
            this.List.Add($"Added Dependency [{dependent.Id}] {dependent} -> [{dependee.Id}] {dependee}");
        }

        // Validation
        public void AddedError(IDerivationError derivationError)
        {
            this.List.Add($"Error {derivationError}");
        }

        // DerivationNode
        public void Cycle(Object root, Object derivable)
        {
            this.List.Add($"Cycle root: {root}, object: {derivable}");
        }

        public void Deriving(Object derivable)
        {
            this.List.Add($"Deriving {derivable}");
        }

        public void Derived(Object derivable)
        {
            this.List.Add($"Derived {derivable}");
        }

        public void PostDeriving(Object derivable)
        {
            this.List.Add($"Post Deriving {derivable}");
        }

        public void PostDerived(Object derivable)
        {
            this.List.Add($"Post Derived {derivable}");
        }
    }
}