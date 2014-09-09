//-------------------------------------------------------------------------------------------------
// <copyright file="Permissions.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the role type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System.Collections.Generic;

    using Allors;
    using Allors.Meta;

    public partial class Permissions
    {
        public void Sync()
        {
            var permissionByOperationByConcreteClassByOperandType = new Dictionary<OperandType, Dictionary<ObjectType, Dictionary<Operation, Permission>>>();

            foreach (Permission permission in new Permissions(this.Session).Extent())
            {
                if (!permission.ExistOperandType || !permission.ExistConcreteClass || !permission.Operation.HasValue)
                {
                    permission.Delete();
                    return;
                }

                Dictionary<ObjectType, Dictionary<Operation, Permission>> permissionByOperationByConcreteClass;
                if (!permissionByOperationByConcreteClassByOperandType.TryGetValue(permission.OperandType, out permissionByOperationByConcreteClass))
                {
                    permissionByOperationByConcreteClass = new Dictionary<ObjectType, Dictionary<Operation, Permission>>();
                    permissionByOperationByConcreteClassByOperandType[permission.OperandType] = permissionByOperationByConcreteClass;
                }

                Dictionary<Operation, Permission> permissionByOperation;
                if (!permissionByOperationByConcreteClass.TryGetValue(permission.ConcreteClass, out permissionByOperation))
                {
                    permissionByOperation = new Dictionary<Operation, Permission>();
                    permissionByOperationByConcreteClass.Add(permission.ConcreteClass, permissionByOperation);
                }

                permissionByOperation[permission.Operation.Value] = permission;
            }

            var domain = this.Session.Population.ObjectFactory.MetaPopulation;
            foreach (var relationType in domain.RelationTypes)
            {
                {
                    // AssociationType
                    var associationType = relationType.AssociationType;
                    Dictionary<ObjectType, Dictionary<Operation, Permission>> permissionByOperationByConcreteClass;
                    permissionByOperationByConcreteClassByOperandType.TryGetValue(associationType, out permissionByOperationByConcreteClass);

                    var composite = associationType.RelationType.RoleType.ObjectType as Composite;
                    if (composite != null)
                    {
                        foreach (var concreteClass in composite.LeafClasses)
                        {
                            Dictionary<Operation, Permission> permissionByOperation = null;
                            if (permissionByOperationByConcreteClass != null)
                            {
                                permissionByOperationByConcreteClass.TryGetValue(concreteClass, out permissionByOperation);
                            }

                            Operation[] operations = { Operation.Read };
                            foreach (var operation in operations)
                            {
                                Permission permission = null;
                                if (permissionByOperation != null)
                                {
                                    permissionByOperation.TryGetValue(operation, out permission);
                                }

                                if (permission == null)
                                {
                                    permission = new PermissionBuilder(this.Session).Build();
                                }

                                permission.Sync(concreteClass, associationType, operation);
                            }
                        }
                    }
                }

                {
                    // RoleType
                    var roleType = relationType.RoleType;
                    Dictionary<ObjectType, Dictionary<Operation, Permission>> permissionByOperationByConcreteClass;
                    permissionByOperationByConcreteClassByOperandType.TryGetValue(roleType, out permissionByOperationByConcreteClass);

                    foreach (var concreteClass in roleType.RelationType.AssociationType.ObjectType.LeafClasses)
                    {
                        Dictionary<Operation, Permission> permissionByOperation = null;
                        if (permissionByOperationByConcreteClass != null)
                        {
                            permissionByOperationByConcreteClass.TryGetValue(concreteClass, out permissionByOperation);
                        }

                        var operations = new[] { Operation.Read, Operation.Write };

                        foreach (var operation in operations)
                        {
                            Permission permission = null;
                            if (permissionByOperation != null)
                            {
                                permissionByOperation.TryGetValue(operation, out permission);
                            }

                            if (operation == Operation.Write && roleType.RelationType.IsDerived)
                            {
                                if (permission != null)
                                {
                                    permission.Delete();
                                }
                            }
                            else
                            {
                                if (permission == null)
                                {
                                    permission = new PermissionBuilder(this.Session).Build();
                                }

                                permission.Sync(concreteClass, roleType, operation);
                            }
                        }
                    }
                }
            }

            foreach (var methodType in domain.MethodTypes)
            {
                Dictionary<ObjectType, Dictionary<Operation, Permission>> permissionByOperationByConcreteClass;
                permissionByOperationByConcreteClassByOperandType.TryGetValue(methodType, out permissionByOperationByConcreteClass);

                foreach (var concreteClass in methodType.ObjectType.LeafClasses)
                {
                    Dictionary<Operation, Permission> permissionByOperation = null;
                    if (permissionByOperationByConcreteClass != null)
                    {
                        permissionByOperationByConcreteClass.TryGetValue(concreteClass, out permissionByOperation);
                    }

                    Permission permission = null;
                    if (permissionByOperation != null)
                    {
                        permissionByOperation.TryGetValue(Operation.Execute, out permission);
                    }

                    if (permission == null)
                    {
                        permission = new PermissionBuilder(this.Session).Build();
                    }

                    permission.Sync(concreteClass, methodType, Operation.Execute);
                }
            }
        }

        protected override void CoreSetup(Setup config)
        {
            base.CoreSetup(config);

            this.Sync();

            SecurityCache.Invalidate();
        }
    }
}