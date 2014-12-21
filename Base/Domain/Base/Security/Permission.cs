// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Permission.cs" company="Allors bvba">
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
    using System.Text;

    using Allors.Meta;

    using Resources;

    public partial class Permission
    {
        public OperandType OperandType
        {
            get
            {
                return (OperandType)this.Session.Population.MetaPopulation.Find(this.OperandTypePointer); 
            }

            set
            {
                if (value == null)
                {
                    this.RemoveOperandTypePointer();
                }
                else
                {
                    this.OperandTypePointer = value.Id;
                }
            }
        }

        public bool ExistOperandType
        {
            get
            {
                return this.ExistOperandTypePointer;
            }
        }

        public Operation Operation
        {
            get
            {
                return (Operation)this.OperationEnum;
            }

            set
            {
                this.OperationEnum = (int)value;
            }
        }

        public bool ExistOperation
        {
            get
            {
                return this.ExistOperationEnum;
            }
        }

        public ObjectType ConcreteClass
        {
            get
            {
                return (ObjectType)this.Session.Population.MetaPopulation.Find(this.ConcreteClassPointer);
            }

            set
            {
                if (value == null)
                {
                    this.RemoveConcreteClassPointer();
                }
                else
                {
                    this.ConcreteClassPointer = value.Id;
                }
            }
        }

        public bool ExistConcreteClass
        {
            get
            {
                return this.ConcreteClass != null;
            }
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            if (this.ExistOperation)
            {
                var operation = this.Operation;
                toString.Append(operation);
            }
            else
            {
                toString.Append("[missing operation]");
            }

            toString.Append(" for ");

            toString.Append(this.ExistOperandType ? this.OperandType.GetType().Name + ":" + this.OperandType.ToString() : "[missing operand]");

            return toString.ToString();
        }

        internal void Sync(ObjectType concreteClass, OperandType operandType, Operation operation)
        {
            this.OperandType = operandType;
            this.Operation = operation;
            this.ConcreteClassPointer = concreteClass.Id;
        }

        public void BaseDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            switch (this.Operation)
            {
                case Operation.Read:
                    // Read Operations should only be allowed on AssociaitonTypes && RoleTypes
                    if (!(this.OperandType is RoleType || this.OperandType is AssociationType))
                    {
                        derivation.Log.AddError(this, Meta.OperationEnum, ErrorMessages.PermissionOnlyReadForRoleOrAssociationType);
                    }

                    break;

                case Operation.Write:
                    // Write Operations should only be allowed on RoleTypes
                    if (!(this.OperandType is RoleType))
                    {
                        derivation.Log.AddError(this, Meta.OperationEnum, ErrorMessages.PermissionOnlyWriteForRoleType);
                    }

                    break;

                case Operation.Execute:
                    // Execute Operations should only be allowed on MethodTypes
                    if (!(this.OperandType is MethodType))
                    {
                        derivation.Log.AddError(this, Meta.OperationEnum, ErrorMessages.PermissionOnlyExecuteForMethodType);
                    }

                    break;
            }

            SecurityCache.Invalidate();
        }
    }
}