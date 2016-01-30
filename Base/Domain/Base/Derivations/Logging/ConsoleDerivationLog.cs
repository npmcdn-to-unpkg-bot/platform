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
            var message = DerivationLogFormatter.FormatStartedGeneration(generation);
            Console.WriteLine(message);
        }

        public void StartedPreparation(int preparationRun)
        {
            var message = DerivationLogFormatter.FormatStartedPreparation(preparationRun);
            Console.WriteLine(message);
        }

        public void PreDeriving(Object derivable)
        {
            var message = DerivationLogFormatter.FormatPreDeriving(derivable);
            Console.WriteLine(message);
        }

        public void PreDerived(Object derivable)
        {
            var message = DerivationLogFormatter.FormatPreDerived(derivable);
            Console.WriteLine(message);
        }

        public void AddedDerivable(Object derivable)
        {
            var message = DerivationLogFormatter.FormatAddedDerivable(derivable);
            Console.WriteLine(message);
        }

        public void AddedDependency(Object dependent, Object dependee)
        {
            var message = DerivationLogFormatter.FormatAddedDependency(dependent, dependee);
            Console.WriteLine(message);
        }

        // Validation
        public void AddedError(IDerivationError derivationError)
        {
            var message = DerivationLogFormatter.FormatAddedError(derivationError);
            Console.WriteLine(message);
        }

        // DerivationNode
        public void Cycle(Object root, Object derivable)
        {
            var message = DerivationLogFormatter.FormatCycle(root, derivable);
            Console.WriteLine(message);
        }

        public void Deriving(Object derivable)
        {
            var message = DerivationLogFormatter.FormatDeriving(derivable);
            Console.WriteLine(message);
        }

        public void Derived(Object derivable)
        {
            var message = DerivationLogFormatter.FormatDerived(derivable);
            Console.WriteLine(message);
        }

        public void PostDeriving(Object derivable)
        {
            var message = DerivationLogFormatter.FormatPostDeriving(derivable);
            Console.WriteLine(message);
        }

        public void PostDerived(Object derivable)
        {
            var message = DerivationLogFormatter.FormatPostDerived(derivable);
            Console.WriteLine(message);
        }
    }
}