// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugDerivation.cs" company="Allors bvba">
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

    using Allors.Domain.NonLogging;

    public sealed class Derivation : DerivationBase
    {
        private readonly IDerivationLog derivationLog;

        public Derivation(ISession session, IDerivationLog derivationLog)
            : base(session)
        {
            this.derivationLog = derivationLog;
            this.Validation = new Validation(this, this.derivationLog);
        }

        public Derivation(ISession session, IDerivationLog derivationLog, IEnumerable<long> forcedDerivations)
            : base(session, forcedDerivations)
        {
            this.derivationLog = derivationLog;
            this.Validation = new Validation(this, this.derivationLog);
        }

        public Derivation(ISession session, IDerivationLog derivationLog, IEnumerable<IObject> forcedObjects)
            : base(session, forcedObjects)
        {
            this.derivationLog = derivationLog;
            this.Validation = new Validation(this, this.derivationLog);
        }

        protected override DerivationGraphBase CreateDerivationGraph(DerivationBase derivation)
        {
            return new DerivationGraph(derivation);
        }

        protected override void OnAddedDerivable(Object derivable)
        {
        }

        protected override void OnAddedDependency(Object dependent, Object dependee)
        {
        }

        protected override void OnStartedPreparation(int preparationRun)
        {
        }

        protected override void OnStartedGeneration(int generation)
        {
        }

        protected override void OnPreparing(Object derivable)
        {
        }

    }
}