//------------------------------------------------------------------------------------------------- 
// <copyright file="MappingValidationError.cs" company="Allors bvba">
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
    using Allors.Meta;

    /// <summary>
    /// <para>Raised during the validation of the <see cref="Domain"/> against the Sql schema.</para>
    /// <para>The <see cref="TableMappingValidationError#IObjectType"/>, <see cref="TableMappingValidationError#IRelationType"/>
    /// and <see cref="TableMappingValidationError#IRoleType"/> objects are mutually exclusive.
    /// </para>
    /// </summary>
    internal class TableMappingValidationError : IMappingValidationError
    {
        /// <summary>
        /// The column name.
        /// </summary>
        private readonly string columnName;

        /// <summary>
        /// The kind of schema validation error.
        /// </summary>
        private readonly MappingValidationErrorKind kind;

        /// <summary>
        /// The validation error message.
        /// </summary>
        private readonly string message;

        /// <summary>
        /// The invalid relation type.
        /// </summary>
        private readonly IRelationType relationType;

        /// <summary>
        /// The invalid role.
        /// </summary>
        private readonly IRoleType role;

        /// <summary>
        /// The name of the table.
        /// </summary>
        private readonly string tableName;

        /// <summary>
        /// The object type.
        /// </summary>
        private readonly IObjectType objectType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableMappingValidationError"/> class.
        /// </summary>
        /// <param name="objectType">The invalid object type.</param>
        /// <param name="relationType">The invalid relation type.</param>
        /// <param name="role">The invalid role.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="errorKind">The kind of validation error.</param>
        /// <param name="message">The validation error message.</param>
        internal TableMappingValidationError(IObjectType objectType, IRelationType relationType, IRoleType role, string tableName, string columnName, MappingValidationErrorKind errorKind, string message)
        {
            this.objectType = objectType;
            this.relationType = relationType;
            this.role = role;
            this.tableName = tableName;
            this.columnName = columnName;
            this.kind = errorKind;
            this.message = message;
        }

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <value>The name of the column.</value>
        internal string ColumnName
        {
            get { return this.columnName; }
        }

        /// <summary>
        /// Gets the kind of validation error.
        /// </summary>
        /// <value>The kind of validation error.</value>
        public MappingValidationErrorKind Kind
        {
            get { return this.kind; }
        }

        /// <summary>
        /// Gets the validation error message.
        /// </summary>
        /// <value>The validation error message.</value>
        public string Message
        {
            get { return this.message; }
        }

        /// <summary>
        /// Gets the invalid relation type.
        /// </summary>
        /// <value>The ChangedRelations Type.</value>
        internal IRelationType IRelationType
        {
            get { return this.relationType; }
        }

        /// <summary>
        /// Gets the invalid role.
        /// </summary>
        /// <value>The role .</value>
        internal IRoleType Role
        {
            get { return this.role; }
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        internal string TableName
        {
            get { return this.tableName; }
        }

        /// <summary>
        /// Gets the invalid object type. 
        /// </summary>
        /// <value>The Object Type.</value>
        internal IObjectType IObjectType
        {
            get { return this.objectType; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return this.message;
        }
    }
}