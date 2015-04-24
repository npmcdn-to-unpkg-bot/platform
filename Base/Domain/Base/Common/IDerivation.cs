// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDerivation.cs" company="Allors bvba">
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

    using Allors;
    using Allors.Meta;

    public interface IDerivation
    {
        bool InDatabase { get; }

        bool InWorkspace { get; }

        ISession Session { get; }

        DerivationLog Log { get; }

        IChangeSet ChangeSet { get; }

        int Generation { get; }

        ISet<Object> DerivedObjects { get; }

        object this[string name] { get; set; }

        bool IsForced(ObjectId objectId);

        ISet<IRoleType> GetChangedRoleTypes(IObject association);

        void AddDerivable(Object derivable);

        void AddDerivables(IEnumerable<Object> derivables);

        /// <summary>
        /// The dependee is derived before the dependent object;
        /// </summary>
        /// <param name="dependent"></param>
        /// <param name="dependee"></param>
        void AddDependency(Object dependent, Object dependee);

        DerivationLog Derive();
    }
}