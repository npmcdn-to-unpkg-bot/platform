//------------------------------------------------------------------------------------------------- 
// <copyright file="Unit.cs" company="Allors bvba">
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
// <summary>Defines the ObjectType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;

    public partial class Unit : ObjectType
    {
        private int unitTag;

        public Unit(Whole domain, Guid id) : base(domain, id)
        {
            this.Domain.OnUnitCreated(this);
        }

        public int UnitTag
        {
            get
            {
                return this.unitTag;
            }

            set
            {
                this.unitTag = value;
                this.Domain.Stale();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a binary.
        /// </summary>
        /// <value><c>true</c> if this instance is a binary; otherwise, <c>false</c>.</value>
        public bool IsBinary
        {
            get { return this.Id.Equals(UnitIds.BinaryId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a boolean.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a boolean; otherwise, <c>false</c>.
        /// </value>
        public bool IsBoolean
        {
            get { return this.Id.Equals(UnitIds.BooleanId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a date time.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is a date time; otherwise, <c>false</c>.
        /// </value>
        public bool IsDateTime
        {
            get { return this.Id.Equals(UnitIds.DatetimeId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a decimal.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is a decimal; otherwise, <c>false</c>.
        /// </value>
        public bool IsDecimal
        {
            get { return this.Id.Equals(UnitIds.DecimalId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a double.
        /// </summary>
        /// <value><c>true</c> if this instance is a double; otherwise, <c>false</c>.</value>
        public bool IsDouble
        {
            get { return this.Id.Equals(UnitIds.DoubleId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is an integer.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is an integer; otherwise, <c>false</c>.
        /// </value>
        public bool IsInteger
        {
            get { return this.Id.Equals(UnitIds.IntegerId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is long.
        /// </summary>
        /// <value><c>true</c> if this instance is a long; otherwise, <c>false</c>.</value>
        public bool IsLong
        {
            get { return this.Id.Equals(UnitIds.LongId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a string.
        /// </summary>
        /// <value><c>true</c> if this instance is a string; otherwise, <c>false</c>.</value>
        public bool IsString
        {
            get { return this.Id.Equals(UnitIds.StringId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a unique.
        /// </summary>
        /// <value><c>true</c> if this instance is a unique; otherwise, <c>false</c>.</value>
        public bool IsUnique
        {
            get { return this.Id.Equals(UnitIds.Unique); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance requires precision.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance requires precision; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrecisionRequired
        {
            get
            {
                if (this.IsDecimal)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance requires a scale.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance requires a scale; otherwise, <c>false</c>.
        /// </value>
        public bool IsScaleRequired
        {
            get
            {
                if (this.IsDecimal)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance requires a size.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance requires a size; otherwise, <c>false</c>.
        /// </value>
        public bool IsSizeRequired
        {
            get
            {
                if (this.IsString || this.IsBinary)
                {
                    return true;
                }

                return false;
            }
        }
    }
}