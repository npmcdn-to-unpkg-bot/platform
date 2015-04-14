//------------------------------------------------------------------------------------------------- 
// <copyright file="MappingValidationErrors.cs" company="Allors bvba">
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
//-------------------------------------------------------------------------------------------------
namespace Allors.Databases.Object.SqlClient
{
    using System.Collections.Generic;

    using Allors.Meta;

    /// <summary>
    /// Holds the <see cref="IMappingValidationError"/>s that occured during the validation
    /// of the <see cref="Domain"/> against the database schema.
    /// </summary>
    internal class MappingValidationErrors
    {
        /// <summary>
        /// The errors that occured during validation of the <see cref="Domain"/> against the Sql schema.
        /// </summary>
        private readonly List<IMappingValidationError> errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingValidationErrors"/> class.
        /// </summary>
        internal MappingValidationErrors()
        {
            this.errors = new List<IMappingValidationError>();
        }

        /// <summary>
        /// Gets the schema validation errors.
        /// </summary>
        /// <value>The errors.</value>
        internal IMappingValidationError[] Errors
        {
            get { return this.errors.ToArray(); }
        }

        internal TableMappingValidationError[] TableErrors
        {
            get
            {
                var tableErrors = new List<TableMappingValidationError>();
                foreach (var error in this.errors)
                {
                    if (error is TableMappingValidationError)
                    {
                        tableErrors.Add((TableMappingValidationError)error);
                    }
                }

                return tableErrors.ToArray();
            }
        }

        internal ProcedureMappingValidationError[] ProcedureErrors
        {
            get
            {
                var procedureErrors = new List<ProcedureMappingValidationError>();
                foreach (var error in this.errors)
                {
                    if (error is TableMappingValidationError)
                    {
                        procedureErrors.Add((ProcedureMappingValidationError)error);
                    }
                }

                return procedureErrors.ToArray();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        internal bool HasErrors
        {
            get { return this.errors.Count > 0; }
        }

        /// <summary>
        /// Gets the validation error messages.
        /// </summary>
        /// <value>The validation error messages.</value>
        internal string[] Messages
        {
            get
            {
                var messages = new string[this.errors.Count];
                for (int i = 0; i < messages.Length; i++)
                {
                    messages[i] = this.errors[i].Message;
                }

                return messages;
            }
        }

        /// <summary>
        /// Adds a new schema validation error.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="relationType">The relation type.</param>
        /// <param name="roleType">The role .</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="kind">The kind of validation error.</param>
        /// <param name="message">The validation error message.</param>
        internal void AddTableError(IObjectType objectType, IRelationType relationType, IRoleType roleType, string tableName, string columnName, MappingValidationErrorKind kind, string message)
        {
            this.errors.Add(new TableMappingValidationError(objectType, relationType, roleType, tableName, columnName, kind, message));
        }

        internal void AddProcedureError(MappingProcedure mappingProcedure, MappingValidationErrorKind kind, string message)
        {
            this.errors.Add(new ProcedureMappingValidationError(mappingProcedure, kind, message));
        }
    }
}