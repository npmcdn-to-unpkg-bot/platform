// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtentFiltered.cs" company="Allors bvba">
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

namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Databases.Object.SqlClient;

    using Meta;

    public class ExtentFiltered : SqlExtent
    {
        private readonly DatabaseSession session;
        private readonly IComposite objectType;

        private AndPredicate filter;

        public ExtentFiltered(DatabaseSession session, Strategy strategy, IRoleType roleType)
            : this(session, (IComposite)roleType.ObjectType)
        {
            this.Strategy = strategy;
            this.RoleType = roleType;
        }

        public ExtentFiltered(DatabaseSession session, Strategy strategy, IAssociationType associationType)
            : this(session, associationType.ObjectType)
        {
            this.Strategy = strategy;
            this.AssociationType = associationType;
        }

        public ExtentFiltered(DatabaseSession session, IComposite objectType)
        {
            this.session = session;
            this.objectType = objectType;
        }

        public override ICompositePredicate Filter
        {
            get
            {
                this.LazyLoadFilter();
                return this.filter;
            }
        }

        public SqlClient.Schema Schema
        {
            get { return this.session.SqlDatabase.Schema; }
        }

        public override DatabaseSession Session
        {
            get { return this.session; }
        }

        public override IComposite ObjectType
        {
            get { return this.objectType; }
        }

        public IAssociationType AssociationType { get; private set; }

        public IRoleType RoleType { get; private set; }

        public Strategy Strategy { get; private set; }

        public override string BuildSql(ExtentStatement statement)
        {
            this.LazyLoadFilter();
            this.filter.Setup(statement);

            if (this.objectType.ExistLeafClasses)
            {
                if (this.objectType.ExistExclusiveLeafClass)
                {
                    return this.BuildSqlWithExclusiveRootClass(statement);
                }

                return this.BuildSqlWithExclusiveRootClasses(statement);
            }

            return null;
        }

        internal void CheckAssociation(IAssociationType associationType)
        {
            // TODO: Optimize
            if (!new List<IAssociationType>(this.objectType.AssociationTypes).Contains(associationType))
            {
                throw new ArgumentException("Extent does not implement association " + associationType.SingularFullName);
            }
        }

        internal void CheckRole(IRoleType roleType)
        {
            // TODO: Optimize
            if (!new List<IRoleType>(this.objectType.RoleTypes).Contains(roleType))
            {
                throw new ArgumentException("Extent does not implement role " + roleType.SingularFullName);
            }
        }


        protected override IList<ObjectId> GetObjectIds()
        {
            if (this.Strategy != null)
            {
                return this.Strategy.ExtentGetCompositeAssociations(this.AssociationType);
            }

            this.session.Flush();

            var statement = new ExtentStatementRoot(this);
            var objectIds = new List<ObjectId>();

            var alias = this.BuildSql(statement);
            
            using (var command = statement.CreateDbCommand(alias))
            {
                if (command != null)
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var objectId = this.session.SqlDatabase.AllorsObjectIds.Parse(reader.GetValue(0).ToString());
                            objectIds.Add(objectId);
                        }

                        reader.Close();
                    }
                }
            }

            return objectIds;
        }

        /// <summary>
        /// Lazy loads the filter.
        /// Should also be used to upgrade from a strategy extent to a full extent
        /// </summary>
        protected override void LazyLoadFilter()
        {
            if (this.filter == null)
            {
                this.filter = new AndPredicate(this);
                this.Strategy = null;
                this.AssociationType = null;
                this.RoleType = null;
                this.FlushCache();
            }
        }

        private string BuildSqlWithExclusiveRootClass(ExtentStatement statement)
        {
            var alias = statement.CreateAlias();
            var rootClass = this.objectType.ExclusiveLeafClass;

            if (statement.IsRoot)
            {
                statement.Append("SELECT DISTINCT " + alias + "." + this.Schema.ObjectId);
                if (statement.Sorter != null)
                {
                    statement.Sorter.BuildSelect(statement, alias);
                }

                statement.Append(" FROM " + this.Schema.Table(rootClass) + " " + alias);
                statement.AddJoins(rootClass, alias);
                statement.AddWhere(rootClass, alias);
                if (this.filter != null)
                {
                    this.filter.BuildWhere(statement, alias);
                }
            }
            else
            {
                // ContainedIn
                var inStatement = (ExtentStatementChild)statement;

                if (inStatement.RoleType != null)
                {
                    var inRole = inStatement.RoleType;
                    var inIRelationType = inRole.RelationType;
                    if (inIRelationType.Multiplicity == Multiplicity.ManyToMany || !inIRelationType.ExistExclusiveLeafClasses)
                    {
                        statement.Append("SELECT " + inRole.AssociationType.SingularFullName + "_A." + this.Schema.AssociationId);
                    }
                    else
                    {
                        if (inRole.IsMany)
                        {
                            statement.Append("SELECT " + alias + "." + this.Schema.ObjectId);
                        }
                        else
                        {
                            statement.Append("SELECT " + inRole.AssociationType.SingularFullName + "_A." + this.Schema.Column(inRole.AssociationType));
                        }
                    }

                    statement.Append(" FROM " + this.Schema.Table(rootClass) + " " + alias);
                    statement.AddJoins(rootClass, alias);

                    var wherePresent = statement.AddWhere(rootClass, alias);
                    var filterUsed = false;
                    if (this.filter != null)
                    {
                        filterUsed = this.filter.BuildWhere(statement, alias);
                    }

                    if (wherePresent || filterUsed)
                    {
                        statement.Append(" AND ");
                    }
                    else
                    {
                        statement.Append(" WHERE ");
                    }

                    if (inIRelationType.Multiplicity == Multiplicity.ManyToMany || !inIRelationType.ExistExclusiveLeafClasses)
                    {
                        statement.Append(inRole.AssociationType.SingularFullName + "_A." + this.Schema.AssociationId + " IS NOT NULL ");
                    }
                    else
                    {
                        if (inRole.IsMany)
                        {
                            statement.Append(alias + "." + this.Schema.Column(inRole.AssociationType) + " IS NOT NULL ");
                        }
                        else
                        {
                            statement.Append(inRole.AssociationType.SingularFullName + "_A." +
                                             this.Schema.Column(inRole.AssociationType) + " IS NOT NULL ");
                        }
                    }
                }
                else
                {
                    var inAssociation = inStatement.AssociationType;
                    var inIRelationType = inAssociation.RelationType;

                    if (inIRelationType.Multiplicity == Multiplicity.ManyToMany || !inIRelationType.ExistExclusiveLeafClasses)
                    {
                        statement.Append("SELECT " + inAssociation.RoleType.SingularFullName + "_R." + this.Schema.RoleId);
                    }
                    else
                    {
                        if (inAssociation.RoleType.IsMany)
                        {
                            statement.Append("SELECT " + inAssociation.RoleType.SingularFullName + "_R." +
                                             this.Schema.Column(inAssociation.RoleType));
                        }
                        else
                        {
                            statement.Append("SELECT " + alias + "." + this.Schema.ObjectId);
                        }
                    }

                    statement.Append(" FROM " + this.Schema.Table(rootClass) + " " + alias);
                    statement.AddJoins(rootClass, alias);

                    var wherePresent = statement.AddWhere(rootClass, alias);
                    var filterUsed = false;
                    if (this.filter != null)
                    {
                        filterUsed = this.filter.BuildWhere(statement, alias);
                    }

                    if (wherePresent || filterUsed)
                    {
                        statement.Append(" AND ");
                    }
                    else
                    {
                        statement.Append(" WHERE ");
                    }

                    if (inIRelationType.Multiplicity == Multiplicity.ManyToMany || !inIRelationType.ExistExclusiveLeafClasses)
                    {
                        statement.Append(inAssociation.RoleType.SingularFullName + "_R." + this.Schema.RoleId + " IS NOT NULL ");
                    }
                    else
                    {
                        if (inAssociation.RoleType.IsMany)
                        {
                            statement.Append(inAssociation.RoleType.SingularFullName + "_R." +
                                             this.Schema.Column(inAssociation.RoleType) + " IS NOT NULL ");
                        }
                        else
                        {
                            statement.Append(alias + "." + inAssociation.RoleType.SingularFullName + " IS NOT NULL ");
                        }
                    }
                }
            }

            return alias;
        }

        private string BuildSqlWithExclusiveRootClasses(ExtentStatement statement)
        {
            if (statement.IsRoot)
            {
                for (var i = 0; i < this.objectType.LeafClasses.Count(); i++)
                {
                    var alias = statement.CreateAlias();
                    var rootClass = this.objectType.LeafClasses.ToArray()[i];

                    statement.Append("SELECT " + alias + "." + this.Schema.ObjectId);
                    if (statement.Sorter != null)
                    {
                        statement.Sorter.BuildSelect(statement);
                    }

                    statement.Append(" FROM " + this.Schema.Table(rootClass) + " " + alias);

                    statement.AddJoins(rootClass, alias);
                    statement.AddWhere(rootClass, alias);

                    if (this.filter != null)
                    {
                        this.filter.BuildWhere(statement, alias);
                    }

                    if (i < this.objectType.LeafClasses.Count() - 1)
                    {
                        statement.Append("\nUNION\n");
                    }
                }
            }
            else
            {
                var inStatement = (ExtentStatementChild)statement;

                if (inStatement.RoleType != null)
                {
                    var useUnion = false;
                    foreach (var rootClass in this.objectType.LeafClasses)
                    {
                        var inRole = inStatement.RoleType;
                        var inIRelationType = inRole.RelationType;

                        if (!((IComposite)inRole.ObjectType).LeafClasses.Contains(rootClass))
                        {
                            continue;
                        }

                        if (useUnion)
                        {
                            statement.Append("\nUNION\n");
                        }
                        else
                        {
                            useUnion = true;
                        }

                        var alias = statement.CreateAlias();

                        if (inIRelationType.Multiplicity == Multiplicity.ManyToMany || !inIRelationType.ExistExclusiveLeafClasses)
                        {
                            statement.Append("SELECT " + inRole.AssociationType.SingularFullName + "_A." + this.Schema.AssociationId);
                        }
                        else
                        {
                            if (inRole.IsMany)
                            {
                                statement.Append("SELECT " + alias + "." + this.Schema.ObjectId);
                            }
                            else
                            {
                                statement.Append("SELECT " + inRole.AssociationType.SingularFullName + "_A." + this.Schema.Column(inRole.AssociationType));
                            }
                        }

                        statement.Append(" FROM " + this.Schema.Table(rootClass) + " " + alias);

                        statement.AddJoins(rootClass, alias);

                        var wherePresent = statement.AddWhere(rootClass, alias);
                        var filterUsed = false;
                        if (this.filter != null)
                        {
                            filterUsed = this.filter.BuildWhere(statement, alias);
                        }

                        if (wherePresent || filterUsed)
                        {
                            statement.Append(" AND ");
                        }
                        else
                        {
                            statement.Append(" WHERE ");
                        }

                        if (inIRelationType.Multiplicity == Multiplicity.ManyToMany || !inIRelationType.ExistExclusiveLeafClasses)
                        {
                            statement.Append(inRole.AssociationType.SingularFullName + "_A." + this.Schema.AssociationId + " IS NOT NULL ");
                        }
                        else
                        {
                            if (inRole.IsMany)
                            {
                                statement.Append(alias + "." + this.Schema.Column(inRole.AssociationType) + " IS NOT NULL ");
                            }
                            else
                            {
                                statement.Append(inRole.AssociationType.SingularFullName + "_A." + this.Schema.Column(inRole.AssociationType) + " IS NOT NULL ");
                            }
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < this.objectType.LeafClasses.Count(); i++)
                    {
                        var alias = statement.CreateAlias();
                        var rootClass = this.objectType.LeafClasses.ToArray()[i];

                        if (statement.IsRoot)
                        {
                            statement.Append("SELECT " + alias + "." + this.Schema.ObjectId);
                            if (statement.Sorter != null)
                            {
                                statement.Sorter.BuildSelect(statement);
                            }
                        }
                        else
                        {
                            statement.Append("SELECT " + alias + "." + this.Schema.ObjectId);
                        }

                        statement.Append(" FROM " + this.Schema.Table(rootClass) + " " + alias);

                        statement.AddJoins(rootClass, alias);
                        statement.AddWhere(rootClass, alias);

                        if (this.filter != null)
                        {
                            this.filter.BuildWhere(statement, alias);
                        }

                        if (i < this.objectType.LeafClasses.Count() - 1)
                        {
                            statement.Append("\nUNION\n");
                        }
                    }
                }
            }

            return null;
        }
    }
}