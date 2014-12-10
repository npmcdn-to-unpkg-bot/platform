// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReferenceOne2ManyTest.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.General
{
    using System;

    using Allors;
    using Allors.Meta;

    using NUnit.Framework;

    public abstract class ReferenceOne2ManyTest : ReferenceSubjectTest
    {
        [Test]
        [Category("Dynamic")]
        public void DifferentAssociation()
        {
            for (var iRepeat = 0; iRepeat < this.GetRepeats().Length; iRepeat++)
            {
                var repeat = this.GetRepeats()[iRepeat];
                for (var iTestRepeat = 0; iTestRepeat < this.GetTestRepeats().Length; iTestRepeat++)
                {
                    var testRepeat = this.GetTestRepeats()[iTestRepeat];
                    for (int iAssertRepeat = 0; iAssertRepeat < this.GetAssertRepeats().Length; iAssertRepeat++)
                    {
                        int assertRepeat = this.GetAssertRepeats()[iAssertRepeat];
                        for (int iTransactionFlag = 0; iTransactionFlag < this.GetBooleanFlags().Length; iTransactionFlag++)
                        {
                            var transactionFlag = this.GetBooleanFlags()[iTransactionFlag];
                            for (int iRelation = 0; iRelation < this.GetRelations().Length; iRelation++)
                            {
                                var relationType = this.GetRelations()[iRelation];
                                IObject[] emptyRoles = this.CreateArray(relationType.RoleType.ObjectType, 0);

                                // Different AssociationTypes With Same ObjectType
                                var associationTypes = relationType.AssociationType.ObjectType.ConcreteClasses;
                                for (int iAssociationType = 0; iAssociationType < associationTypes.Length; iAssociationType++)
                                {
                                    var associationType = associationTypes[iAssociationType];
                                    IObject[] associations = this.CreateAssociationsWithSameClass(relationType, associationType);
                                    var roleTypes = relationType.RoleType.ObjectType.ConcreteClasses;
                                    for (int iRoleType = 0; iRoleType < roleTypes.Length; iRoleType++)
                                    {
                                        ObjectType roleClass = roleTypes[iRoleType];

                                        // One Role
                                        var role = this.GetSession().Create(roleClass);
                                        IObject[] roles = this.CreateRoles(relationType, role);
                                        new DifferentAssociationSameRole(this).Test(
                                            relationType, 
                                            associations, 
                                            role, 
                                            roles, 
                                            emptyRoles, 
                                            transactionFlag, 
                                            repeat, 
                                            assertRepeat, 
                                            testRepeat);
                                    }

                                    for (int iRoleType = 0; iRoleType < roleTypes.Length; iRoleType++)
                                    {
                                        ObjectType roleClass = roleTypes[iRoleType];

                                        // Many RoleTypes With Same ObjectType
                                        IObject[] roles = this.CreateRolesWithSameClass(relationType, roleClass);
                                        new DifferentAssociationSameRolesByOne(this).Test(
                                            relationType, 
                                            associations, 
                                            roles, 
                                            transactionFlag, 
                                            repeat, 
                                            assertRepeat, 
                                            testRepeat);
                                        new DifferentAssociationDifferentRolesByOne(this).Test(
                                            relationType, 
                                            associations, 
                                            roles, 
                                            transactionFlag, 
                                            repeat, 
                                            testRepeat, 
                                            assertRepeat);
                                        new DifferentAssociationOverlappingRolesByOne(this).Test(
                                            relationType.RoleType, 
                                            associations, 
                                            roles, 
                                            emptyRoles, 
                                            transactionFlag, 
                                            repeat, 
                                            testRepeat, 
                                            assertRepeat);
                                    }

                                    {
                                        // Many RoleTypes Different ObjectTypes
                                        IObject[] roles = this.CreateRolesWithDifferentClass(relationType);
                                        new DifferentAssociationSameRolesByOne(this).Test(relationType, associations, roles, transactionFlag, repeat, assertRepeat, testRepeat);
                                        new DifferentAssociationDifferentRolesByOne(this).Test(relationType, associations, roles, transactionFlag, repeat, testRepeat, assertRepeat);
                                        new DifferentAssociationOverlappingRolesByOne(this).Test(relationType.RoleType, associations, roles, emptyRoles, transactionFlag, repeat, testRepeat, assertRepeat);
                                    }
                                }

                                {
                                    // Different Associaitons With Different ObjectTypes
                                    IObject[] associations = this.CreateAssociationsWithDifferentClass(relationType);
                                    var roleTypes = relationType.RoleType.ObjectType.ConcreteClasses;
                                    for (int iRoleType = 0; iRoleType < roleTypes.Length; iRoleType++)
                                    {
                                        ObjectType roleClass = roleTypes[iRoleType];

                                        // One Role
                                        var role = this.GetSession().Create(roleClass);
                                        IObject[] roles = this.CreateRoles(relationType, role);
                                        new DifferentAssociationSameRole(this).Test(relationType, associations, role, roles, emptyRoles, transactionFlag, repeat, assertRepeat, testRepeat);
                                    }

                                    for (int iRoleType = 0; iRoleType < roleTypes.Length; iRoleType++)
                                    {
                                        ObjectType roleClass = roleTypes[iRoleType];

                                        // Many RoleTypes With Same ObjectType
                                        IObject[] roles = this.CreateRolesWithSameClass(relationType, roleClass);
                                        new DifferentAssociationSameRolesByOne(this).Test(relationType, associations, roles, transactionFlag, repeat, assertRepeat, testRepeat);
                                        new DifferentAssociationDifferentRolesByOne(this).Test(relationType, associations, roles, transactionFlag, repeat, testRepeat, assertRepeat);
                                        new DifferentAssociationOverlappingRolesByOne(this).Test(relationType.RoleType, associations, roles, emptyRoles, transactionFlag, repeat, testRepeat, assertRepeat);
                                    }

                                    {
                                        // Many RoleTypes Different ObjectTypes
                                        IObject[] roles = this.CreateRolesWithDifferentClass(relationType);
                                        new DifferentAssociationSameRolesByOne(this).Test(relationType, associations, roles, transactionFlag, repeat, assertRepeat, testRepeat);
                                        new DifferentAssociationDifferentRolesByOne(this).Test(relationType, associations, roles, transactionFlag, repeat, testRepeat, assertRepeat);
                                        new DifferentAssociationOverlappingRolesByOne(this).Test(relationType.RoleType, associations, roles, emptyRoles, transactionFlag, repeat, testRepeat, assertRepeat);
                                    }
                                }
                            }
                        }

                        if (this.IsRollbackSupported())
                        {
                        }
                    }
                }
            }
        }

        [Test]
        [Category("Dynamic")]
        public void SameAssociation()
        {
            for (var iRepeat = 0; iRepeat < this.GetRepeats().Length; iRepeat++)
            {
                var repeat = this.GetRepeats()[iRepeat];
                for (var iTestRepeat = 0; iTestRepeat < this.GetTestRepeats().Length; iTestRepeat++)
                {
                    var testRepeat = this.GetTestRepeats()[iTestRepeat];
                    for (int iAssertRepeat = 0; iAssertRepeat < this.GetAssertRepeats().Length; iAssertRepeat++)
                    {
                        int assertRepeat = this.GetAssertRepeats()[iAssertRepeat];
                        for (int iTransactionFlag = 0; iTransactionFlag < this.GetBooleanFlags().Length; iTransactionFlag++)
                        {
                            var transactionFlag = this.GetBooleanFlags()[iTransactionFlag];
                            for (int iRelation = 0; iRelation < this.GetRelations().Length; iRelation++)
                            {
                                var relationType = this.GetRelations()[iRelation];
                                IObject[] emptyRoles = this.CreateArray(relationType.RoleType.ObjectType, 0);

                                var associationTypes = relationType.AssociationType.ObjectType.ConcreteClasses;
                                for (int iAssociationType = 0; iAssociationType < associationTypes.Length; iAssociationType++)
                                {
                                    var associationType = associationTypes[iAssociationType];
                                    var roleTypes = relationType.RoleType.ObjectType.ConcreteClasses;
                                    for (int iRoleType = 0; iRoleType < roleTypes.Length; iRoleType++)
                                    {
                                        var roleType = roleTypes[iRoleType];

                                        // One Role
                                        var association = this.GetSession().Create(associationType);
                                        var role = this.GetSession().Create(roleType);
                                        IObject[] allRoles = this.CreateRoles(relationType, role);
                                        new SameAssociationSameRole(this).Test(
                                            relationType, 
                                            association, 
                                            role, 
                                            allRoles, 
                                            emptyRoles, 
                                            transactionFlag, 
                                            repeat, 
                                            testRepeat, 
                                            assertRepeat);
                                    }

                                    for (int iRoleType = 0; iRoleType < roleTypes.Length; iRoleType++)
                                    {
                                        ObjectType roleClass = roleTypes[iRoleType];

                                        // Many RoleTypes With Same ObjectType
                                        var association = this.GetSession().Create(associationType);
                                        IObject[] allRoles = this.CreateRolesWithSameClass(relationType, roleClass);
                                        IObject[] rolesOtherDatabase =
                                            this.CreateRolesWithSameClass(this.GetSession2(), relationType, roleClass);
                                        new SameAssociationSameRolesByOne(this).Test(
                                            relationType, 
                                            association, 
                                            allRoles, 
                                            emptyRoles, 
                                            rolesOtherDatabase, 
                                            transactionFlag, 
                                            repeat, 
                                            testRepeat, 
                                            assertRepeat);
                                        new SameAssociationSameRolesByAll(this).Test(
                                            relationType, 
                                            association, 
                                            allRoles, 
                                            emptyRoles, 
                                            transactionFlag, 
                                            repeat, 
                                            testRepeat, 
                                            assertRepeat);
                                    }

                                    {
                                        // Many RoleTypes Different ObjectTypes
                                        var association = this.GetSession().Create(associationType);
                                        IObject[] allRoles = this.CreateRolesWithDifferentClass(relationType);
                                        IObject[] rolesOtherDatabase = this.CreateRolesWithDifferentClass(this.GetSession2(), relationType);
                                        new SameAssociationSameRolesByOne(this).Test(relationType, association, allRoles, emptyRoles, rolesOtherDatabase, transactionFlag, repeat, testRepeat, assertRepeat);
                                        new SameAssociationSameRolesByAll(this).Test(relationType, association, allRoles, emptyRoles, transactionFlag, repeat, testRepeat, assertRepeat);
                                    }
                                }
                            }
                        }

                        if (this.IsRollbackSupported())
                        {
                        }
                    }
                }
            }
        }

        private IObject[] CreateAssociationsWithDifferentClass(RelationType relationType)
        {
            IObject[] associations = this.CreateArray(relationType.AssociationType.ObjectType, this.GetAssociationCount());
            var concreteClasses = relationType.AssociationType.ObjectType.ConcreteClasses;
            for (int i = 0; i < associations.Length; i++)
            {
                int classIndex = i % concreteClasses.Length;
                var associationType = concreteClasses[classIndex];
                associations[i] = this.GetSession().Create(associationType);
            }

            return associations;
        }

        private IObject[] CreateAssociationsWithSameClass(RelationType relationType, ObjectType associationClass)
        {
            IObject[] associations = this.CreateArray(relationType.AssociationType.ObjectType, this.GetAssociationCount());
            for (int i = 0; i < this.GetAssociationCount(); i++)
            {
                associations[i] = this.GetSession().Create(associationClass);
            }

            return associations;
        }

        private IObject[] CreateRoles(RelationType relationType, IObject role)
        {
            IObject[] roles = this.CreateArray(relationType.RoleType.ObjectType, 1);
            roles[0] = role;
            return roles;
        }

        private IObject[] CreateRolesWithDifferentClass(RelationType relationType)
        {
            return this.CreateRolesWithDifferentClass(this.GetSession(), relationType);
        }

        private IObject[] CreateRolesWithDifferentClass(ISession session, RelationType relationType)
        {
            IObject[] allRoles = this.CreateArray(relationType.RoleType.ObjectType, this.GetRoleCount());
            var concreteClasses = relationType.RoleType.ObjectType.ConcreteClasses;
            for (int i = 0; i < allRoles.Length; i++)
            {
                int classIndex = i % concreteClasses.Length;
                ObjectType roleType = concreteClasses[classIndex];
                allRoles[i] = session.Create(roleType);
            }

            return allRoles;
        }

        private IObject[] CreateRolesWithSameClass(RelationType relationType, ObjectType roleClass)
        {
            return this.CreateRolesWithSameClass(this.GetSession(), relationType, roleClass);
        }

        private IObject[] CreateRolesWithSameClass(ISession session, RelationType relationType, ObjectType roleClass)
        {
            IObject[] allRoles = this.CreateArray(relationType.RoleType.ObjectType, this.GetRoleCount());
            for (int i = 0; i < allRoles.Length; i++)
            {
                allRoles[i] = session.Create(roleClass);
            }

            return allRoles;
        }

        private RelationType[] GetRelations()
        {
            return this.GetOne2ManyRelations(this.GetMetaDomain());
        }

        private class DifferentAssociationDifferentRolesByOne : ReferenceProxyTest
        {
            public DifferentAssociationDifferentRolesByOne(ReferenceTest referenceTest) : base(referenceTest)
            {
            }

            public void Test(RelationType relationType, IObject[] associations, IObject[] roles, bool transactionFlag, int repeat, int testRepeat, int assertRepeat)
            {
                this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, assertRepeat, testRepeat);

                // Add Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    var role = roles[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationForwardExists(relationType, associations, roles, i, transactionFlag, testRepeat);
                    }
                }

                // Remove Forward
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    var role = roles[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationForwardNotExists(relationType, associations, roles, i, transactionFlag, testRepeat);
                    }
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    var role = roles[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationForwardExists(relationType, associations, roles, i, transactionFlag, testRepeat);
                    }
                }

                // Remove Backward
                for (int i = associations.Length - 1; i >= 0; i--)
                {
                    var association = associations[i];
                    var role = roles[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationBackwardNotExists(relationType, associations, roles, i, transactionFlag, testRepeat);
                    }
                }
            }

            private void AssertRelationNotExists(RelationType relationType, IObject[] associations, IObject[] roles, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int iAssociation = 0; iAssociation < associations.Length; iAssociation++)
                    {
                        var association = associations[iAssociation];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.IsEmpty((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                        }
                    }

                    for (int iRole = 0; iRole < roles.Length; iRole++)
                    {
                        IObject testRole = roles[iRole];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                    }

                    if (transactionFlag)
                    {
                        this.GetSession().Commit();
                    }
                }
            }

            private void AssertRelationForwardExists(RelationType relationType, IObject[] associations, IObject[] roles, int index, bool transactionFlag, int testRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int k = 0; k < associations.Length; k++)
                    {
                        IObject testAssociation = associations[k];
                        IObject testRole = roles[k];
                        if (k <= index)
                        {
                            Assert.AreEqual(1, ((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType)).Length);
                            Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                            Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                        else
                        {
                            Assert.AreEqual(0, ((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType)).Length);
                            CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType), testRole);
                            Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationForwardNotExists(RelationType relationType, IObject[] associations, IObject[] roles, int index, bool transactionFlag, int testRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int k = 0; k < associations.Length; k++)
                    {
                        IObject testAssociation = associations[k];
                        IObject testRole = roles[k];
                        if (k <= index)
                        {
                            Assert.AreEqual(0, ((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType)).Length);
                            CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType), testRole);
                            Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                        else
                        {
                            Assert.AreEqual(1, ((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType)).Length);
                            Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                            Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationBackwardNotExists(RelationType relationType, IObject[] associations, IObject[] roles, int index, bool transactionFlag, int testRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int k = 0; k < associations.Length; k++)
                    {
                        IObject testAssociation = associations[k];
                        IObject testRole = roles[k];
                        if (k >= index)
                        {
                            Assert.AreEqual(0, ((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType)).Length);
                            CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType), testRole);
                            Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                        else
                        {
                            Assert.AreEqual(1, ((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType)).Length);
                            Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                            Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }
        }

        private class DifferentAssociationOverlappingRolesByOne : ReferenceProxyTest
        {
            public DifferentAssociationOverlappingRolesByOne(ReferenceTest referenceTest) : base(referenceTest)
            {
            }

            public void Test(RoleType roleType, IObject[] associations, IObject[] roles, IObject[] emptyRoles, bool transactionFlag, int repeat, int testRepeat, int assertRepeat)
            {
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    this.AssertRoleNotExists(roleType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(roleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRoleNotExists(roleType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(roleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRoleNotExists(roleType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int j = i; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            var role = roles[j];
                            association.Strategy.AddCompositeRole(roleType, role);
                            this.Commit(transactionFlag);
                        }
                    }

                    this.AssertRelationForwardExists(roleType, associations, i, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(roleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationExists(roleType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(roleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationExists(roleType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Forward
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int j = i; j < roles.Length; j++)
                    {
                        var role = roles[j];
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            association.Strategy.RemoveCompositeRole(roleType, role);
                            this.Commit(transactionFlag);
                        }
                    }

                    this.AssertRelationForwardNotExists(roleType, associations, i, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(roleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRoleNotExists(roleType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int j = i; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            var role = roles[j];
                            association.Strategy.AddCompositeRole(roleType, role);
                            this.Commit(transactionFlag);
                        }
                    }

                    this.AssertRelationForwardExists(roleType, associations, i, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Backward
                for (int i = associations.Length - 1; i >= 0; i--)
                {
                    var association = associations[i];
                    for (int j = i; j < roles.Length; j++)
                    {
                        var role = roles[j];
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            association.Strategy.RemoveCompositeRole(roleType, role);
                            this.Commit(transactionFlag);
                        }
                    }

                    this.AssertRelationBackwardNotExists(roleType, associations, i, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(roleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRoleNotExists(roleType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int j = i; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            var role = roles[j];
                            association.Strategy.AddCompositeRole(roleType, role);
                            this.Commit(transactionFlag);
                        }
                    }

                    this.AssertRelationForwardExists(roleType, associations, i, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove All
                for (int i = associations.Length - 1; i >= 0; i--)
                {
                    var association = associations[i];
                    for (int j = i; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            association.Strategy.RemoveCompositeRoles(roleType);
                            this.Commit(transactionFlag);
                        }
                    }

                    this.AssertRelationBackwardNotExists(roleType, associations, i, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int j = i; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            var role = roles[j];
                            association.Strategy.AddCompositeRole(roleType, role);
                            this.Commit(transactionFlag);
                        }
                    }

                    this.AssertRelationForwardExists(roleType, associations, i, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Set Null
                for (int i = associations.Length - 1; i >= 0; i--)
                {
                    var association = associations[i];
                    for (int j = i; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            association.Strategy.SetCompositeRoles(roleType, (IObject[])null);
                            this.Commit(transactionFlag);
                        }
                    }

                    this.AssertRelationBackwardNotExists(roleType, associations, i, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int j = i; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            var role = roles[j];
                            association.Strategy.AddCompositeRole(roleType, role);
                            this.Commit(transactionFlag);
                        }
                    }

                    this.AssertRelationForwardExists(roleType, associations, i, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Set Empty
                for (int i = associations.Length - 1; i >= 0; i--)
                {
                    var association = associations[i];
                    for (int j = i; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            association.Strategy.SetCompositeRoles(roleType, emptyRoles);
                            this.Commit(transactionFlag);
                        }
                    }

                    this.AssertRelationBackwardNotExists(roleType, associations, i, roles, transactionFlag, testRepeat, assertRepeat);
                }
            }

            private void AssertRoleNotExists(RoleType roleType, IObject[] associations, IObject[] roles, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int iAssociation = 0; iAssociation < associations.Length; iAssociation++)
                    {
                        var association = associations[iAssociation];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.IsEmpty((IObject[])association.Strategy.GetCompositeRoles(roleType));
                        }
                    }

                    for (int iRole = 0; iRole < roles.Length; iRole++)
                    {
                        IObject testRole = roles[iRole];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.IsNull(testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                        }
                    }

                    if (transactionFlag)
                    {
                        this.GetSession().Commit();
                    }
                }
            }

            private void AssertRelationExists(RoleType roleType, IObject[] associations, IObject[] roles, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int k = 0; k < associations.Length; k++)
                    {
                        IObject testAssociation = associations[k];
                        if (k < associations.Length - 1)
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsNotEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                            }

                            for (int l = 0; l < roles.Length; l++)
                            {
                                IObject testRole = roles[l];
                                if (l == k)
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                    }
                                }
                                else
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType), testRole);
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreNotEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsNotEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                            }

                            for (int l = 0; l < roles.Length; l++)
                            {
                                IObject testRole = roles[l];
                                if (l >= k)
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                    }
                                }
                                else
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType), testRole);
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreNotEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                    }
                                }
                            }
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationForwardExists(RoleType roleType, IObject[] associations, int associationIndex, IObject[] roles, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int k = 0; k < associations.Length; k++)
                    {
                        IObject testAssociation = associations[k];
                        if (k < associationIndex)
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsNotEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                            }

                            for (int l = 0; l < roles.Length; l++)
                            {
                                IObject testRole = roles[l];
                                if (l == k)
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                    }
                                }
                                else
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType), testRole);
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreNotEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                    }
                                }
                            }
                        }
                        else if (k == associationIndex)
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsNotEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                            }

                            for (int l = 0; l < roles.Length; l++)
                            {
                                IObject testRole = roles[l];
                                if (l >= k)
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                    }
                                }
                                else
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType), testRole);
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreNotEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                            }
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationForwardNotExists(RoleType roleType, IObject[] associations, int associationIndex, IObject[] roles, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int k = 0; k < associations.Length; k++)
                    {
                        IObject testAssociation = associations[k];

                        if (k <= associationIndex)
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsNotEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                            }

                            if (k == associations.Length - 1)
                            {
                                for (int l = 0; l < roles.Length; l++)
                                {
                                    IObject testRole = roles[l];
                                    if (l >= k)
                                    {
                                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                        {
                                            Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                                        }

                                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                        {
                                            Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                        }
                                    }
                                    else
                                    {
                                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                        {
                                            CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType), testRole);
                                        }

                                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                        {
                                            Assert.AreNotEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int l = 0; l < roles.Length; l++)
                                {
                                    IObject testRole = roles[l];
                                    if (l == k)
                                    {
                                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                        {
                                            Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                                        }

                                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                        {
                                            Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                        }
                                    }
                                    else
                                    {
                                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                        {
                                            CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType), testRole);
                                        }

                                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                        {
                                            Assert.AreNotEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationBackwardNotExists(RoleType roleType, IObject[] associations, int associationIndex, IObject[] roles, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int k = 0; k < associations.Length; k++)
                    {
                        IObject testAssociation = associations[k];

                        if (k >= associationIndex)
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsNotEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                            }

                            for (int l = 0; l < roles.Length; l++)
                            {
                                IObject testRole = roles[l];
                                if (l == k)
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(roleType));
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                    }
                                }
                                else
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(roleType), testRole);
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreNotEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(roleType.AssociationType));
                                    }
                                }
                            }
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }
        }

        private class DifferentAssociationSameRole : ReferenceProxyTest
        {
            public DifferentAssociationSameRole(ReferenceTest referenceTest) : base(referenceTest)
            {
            }

            public void Test(RelationType relationType, IObject[] associations, IObject role, IObject[] roles, IObject[] emptyRoles, bool transactionFlag, int repeat, int testRepeat, int assertRepeat)
            {
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    this.AssertRelationNotExists(relationType, associations, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Add Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationNotExists(relationType, associations, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    var association = associations[this.GetAssociationCount() - 1];
                    association.Strategy.RemoveCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, associations, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);
                    }

                    this.AssertExclusiveRelation(relationType, associations, association, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Add Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertExclusiveRelation(relationType, associations, associations[associations.Length - 1], role, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    var association = associations[this.GetAssociationCount() - 1];
                    association.Strategy.RemoveCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);
                    this.AssertExclusiveRelation(relationType, associations, associations[associations.Length - 1], role, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    associations[this.GetAssociationCount() - 1].Strategy.RemoveCompositeRole(relationType.RoleType, role);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, associations, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationNotExists(relationType, associations, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Set All
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.SetCompositeRoles(relationType.RoleType, roles);
                        this.Commit(transactionFlag);
                    }

                    this.AssertExclusiveRelation(relationType, associations, association, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove All
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    associations[this.GetAssociationCount() - 1].Strategy.RemoveCompositeRoles(relationType.RoleType);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, associations, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Set
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.SetCompositeRoles(relationType.RoleType, roles);
                        this.Commit(transactionFlag);
                    }

                    this.AssertExclusiveRelation(relationType, associations, association, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Set Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    associations[this.GetAssociationCount() - 1].Strategy.SetCompositeRoles(relationType.RoleType, (IObject[])null);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, associations, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Set
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.SetCompositeRoles(relationType.RoleType, roles);
                        this.Commit(transactionFlag);
                    }

                    this.AssertExclusiveRelation(relationType, associations, association, role, transactionFlag, testRepeat, assertRepeat);
                }

                // Set Empty
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    associations[this.GetAssociationCount() - 1].Strategy.SetCompositeRoles(relationType.RoleType, emptyRoles);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, associations, role, transactionFlag, testRepeat, assertRepeat);
                }
            }

            private void AssertRelationNotExists(RelationType relationType, IObject[] associations, IObject role, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int iAssociation = 0; iAssociation < associations.Length; iAssociation++)
                    {
                        var association = associations[iAssociation];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.IsEmpty((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                        }
                    }

                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.IsNull(role.Strategy.GetCompositeAssociation(relationType.AssociationType));
                    }

                    if (transactionFlag)
                    {
                        this.GetSession().Commit();
                    }
                }
            }

            private void AssertExclusiveRelation(RelationType relationType, IObject[] associations, IObject association, IObject role, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.Contains(role, (IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                    }

                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.AreEqual(1, ((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType)).Length);
                    }

                    for (int iAssociation = 0; iAssociation < associations.Length; iAssociation++)
                    {
                        IObject testAssociation = associations[iAssociation];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            if (testAssociation.Equals(association))
                            {
                                Assert.AreEqual(association, role.Strategy.GetCompositeAssociation(relationType.AssociationType));
                            }
                            else
                            {
                                Assert.IsEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                            }
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }
        }

        private class DifferentAssociationSameRolesByOne : ReferenceProxyTest
        {
            public DifferentAssociationSameRolesByOne(ReferenceTest referenceTest) : base(referenceTest)
            {
            }

            public void Test(RelationType relationType, IObject[] associations, IObject[] roles, bool transactionFlag, int repeat, int testRepeat, int assertRepeat)
            {
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (var j = 0; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            var role = roles[j];
                            association.Strategy.AddCompositeRole(relationType.RoleType, role);
                            this.Commit(transactionFlag);

                            this.AssertRelationForwardExists(relationType, associations, i, roles, j, transactionFlag, testRepeat, assertRepeat);
                        }
                    }
                }

                // Add Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationExists(relationType, associations, associations[associations.Length - 1], roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationExists(relationType, associations, associations[associations.Length - 1], roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Forward
                for (int i = 0; i < roles.Length; i++)
                {
                    var association = associations[associations.Length - 1];
                    var role = roles[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationForwardNotExists(relationType, associations, association, roles, i, transactionFlag, testRepeat, assertRepeat);
                    }
                }

                // Remove Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (var j = 0; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            var role = roles[j];
                            association.Strategy.AddCompositeRole(relationType.RoleType, role);
                            this.Commit(transactionFlag);

                            this.AssertRelationForwardExists(relationType, associations, i, roles, j, transactionFlag, testRepeat, assertRepeat);
                        }
                    }
                }

                // Remove Backward
                for (int i = roles.Length - 1; i >= 0; i--)
                {
                    var association = associations[associations.Length - 1];
                    var role = roles[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationBackwardNotExists(relationType, associations, association, roles, i, transactionFlag, testRepeat, assertRepeat);
                    }
                }

                // Remove Null
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, null);
                        this.Commit(transactionFlag);
                    }

                    this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove All
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    var association = associations[associations.Length - 1];
                    association.Strategy.RemoveCompositeRoles(relationType.RoleType);
                    this.Commit(transactionFlag);

                    this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (var j = 0; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            var role = roles[j];
                            association.Strategy.AddCompositeRole(relationType.RoleType, role);
                            this.Commit(transactionFlag);

                            this.AssertRelationForwardExists(relationType, associations, i, roles, j, transactionFlag, testRepeat, assertRepeat);
                        }
                    }
                }

                // Set Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    var association = associations[associations.Length - 1];
                    association.Strategy.SetCompositeRoles(relationType.RoleType, (IObject[])null);
                    this.Commit(transactionFlag);

                    this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int i = 0; i < associations.Length; i++)
                {
                    var association = associations[i];
                    for (var j = 0; j < roles.Length; j++)
                    {
                        for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                        {
                            var role = roles[j];
                            association.Strategy.AddCompositeRole(relationType.RoleType, role);
                            this.Commit(transactionFlag);

                            this.AssertRelationForwardExists(relationType, associations, i, roles, j, transactionFlag, testRepeat, assertRepeat);
                        }
                    }
                }

                // Set Empty
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    var association = associations[associations.Length - 1];
                    association.Strategy.SetCompositeRoles(relationType.RoleType, (IObject[])null);
                    this.Commit(transactionFlag);

                    this.AssertRelationNotExists(relationType, associations, roles, transactionFlag, testRepeat, assertRepeat);
                }
            }

            private void AssertRelationNotExists(RelationType relationType, IObject[] associations, IObject[] roles, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int iAssociation = 0; iAssociation < associations.Length; iAssociation++)
                    {
                        var association = associations[iAssociation];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.IsEmpty((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                        }
                    }

                    for (int iRole = 0; iRole < roles.Length; iRole++)
                    {
                        IObject testRole = roles[iRole];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                    }

                    if (transactionFlag)
                    {
                        this.GetSession().Commit();
                    }
                }
            }

            private void AssertRelationExists(RelationType relationType, IObject[] associations, IObject association, IObject[] roles, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int iAssociation = 0; iAssociation < associations.Length; iAssociation++)
                    {
                        IObject testAssociation = associations[iAssociation];
                        if (testAssociation.Equals(association))
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.AreEqual(this.GetRoleCount(), ((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType)).Length);
                            }

                            for (int iRole = 0; iRole < roles.Length; iRole++)
                            {
                                var role = roles[iRole];
                                for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                {
                                    Assert.Contains(role, (IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                                }

                                for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                {
                                    Assert.AreEqual(association, role.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                }
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.AreEqual(0, ((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType)).Length);
                            }

                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                            }
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationForwardExists(RelationType relationType, IObject[] associations, int associationIndex, IObject[] roles, int roleIndex, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int k = 0; k < associations.Length; k++)
                    {
                        IObject testAssociation = associations[k];
                        if (k == associationIndex)
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsNotEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                            }

                            for (int l = 0; l < roles.Length; l++)
                            {
                                IObject testRole = roles[l];
                                if (l <= roleIndex)
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                    }
                                }
                                else
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType), testRole);
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreNotEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                    }
                                }
                            }
                        }
                        else if (k + 1 == associationIndex)
                        {
                            for (int l = 0; l < roles.Length; l++)
                            {
                                IObject testRole = roles[l];
                                if (l <= roleIndex)
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType), testRole);
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreNotEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                    }
                                }
                                else
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                            }
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationForwardNotExists(RelationType relationType, IObject[] associations, IObject association, IObject[] roles, int roleIndex, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int iAssociation = 0; iAssociation < associations.Length; iAssociation++)
                    {
                        IObject testAssociation = associations[iAssociation];
                        if (association.Equals(testAssociation))
                        {
                            for (int k = 0; k < roles.Length; k++)
                            {
                                IObject testRole = roles[k];
                                if (k <= roleIndex)
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType), testRole);
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                    }
                                }
                                else
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                            }

                            for (int k = 0; k < roles.Length; k++)
                            {
                                IObject testRole = roles[k];
                                for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                {
                                    Assert.AreNotEqual(testRole, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                }
                            }
                        }
                    }
                }

                this.Commit(transactionFlag);
            }

            private void AssertRelationBackwardNotExists(RelationType relationType, IObject[] associations, IObject association, IObject[] roles, int roleIndex, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int iAssociation = 0; iAssociation < associations.Length; iAssociation++)
                    {
                        IObject testAssociation = associations[iAssociation];
                        if (association.Equals(testAssociation))
                        {
                            for (int k = 0; k < roles.Length; k++)
                            {
                                IObject testRole = roles[k];
                                if (k >= roleIndex)
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        CollectionAssert.DoesNotContain((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType), testRole);
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                    }
                                }
                                else
                                {
                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.Contains(testRole, (IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                                    }

                                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                    {
                                        Assert.AreEqual(testAssociation, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsEmpty((IObject[])testAssociation.Strategy.GetCompositeRoles(relationType.RoleType));
                            }

                            for (int k = 0; k < roles.Length; k++)
                            {
                                IObject testRole = roles[k];
                                for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                                {
                                    Assert.AreNotEqual(testRole, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                                }
                            }
                        }
                    }
                }

                this.Commit(transactionFlag);
            }
        }

        private class SameAssociationSameRole : ReferenceProxyTest
        {
            public SameAssociationSameRole(ReferenceTest referenceTest) : base(referenceTest)
            {
            }

            public void Test(RelationType relationType, IObject association, IObject role, IObject[] roles, IObject[] emptyRoles, bool transactionFlag, int repeat, int testRepeat, int assertRepeat)
            {
                this.AssertRelationNotExists(relationType, association, role, transactionFlag, testRepeat, assertRepeat);

                // Add
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.AddCompositeRole(relationType.RoleType, role);
                    this.Commit(transactionFlag);
                    this.AssertRelation(relationType, association, role, transactionFlag, testRepeat, assertRepeat);
                }

                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.RemoveCompositeRoles(relationType.RoleType);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, association, role, transactionFlag, testRepeat, assertRepeat);
                }

                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, roles);
                    this.Commit(transactionFlag);
                    this.AssertRelation(relationType, association, role, transactionFlag, testRepeat, assertRepeat);
                }

                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.RemoveCompositeRoles(relationType.RoleType);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, association, role, transactionFlag, testRepeat, assertRepeat);
                }

                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, roles);
                    this.Commit(transactionFlag);
                    this.AssertRelation(relationType, association, role, transactionFlag, testRepeat, assertRepeat);
                }

                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, (IObject[])null);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, association, role, transactionFlag, testRepeat, assertRepeat);
                }

                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, emptyRoles);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, association, role, transactionFlag, testRepeat, assertRepeat);
                }
            }

            private void AssertRelation(RelationType relationType, IObject association, IObject role, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.AreEqual(1, ((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType)).Length);
                    }

                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.Contains(role, (IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                    }

                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.AreEqual(association, role.Strategy.GetCompositeAssociation(relationType.AssociationType));
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationNotExists(RelationType relationType, IObject association, IObject role, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.IsEmpty((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                    }

                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.IsNull(role.Strategy.GetCompositeAssociation(relationType.AssociationType));
                    }

                    this.Commit(transactionFlag);
                }
            }
        }

        private class SameAssociationSameRolesByAll : ReferenceProxyTest
        {
            public SameAssociationSameRolesByAll(ReferenceTest referenceTest) : base(referenceTest)
            {
            }

            public void Test(RelationType relationType, IObject association, IObject[] roles, IObject[] emptyRoles, bool transactionFlag, int repeat, int testRepeat, int assertRepeat)
            {
                this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);

                // Add Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.AddCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);

                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                }

                // Add Empty
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.AddCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);

                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                }

                // Set
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, roles);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.AddCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add Empty
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.AddCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.RemoveCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.RemoveCompositeRoles(relationType.RoleType);
                    this.Commit(transactionFlag);

                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Set
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, roles);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove All
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.RemoveCompositeRoles(relationType.RoleType);
                    this.Commit(transactionFlag);

                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, roles);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove All
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.RemoveCompositeRoles(relationType.RoleType);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                }

                // Set
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, roles);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Set Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, (IObject[])null);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                }

                // Set
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, roles);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Set Empty Array
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, emptyRoles);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                }

                // TODO: (Exist)
            }

            private void AssertRelationNotExists(RelationType relationType, IObject association, IObject[] roles, bool transactionFlag, int assertRepeat, int testRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.IsEmpty((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                    }

                    for (int iRole = 0; iRole < roles.Length; iRole++)
                    {
                        IObject testRole = roles[iRole];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationExists(RelationType relationType, IObject association, IObject[] roles, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.IsNotEmpty((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                    }

                    for (int k = 0; k < roles.Length; k++)
                    {
                        IObject testRole = roles[k];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.Contains(testRole, (IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                        }

                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.AreEqual(association, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }
        }

        private class SameAssociationSameRolesByOne : ReferenceProxyTest
        {
            public SameAssociationSameRolesByOne(ReferenceTest referenceTest) : base(referenceTest)
            {
            }

            public void Test(RelationType relationType, IObject association, IObject[] roles, IObject[] emptyRoles, IObject[] rolesOtherDatabase, bool transactionFlag, int repeat, int testRepeat, int assertRepeat)
            {
                this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);

                // Add Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.AddCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);

                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                }

                // Add Empty
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.AddCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);

                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                }

                // Add
                for (int i = 0; i < roles.Length; i++)
                {
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        var role = roles[i];
                        association.Strategy.AddCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationForwardExists(relationType, association, roles, i, transactionFlag, testRepeat, assertRepeat);
                    }
                }

                // Add Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.AddCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Add Empty
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.AddCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.RemoveCompositeRole(relationType.RoleType, null);
                    this.Commit(transactionFlag);

                    this.AssertRelationExists(relationType, association, roles, transactionFlag, testRepeat, assertRepeat);
                }

                // Remove Forwards
                for (int i = 0; i < roles.Length; i++)
                {
                    var role = roles[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationForwardNotExists(relationType, association, roles, i, transactionFlag, testRepeat, assertRepeat);
                    }
                }

                this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);

                // Add
                for (int i = 0; i < roles.Length; i++)
                {
                    var role = roles[i];
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        association.Strategy.AddCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationForwardExists(relationType, association, roles, i, transactionFlag, testRepeat, assertRepeat);
                    }
                }

                // Remove Backwards
                for (int i = roles.Length - 1; i >= 0; i--)
                {
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        var role = roles[i];
                        association.Strategy.RemoveCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationBackwardsNotExists(relationType, association, roles, i, transactionFlag, testRepeat, assertRepeat);
                    }
                }

                this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);

                // Add
                for (int i = 0; i < roles.Length; i++)
                {
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        var role = roles[i];
                        association.Strategy.AddCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationForwardExists(relationType, association, roles, i, transactionFlag, testRepeat, assertRepeat);
                    }
                }

                // RemoveAll
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.RemoveCompositeRoles(relationType.RoleType);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                }

                // Add
                for (int i = 0; i < roles.Length; i++)
                {
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        var role = roles[i];
                        association.Strategy.AddCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationForwardExists(relationType, association, roles, i, transactionFlag, testRepeat, assertRepeat);
                    }
                }

                // Set Null
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, (IObject[])null);
                    this.Commit(transactionFlag);

                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                }

                // Add
                for (int i = 0; i < roles.Length; i++)
                {
                    for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                    {
                        var role = roles[i];
                        association.Strategy.AddCompositeRole(relationType.RoleType, role);
                        this.Commit(transactionFlag);

                        this.AssertRelationForwardExists(relationType, association, roles, i, transactionFlag, testRepeat, assertRepeat);
                    }
                }

                // Set Empty Array
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    association.Strategy.SetCompositeRoles(relationType.RoleType, emptyRoles);
                    this.Commit(transactionFlag);
                    this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                }

                // Add different Population
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    try
                    {
                        association.Strategy.AddCompositeRole(relationType.RoleType, rolesOtherDatabase[0]);
                        Assert.Fail();
                    }
                    catch (ArgumentException exception)
                    {
                        Assert.IsNotNull(exception);
                    }

                    this.Commit(transactionFlag);

                    for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                    {
                        this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                    }
                }

                // Set different Population
                for (int repeatIndex = 0; repeatIndex < repeat; repeatIndex++)
                {
                    try
                    {
                        association.Strategy.SetCompositeRoles(relationType.RoleType, rolesOtherDatabase);
                        Assert.Fail();
                    }
                    catch (ArgumentException exception)
                    {
                        Assert.IsNotNull(exception);
                    }

                    this.Commit(transactionFlag);

                    for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                    {
                        this.AssertRelationNotExists(relationType, association, roles, transactionFlag, assertRepeat, testRepeat);
                    }
                }

                // TODO: (Exist)
            }

            private void AssertRelationNotExists(RelationType relationType, IObject association, IObject[] roles, bool transactionFlag, int assertRepeat, int testRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.IsEmpty((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                    }

                    for (int iRole = 0; iRole < roles.Length; iRole++)
                    {
                        IObject testRole = roles[iRole];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationExists(RelationType relationType, IObject association, IObject[] roles, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.IsNotEmpty((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                    }

                    for (int k = 0; k < roles.Length; k++)
                    {
                        IObject testRole = roles[k];
                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.Contains(testRole, (IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                        }

                        for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                        {
                            Assert.AreEqual(association, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationForwardExists(RelationType relationType, IObject association, IObject[] roles, int roleIndex, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                    {
                        Assert.IsNotEmpty((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                    }

                    for (int k = 0; k < roles.Length; k++)
                    {
                        IObject testRole = roles[k];
                        if (k <= roleIndex)
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.Contains(testRole, (IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                            }

                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.AreEqual(association, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                CollectionAssert.DoesNotContain((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType), testRole);
                            }

                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                            }
                        }
                    }

                    this.Commit(transactionFlag);
                }
            }

            private void AssertRelationForwardNotExists(RelationType relationType, IObject association, IObject[] roles, int roleIndex, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int k = 0; k < roles.Length; k++)
                    {
                        IObject testRole = roles[k];
                        if (k <= roleIndex)
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                CollectionAssert.DoesNotContain((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType), testRole);
                            }

                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.Contains(testRole, (IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                            }

                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.AreEqual(association, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                            }
                        }
                    }
                }

                this.Commit(transactionFlag);
            }

            private void AssertRelationBackwardsNotExists(RelationType relationType, IObject association, IObject[] roles, int roleIndex, bool transactionFlag, int testRepeat, int assertRepeat)
            {
                for (int testRepeatIndex = 0; testRepeatIndex < testRepeat; testRepeatIndex++)
                {
                    for (int k = 0; k < roles.Length; k++)
                    {
                        IObject testRole = roles[k];
                        if (k >= roleIndex)
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                CollectionAssert.DoesNotContain((IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType), testRole);
                            }

                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.IsNull(testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                            }
                        }
                        else
                        {
                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.Contains(testRole, (IObject[])association.Strategy.GetCompositeRoles(relationType.RoleType));
                            }

                            for (int assertRepeateIndex = 0; assertRepeateIndex < assertRepeat; assertRepeateIndex++)
                            {
                                Assert.AreEqual(association, testRole.Strategy.GetCompositeAssociation(relationType.AssociationType));
                            }
                        }
                    }
                }

                this.Commit(transactionFlag);
            }
        }
    }
}