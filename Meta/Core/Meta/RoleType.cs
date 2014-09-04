//------------------------------------------------------------------------------------------------- 
// <copyright file="RoleType.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the RoleType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;

    /// <summary>
    /// A <see cref="RoleType"/> defines the role side of a relation.
    /// This is also called the 'passive' side.
    /// RoleTypes can have composite and unit <see cref="ObjectType"/>s.
    /// </summary>
    public partial class RoleType
    {
        /// <summary>
        /// The maximum size value.
        /// </summary>
        public const int MaximumSize = -1;

        private int? scale;

        private int? precision;

        private int? size;

        public int? Scale
        {
            get
            {
                return this.scale;
            }

            set
            {
                this.scale = value;
                this.Environment.Stale();
            }
        }

        public int? Precision
        {
            get
            {
                return this.precision;
            }

            set
            {
                this.precision = value;
                this.Environment.Stale();
            }
        }

        public int? Size
        {
            get
            {
                return this.size;
            }

            set
            {
                this.size = value;
                this.Environment.Stale();
            }
        }

        /// <summary>
        /// Derive multiplicity, scale and size.
        /// </summary>
        internal void DeriveScaleAndSize()
        {
            var unitType = this.ObjectType as Unit;
            if (unitType != null)
            {
                switch (unitType.UnitTag)
                {
                    case UnitTags.AllorsString:
                        if (!this.Size.HasValue)
                        {
                            this.Size = 256;
                        }

                        this.Scale = null;
                        this.Precision = null;
                
                        break;
                    case UnitTags.AllorsBinary:
                        if (!this.Size.HasValue)
                        {
                            this.Size = MaximumSize;
                        }

                        this.Scale = null;
                        this.Precision = null;

                        break;
                    case UnitTags.AllorsDecimal:
                        if (!this.Precision.HasValue)
                        {
                            this.Precision = 19;
                        }

                        if (!this.Scale.HasValue)
                        {
                            this.Scale = 2;
                        }

                        this.Size = null;

                        break;

                    default:
                        this.Size = null;
                        this.Scale = null;
                        this.Precision = null;
                
                        break;
                }
            }
            else
            {
                this.Size = null;
                this.Scale = null;
                this.Precision = null;
            }
        }

        /// <summary>
        /// Validates the instance.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal void CoreValidate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (this.ObjectType != null)
            {
                var message = this.ValidationName + " has no ObjectType";
                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.ObjectType");
            }
            else
            {
                var unitType = this.ObjectType as Unit;
                if (unitType != null)
                {
                    switch (unitType.UnitTag)
                    {
                        case UnitTags.AllorsString:
                            if (this.Size.HasValue)
                            {
                                var message = this.ValidationName + " should have a size.";
                                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.Size");
                            }

                            break;
                        case UnitTags.AllorsBinary:
                            if (this.Size.HasValue)
                            {
                                var message = this.ValidationName + " should have a size.";
                                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.Size");
                            }

                            break;
                        case UnitTags.AllorsDecimal:
                            if (this.Precision.HasValue)
                            {
                                var message = this.ValidationName + " should have a precision.";
                                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.Precision");
                            }

                            if (this.Scale.HasValue)
                            {
                                var message = this.ValidationName + " should have a scale.";
                                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.Scale");
                            }

                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(this.AssignedSingularName) && string.IsNullOrEmpty(this.AssignedPluralName))
            {
                var message = this.ValidationName + " has a singular but no plural name";
                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.AssignedPluralName");
            }

            if (!string.IsNullOrEmpty(this.AssignedSingularName) && this.AssignedSingularName.Length < 2)
            {
                var message = this.ValidationName + " should have an assigned singular name with at least 2 characters";
                validationLog.AddError(message, this, ValidationKind.MinimumLength, "RoleType.AssignedSingularName");
            }

            if (!string.IsNullOrEmpty(this.AssignedPluralName) && this.AssignedPluralName.Length < 2)
            {
                var message = this.ValidationName + " should have an assigned plural role name with at least 2 characters";
                validationLog.AddError(message, this, ValidationKind.MinimumLength, "RoleType.AssignedPluralName");
            }
        }
    }
}