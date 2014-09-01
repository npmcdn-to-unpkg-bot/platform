// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetaObject.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base class for Meta objects.
    /// </summary>
    public abstract partial class MetaObject
    {
        /// <summary>
        /// Gets the id comparer for meta objects.
        /// </summary>
        public static readonly IComparer<MetaObject> IdComparer = new PrivateIdComparer();

        private Guid id;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The meta object id.</value>
        public Guid Id
        {
            get
            {
                return this.id;
            }

            protected set
            {
                this.id = value;
                this.Environment.Stale();
            }
        }

        /// <summary>
        /// Gets the id as a number only string.
        /// </summary>
        /// <value>The id as a number only string.</value>
        public string IdAsNumberString
        {
            get { return this.Id.ToString("N").ToLower(); }
        }

        /// <summary>
        /// Gets the id as a string.
        /// </summary>
        /// <value>The id as a string.</value>
        public string IdAsString
        {
            get { return this.Id.ToString("D").ToLower(); }
        }

        public Environment Environment { get; protected set; }

        /// <summary>
        /// Gets the validation name.
        /// </summary>
        protected abstract string ValidationName { get; }

        /// <summary>
        /// Validate this object.
        /// </summary>
        /// <param name="validationLog">
        /// The validation log.
        /// </param>
        protected internal virtual void Validate(ValidationLog validationLog)
        {
            if (this.Id == Guid.Empty)
            {
                var message = "id on " + this.ValidationName + " is required";
                validationLog.AddError(message, this, ValidationKind.Unique, "MetaObject.Id");
            }
            else
            {
                if (validationLog.ExistId(this.Id))
                {
                    var message = "id " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, "MetaObject.Id");
                }
                else
                {
                    validationLog.AddId(this.Id);
                }
            }
        }
        
        /// <summary>
        /// The id comparer.
        /// </summary>
        private class PrivateIdComparer : IComparer<MetaObject>
        {
            /// <summary>
            /// Compares two relationTypes by id.
            /// </summary>
            /// <returns>
            /// The result of the comparison.
            /// </returns>
            /// <param name="x">The first object to compare.</param><param name="y">The second object to compare.</param>
            public int Compare(MetaObject x, MetaObject y)
            {
                return x.Id.CompareTo(y.Id);
            }
        }
    }
}