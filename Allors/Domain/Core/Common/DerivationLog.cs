// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DerivationLog.cs" company="Allors bvba">
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

    using Allors;
    using Allors.Meta;

    public partial class DerivationLog
    {
        private readonly List<IDerivationError> errors;

        public DerivationLog(Derivation derivation)
        {
            Derivation = derivation;
            this.errors = new List<IDerivationError>();
        }

        public Derivation Derivation { get; private set; }

        public bool HasErrors
        {
            get
            {
                return this.errors.Count > 0;
            }
        }

        public IDerivationError[] Errors
        {
            get
            {
                return this.errors.ToArray();
            }
        }
        
        public void AddError(IDerivationError derivationError)
        {
            this.errors.Add(derivationError);
        }

        public void AddError(IObject association, RoleType roleType, string errorMessage, params object[] messageParam)
        {
            var error = new DerivationErrorGeneric(this, new DerivationRelation(association, roleType), errorMessage, messageParam);
            this.AddError(error);
        }

        public void AddConflicts(IConflict[] conflicts)
        {
            foreach (var conflict in conflicts)
            {
                var derivationRole = new DerivationRelation(conflict.Object, conflict.RoleType);
                var derivationErrorConflict = new DerivationErrorConflict(this, derivationRole);
                this.AddError(derivationErrorConflict);
            }
        }

        public void AssertExists(IObject association, RoleType roleType)
        {
            if (!association.Strategy.ExistRole(roleType))
            {
                this.AddError(new DerivationErrorRequired(this, association, roleType));
            }
        }

        public void AssertNotExists(IObject association, RoleType roleType)
        {
            if (association.Strategy.ExistRole(roleType))
            {
                this.AddError(new DerivationErrorNotAllowed(this, association, roleType));
            }
        }

        public void AssertNonEmptyString(IObject association, RoleType roleType)
        {
            if (association.Strategy.ExistRole(roleType))
            {
                if (association.Strategy.GetUnitRole(roleType).Equals(string.Empty))
                {
                    this.AddError(new DerivationErrorRequired(this, association, roleType));
                }
            }
        }

        public void AssertExistsNonEmptyString(IObject association, RoleType roleType)
        {
            this.AssertExists(association, roleType);
            this.AssertNonEmptyString(association, roleType);
        }

        public void AssertIsUnique(IObject association, RoleType roleType)
        {
            var objectType = roleType.AssociationType.ObjectType;
            var role = association.Strategy.GetRole(roleType);

            if (role != null)
            {
                var session = association.Strategy.Session;
                if (session is IDatabaseSession)
                {
                    var extent = association.Strategy.DatabaseSession.Extent(objectType);
                    extent.Filter.AddEquals(roleType, role);
                    if (extent.Count != 1)
                    {
                        this.AddError(new DerivationErrorUnique(this, association, roleType));
                    }
                }
                else
                {
                    var workspaceSession = (IWorkspaceSession)session;

                    // First check workspace
                    IObject[] workspaceObjects = workspaceSession.LocalExtent(objectType);
                    var workspaceMatch = false;
                    foreach (var workspaceObject in workspaceObjects)
                    {
                        var workspaceRole = workspaceObject.Strategy.GetRole(roleType);
                        if (role.Equals(workspaceRole))
                        {
                            if (workspaceMatch)
                            {
                                this.AddError(new DerivationErrorUnique(this, association, roleType));
                                break;
                            }

                            workspaceMatch = true;
                        }
                    }

                    if (!workspaceMatch)
                    {
                        var databaseSession = ((IWorkspaceSession)session).DatabaseSession;
                        var databaseAssociation = databaseSession.Instantiate(association);
                        var databaseRole = role is IObject ? databaseSession.Instantiate((IObject)role) : role;

                        var extent = association.Strategy.DatabaseSession.Extent(objectType);
                        extent.Filter.AddEquals(roleType, databaseRole);

                        if (databaseAssociation == null)
                        {
                            if (extent.Count > 0)
                            {
                                this.AddError(new DerivationErrorUnique(this, association, roleType));
                            }
                        }
                        else
                        {
                            if (extent.Count == 1)
                            {
                                if (!extent[0].Equals(databaseAssociation))
                                {
                                    this.AddError(new DerivationErrorUnique(this, association, roleType));
                                }
                            }
                            else if (extent.Count > 1)
                            {
                                this.AddError(new DerivationErrorUnique(this, association, roleType));
                            }
                        }
                    }
                }
            }
        }

        public void AssertAtLeastOne(IObject association, params RoleType[] roleTypes)
        {
            foreach (var roleType in roleTypes)
            {
                if (association.Strategy.ExistRole(roleType))
                {
                    return;
                }
            }

            this.AddError(new DerivationErrorAtLeastOne(this, DerivationRelation.Create(association, roleTypes)));
        }

        public void AssertExistsAtMostOne(IObject association, params RoleType[] roleTypes)
        {
            var count = 0;
            foreach (var roleType in roleTypes)
            {
                if (association.Strategy.ExistRole(roleType))
                {
                    ++count;
                }
            }

            if (count > 1)
            {
                this.AddError(new DerivationErrorAtMostOne(this, DerivationRelation.Create(association, roleTypes)));
            }
        }

        public void AssertAreEqual(IObject association, RoleType roleType, RoleType otherRoleType)
        {
            var value = association.Strategy.GetRole(roleType);
            var otherValue = association.Strategy.GetRole(otherRoleType);

            bool equal;
            if (value == null)
            {
                equal = otherValue == null;
            }
            else
            {
                equal = value.Equals(otherValue);
            }

            if (!equal)
            {
                this.AddError(new DerivationErrorEquals(this, DerivationRelation.Create(association, roleType, otherRoleType)));
            }
        }
    }
}