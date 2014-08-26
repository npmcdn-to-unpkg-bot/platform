// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtentStatement.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.Sql
{
    using System.Collections;

    using Allors.Meta;

    public abstract class ExtentStatement
    {
        private readonly ArrayList referenceAssociationInstances;
        private readonly ArrayList referenceAssociations;
        private readonly ArrayList referenceRoleInstances;
        private readonly ArrayList referenceRoles;

        private readonly SqlExtent extent;

        protected ExtentStatement(SqlExtent extent)
        {
            this.extent = extent;

            this.referenceRoles = new ArrayList();
            this.referenceAssociations = new ArrayList();

            this.referenceRoleInstances = new ArrayList();
            this.referenceAssociationInstances = new ArrayList();
        }

        public SqlExtent Extent
        {
            get { return this.extent; }
        }

        public abstract bool IsRoot { get; }

        public Schema Schema
        {
            get { return this.Session.SqlDatabase.Schema; }
        }

        public ExtentSort Sorter
        {
            get { return this.extent.Sorter; }
        }

        protected DatabaseSession Session
        {
            get { return this.extent.Session; }
        }

        protected ObjectType Type
        {
            get { return this.extent.ObjectType; }
        }

        public void AddJoins(ObjectType rootClass, string alias)
        {
            foreach (RoleType role in this.referenceRoles)
            {
                var relationType = role.RelationType;
                var association = relationType.AssociationType;

                if (!role.ObjectType.IsUnit)
                {
                    if ((association.IsMany && role.IsMany) || !relationType.ExistExclusiveRootClasses)
                    {
                        this.Append(" LEFT OUTER JOIN " + this.Schema.Table(role) + " " + role.RootName + "_R");
                        this.Append(" ON " + alias + "." + this.Schema.ObjectId + "=" + role.RootName + "_R." + this.Schema.AssociationId);
                    }
                    else
                    {
                        if (role.IsMany)
                        {
                            this.Append(" LEFT OUTER JOIN " + this.Schema.Table(role.ObjectType.ExclusiveRootClass) + " " + role.RootName + "_R");
                            this.Append(" ON " + alias + "." + this.Schema.ObjectId + "=" + role.RootName + "_R." + this.Schema.Column(association));
                        }
                    }
                }
            }

            foreach (RoleType role in this.referenceRoleInstances)
            {
                var relationType = role.RelationType;

                if (!role.ObjectType.IsUnit && role.IsOne)
                {
                    if (!relationType.ExistExclusiveRootClasses)
                    {
                        this.Append(" LEFT OUTER JOIN " + this.Schema.Objects + " " + this.GetJoinName(role));
                        this.Append(" ON " + this.GetJoinName(role) + "." + this.Schema.ObjectId + "=" + role.RootName + "_R." + this.Schema.RoleId + " ");
                    }
                    else
                    {
                        this.Append(" LEFT OUTER JOIN " + this.Schema.Objects + " " + this.GetJoinName(role));
                        this.Append(" ON " + this.GetJoinName(role) + "." + this.Schema.ObjectId + "=" + alias + "." + this.Schema.Column(role) + " ");
                    }
                }
            }

            foreach (AssociationType association in this.referenceAssociations)
            {
                var relationType = association.RelationType;
                var role = relationType.RoleType;

                if ((association.IsMany && role.IsMany) || !relationType.ExistExclusiveRootClasses)
                {
                    this.Append(" LEFT OUTER JOIN " + Schema.Table(association) + " " + association.Name + "_A");
                    this.Append(" ON " + alias + "." + this.Schema.ObjectId + "=" + association.Name + "_A." + this.Schema.RoleId);
                }
                else
                {
                    if (!role.IsMany)
                    {
                        this.Append(" LEFT OUTER JOIN " + Schema.Table(association.ObjectType.ExclusiveRootClass) + " " + association.Name + "_A");
                        this.Append(" ON " + alias + "." + this.Schema.ObjectId + "=" + association.Name + "_A." + Schema.Column(role));
                    }
                }
            }

            foreach (AssociationType association in this.referenceAssociationInstances)
            {
                var relationType = association.RelationType;
                var role = relationType.RoleType;

                if (!association.ObjectType.IsUnit && association.IsOne)
                {
                    if (!relationType.ExistExclusiveRootClasses)
                    {
                        this.Append(" LEFT OUTER JOIN " + this.Schema.Objects + " " + this.GetJoinName(association));
                        this.Append(" ON " + this.GetJoinName(association) + "." + this.Schema.ObjectId + "=" + association.Name + "_A." + this.Schema.AssociationId + " ");
                    }
                    else
                    {
                        if (role.IsOne)
                        {
                            this.Append(" LEFT OUTER JOIN " + this.Schema.Objects + " " + this.GetJoinName(association));
                            this.Append(" ON " + this.GetJoinName(association) + "." + this.Schema.ObjectId + "=" + association.Name + "_A." + this.Schema.ObjectId + " ");
                        }
                        else
                        {
                            this.Append(" LEFT OUTER JOIN " + this.Schema.Objects + " " + this.GetJoinName(association));
                            this.Append(" ON " + this.GetJoinName(association) + "." + this.Schema.ObjectId + "=" + alias + "." + Schema.Column(association) + " ");
                        }
                    }
                }
            }
        }

        public abstract string AddParameter(object obj);

        public bool AddWhere(ObjectType rootClass, string alias)
        {
            var useWhere = !(this.Extent.ObjectType.RootClasses.Count == 1) || this.Extent.ObjectType.ExclusiveRootClass.DerivedExclusiveConcreteLeafClass == null;
            
            if (useWhere)
            {
                this.Append(" WHERE ( ");
                this.Append(" " + alias + "." + this.Schema.TypeId + "=" + this.AddParameter(this.Type.Id));
                foreach (var subClass in this.Type.Subclasses)
                {
                    this.Append(" OR " + alias + "." + this.Schema.TypeId + "=" + this.AddParameter(subClass.Id));
                }

                this.Append(" ) ");
            }

            return useWhere;
        }

        public abstract void Append(string part);

        public abstract string CreateAlias();

        public abstract ExtentStatement CreateChild(SqlExtent extent, AssociationType association);

        public abstract ExtentStatement CreateChild(SqlExtent extent, RoleType role);

        public string GetJoinName(AssociationType association)
        {
            return association.SingularName + "_AC";
        }

        public string GetJoinName(RoleType role)
        {
            return role.FullSingularName + "_RC";
        }

        public void UseAssociation(AssociationType association)
        {
            if (!association.ObjectType.IsUnit && !this.referenceAssociations.Contains(association))
            {
                this.referenceAssociations.Add(association);
            }
        }

        public void UseAssociationInstance(AssociationType association)
        {
            if (!this.referenceAssociationInstances.Contains(association))
            {
                this.referenceAssociationInstances.Add(association);
            }
        }

        public void UseRole(RoleType role)
        {
            if (!role.ObjectType.IsUnit && !this.referenceRoles.Contains(role))
            {
                this.referenceRoles.Add(role);
            }
        }

        public void UseRoleInstance(RoleType role)
        {
            if (!this.referenceRoleInstances.Contains(role))
            {
                this.referenceRoleInstances.Add(role);
            }
        }
    }
}