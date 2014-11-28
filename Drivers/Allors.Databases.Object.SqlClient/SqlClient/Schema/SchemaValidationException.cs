//------------------------------------------------------------------------------------------------- 
// <copyright file="SchemaValidationException.cs" company="Allors bvba">
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
// <summary>Defines the SchemaValidationException type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Allors.Databases.Object.SqlClient
{
    using System;

    using Allors.Meta;

    /// <summary>
    /// Thrown when the <see cref="IDatabase"/> encounters a Sql schema that is incompatible with the <see cref="Domain"/>.
    /// </summary>
    internal class SchemaValidationException : Exception
    {
        /// <summary>
        /// The validation errors.
        /// </summary>
        private readonly SchemaValidationErrors validationErrors;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaValidationException"/> class.
        /// </summary>
        /// <param name="validationErrors">The validation errors.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        internal SchemaValidationException(SchemaValidationErrors validationErrors, string message, Exception innerException) : base(message, innerException)
        {
            this.validationErrors = validationErrors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaValidationException"/> class.
        /// </summary>
        /// <param name="validationErrors">The validation errors.</param>
        /// <param name="message">The message.</param>
        internal SchemaValidationException(SchemaValidationErrors validationErrors, string message) : base(message)
        {
            this.validationErrors = validationErrors;
        }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>The validation errors.</value>
        internal SchemaValidationErrors ValidationErrors
        {
            get { return this.validationErrors; }
        }
    }
}