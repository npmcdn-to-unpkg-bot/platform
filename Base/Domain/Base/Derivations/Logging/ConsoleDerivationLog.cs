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
    using System;

    using Object = Allors.Domain.Object;

    public sealed class ConsoleDerivationLog : IDerivationLog
    {
        // Derivation
        public void StartedGeneration(int generation)
        {
            Console.WriteLine($"Started Generation #{generation}");
        }

        public void StartedPreparation(int preparationRun)
        {
            Console.WriteLine($"Started Preparation #{preparationRun}");
        }

        public void PreDeriving(Object derivable)
        {
            Console.WriteLine($"Preparing #{derivable}");
        }

        public void PreDerived(Object derivable)
        {
            Console.WriteLine($"PreDerived [{derivable.Id}] {derivable}");
        }

        public void AddedDerivable(Object derivable)
        {
            Console.WriteLine($"Added Derivable [{derivable.Id}] {derivable}");
        }

        public void AddedDependency(Object dependent, Object dependee)
        {
            Console.WriteLine($"Added Dependency [{dependent.Id}] {dependent} -> [{dependee.Id}] {dependee}");
        }

        // Validation
        public void AddedError(IDerivationError derivationError)
        {
            Console.WriteLine($"Error {derivationError}");
        }

        // DerivationNode
        public void Cycle(Object root, Object derivable)
        {
            Console.WriteLine($"Cycle root: {root}, object: {derivable}");
        }

        public void Deriving(Object derivable)
        {
            Console.WriteLine($"Deriving {derivable}");
        }

        public void Derived(Object derivable)
        {
            Console.WriteLine($"Derived {derivable}");
        }

        public void PostDeriving(Object derivable)
        {
            Console.WriteLine($"Post Deriving {derivable}");
        }

        public void PostDerived(Object derivable)
        {
            Console.WriteLine($"Post Derived {derivable}");
        }
    }
}