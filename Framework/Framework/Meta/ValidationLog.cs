//------------------------------------------------------------------------------------------------- 
// <copyright file="ValidationLog.cs" company="Allors bvba">
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
// <summary>Defines the ValidationReport type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Allors.Meta
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The report of the validation.
    /// </summary>
    public class ValidationLog
    {
        /// <summary>
        /// The list of errors.
        /// </summary>
        private readonly List<ValidationError> errors;
        
        /// <summary>
        /// The set of all ids.
        /// </summary>
        private readonly HashSet<Guid> ids;

        /// <summary>
        /// The set of all <see cref="IObjectType"/> names.
        /// </summary>
        private readonly HashSet<string> objectTypeNames;

        /// <summary>
        /// The set of all <see cref="RelationType"/> names.
        /// </summary>
        private readonly HashSet<string> relationTypeNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationLog"/> class.
        /// </summary>
        public ValidationLog()
        {
            this.errors = new List<ValidationError>();

            this.ids = new HashSet<Guid>();
            this.objectTypeNames = new HashSet<string>();
            this.relationTypeNames = new HashSet<string>();
        }

        /// <summary>
        /// Gets a value indicating whether this report contains errors.
        /// </summary>
        /// <value><c>true</c> if this report contains errors; otherwise, <c>false</c>.</value>
        public bool ContainsErrors
        {
            get { return this.Errors.Length > 0; }
        }
        
        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>The errors.</value>
        public ValidationError[] Errors
        {
            get { return this.errors.ToArray(); }
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <value>The messages.</value>
        public string[] Messages
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
        /// Adds a new validation error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="source">The source.</param>
        /// <param name="kind">The kind.</param>
        /// <param name="members">The members.</param>
        public void AddError(string message, object source, ValidationKind kind, params string[] members)
        {
            this.errors.Add(new ValidationError(message, source, kind, members));
        }

        /// <summary>
        /// Determines whether the specified source object has errors.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        ///  <c>true</c> if the specified source object has errors; otherwise, <c>false</c>.
        /// </returns>
        public bool HasErrors(object source)
        {
            foreach (ValidationError error in this.Errors)
            {
                if (error.Source.Equals(source))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds an id.
        /// </summary>
        /// <param name="id">The id   .</param>
        public void AddId(Guid id)
        {
            this.ids.Add(id);
        }

        /// <summary>
        /// Adds the name of the object type.
        /// </summary>
        /// <param name="type">The type .</param>
        public void AddObjectTypeName(string type)
        {
            this.objectTypeNames.Add(type);
        }

        /// <summary>
        /// Adds the name of the relation type.
        /// </summary>
        /// <param name="relation">The relation.</param>
        public void AddRelationTypeName(string relation)
        {
            this.relationTypeNames.Add(relation);
        }

        /// <summary>
        /// Gets a value indicating whether the id already exists.
        /// </summary>
        /// <param name="id">The id   .</param>
        /// <returns>The value indicating whether the id already exists.</returns>
        public bool ExistId(Guid id)
        {
            return this.ids.Contains(id);
        }
        
        /// <summary>
        /// Gets a value indicating whether the  name of the relation already exists.
        /// </summary>
        /// <param name="relationName">The name of the relation.</param>
        /// <returns>The value indicating whether the name of the relation already exists.</returns>
        public bool ExistRelationName(string relationName)
        {
            return this.relationTypeNames.Contains(relationName);
        }

        /// <summary>
        /// Gets a value indicating whether the name of the type already exists.
        /// </summary>
        /// <param name="typeName">The short name of the type.</param>
        /// <returns>The value indicating whether the name of the type already exists.</returns>
        public bool ExistObjectTypeName(string typeName)
        {
            return this.objectTypeNames.Contains(typeName);
        }
    }
}