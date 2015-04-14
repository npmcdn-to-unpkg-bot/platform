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

namespace Allors.Databases.Object.SqlClient
{
    using System.Collections;
    using Allors.Meta;

    internal abstract class ExtentStatement
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

        internal SqlExtent Extent
        {
            get { return this.extent; }
        }

        internal abstract bool IsRoot { get; }

        internal SqlClient.Mapping Mapping
        {
            get { return this.Session.SqlDatabase.Mapping; }
        }

        internal ExtentSort Sorter
        {
            get { return this.extent.Sorter; }
        }

        protected DatabaseSession Session
        {
            get { return this.extent.Session; }
        }

        protected IObjectType Type
        {
            get { return this.extent.ObjectType; }
        }

        internal void AddJoins(IObjectType rootClass, string alias)
        {
            foreach (IRoleType role in this.referenceRoles)
            {
                var relationType = role.RelationType;
                var association = relationType.AssociationType;

                if (!role.ObjectType.IsUnit)
                {
                    if ((association.IsMany && role.IsMany) || !relationType.ExistExclusiveLeafClasses)
                    {
                        this.Append(" LEFT OUTER JOIN " + this.Mapping.Table(role) + " " + role.SingularFullName + "_R");
                        this.Append(" ON " + alias + "." + this.Mapping.ObjectId + "=" + role.SingularFullName + "_R." + this.Mapping.AssociationId);
                    }
                    else
                    {
                        if (role.IsMany)
                        {
                            this.Append(" LEFT OUTER JOIN " + this.Mapping.Table(((IComposite)role.ObjectType).ExclusiveLeafClass) + " " + role.SingularFullName + "_R");
                            this.Append(" ON " + alias + "." + this.Mapping.ObjectId + "=" + role.SingularFullName + "_R." + this.Mapping.Column(association));
                        }
                    }
                }
            }

            foreach (IRoleType role in this.referenceRoleInstances)
            {
                var relationType = role.RelationType;

                if (!role.ObjectType.IsUnit && role.IsOne)
                {
                    if (!relationType.ExistExclusiveLeafClasses)
                    {
                        this.Append(" LEFT OUTER JOIN " + this.Mapping.Objects + " " + this.GetJoinName(role));
                        this.Append(" ON " + this.GetJoinName(role) + "." + this.Mapping.ObjectId + "=" + role.SingularFullName + "_R." + this.Mapping.RoleId + " ");
                    }
                    else
                    {
                        this.Append(" LEFT OUTER JOIN " + this.Mapping.Objects + " " + this.GetJoinName(role));
                        this.Append(" ON " + this.GetJoinName(role) + "." + this.Mapping.ObjectId + "=" + alias + "." + this.Mapping.Column(role) + " ");
                    }
                }
            }

            foreach (IAssociationType association in this.referenceAssociations)
            {
                var relationType = association.RelationType;
                var role = relationType.RoleType;

                if ((association.IsMany && role.IsMany) || !relationType.ExistExclusiveLeafClasses)
                {
                    this.Append(" LEFT OUTER JOIN " + this.Mapping.Table(association) + " " + association.SingularFullName + "_A");
                    this.Append(" ON " + alias + "." + this.Mapping.ObjectId + "=" + association.SingularFullName + "_A." + this.Mapping.RoleId);
                }
                else
                {
                    if (!role.IsMany)
                    {
                        this.Append(" LEFT OUTER JOIN " + this.Mapping.Table(association.ObjectType.ExclusiveLeafClass) + " " + association.SingularFullName + "_A");
                        this.Append(" ON " + alias + "." + this.Mapping.ObjectId + "=" + association.SingularFullName + "_A." + this.Mapping.Column(role));
                    }
                }
            }

            foreach (IAssociationType association in this.referenceAssociationInstances)
            {
                var relationType = association.RelationType;
                var role = relationType.RoleType;

                if (!association.ObjectType.IsUnit && association.IsOne)
                {
                    if (!relationType.ExistExclusiveLeafClasses)
                    {
                        this.Append(" LEFT OUTER JOIN " + this.Mapping.Objects + " " + this.GetJoinName(association));
                        this.Append(" ON " + this.GetJoinName(association) + "." + this.Mapping.ObjectId + "=" + association.SingularFullName + "_A." + this.Mapping.AssociationId + " ");
                    }
                    else
                    {
                        if (role.IsOne)
                        {
                            this.Append(" LEFT OUTER JOIN " + this.Mapping.Objects + " " + this.GetJoinName(association));
                            this.Append(" ON " + this.GetJoinName(association) + "." + this.Mapping.ObjectId + "=" + association.SingularFullName + "_A." + this.Mapping.ObjectId + " ");
                        }
                        else
                        {
                            this.Append(" LEFT OUTER JOIN " + this.Mapping.Objects + " " + this.GetJoinName(association));
                            this.Append(" ON " + this.GetJoinName(association) + "." + this.Mapping.ObjectId + "=" + alias + "." + this.Mapping.Column(association) + " ");
                        }
                    }
                }
            }
        }

        internal abstract string AddParameter(object obj);

        internal bool AddWhere(IObjectType rootClass, string alias)
        {
            var useWhere = !this.Extent.ObjectType.ExistExclusiveLeafClass;
            
            if (useWhere)
            {
                this.Append(" WHERE ( ");
                if (!this.Type.IsInterface)
                {
                    this.Append(" " + alias + "." + this.Mapping.TypeId + "=" + this.AddParameter(this.Type.Id));
                }
                else
                {
                    var first = true;
                    foreach (var subClass in ((IInterface)this.Type).Subclasses)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            this.Append(" OR ");
                        }

                        this.Append(" " + alias + "." + this.Mapping.TypeId + "=" + this.AddParameter(subClass.Id));
                    }
                }

                this.Append(" ) ");
            }

            return useWhere;
        }

        internal abstract void Append(string part);

        internal abstract string CreateAlias();

        internal abstract ExtentStatement CreateChild(SqlExtent extent, IAssociationType association);

        internal abstract ExtentStatement CreateChild(SqlExtent extent, IRoleType role);

        internal string GetJoinName(IAssociationType association)
        {
            return association.SingularFullName + "_AC";
        }

        internal string GetJoinName(IRoleType role)
        {
            return role.SingularFullName + "_RC";
        }

        internal void UseAssociation(IAssociationType association)
        {
            if (!association.ObjectType.IsUnit && !this.referenceAssociations.Contains(association))
            {
                this.referenceAssociations.Add(association);
            }
        }

        internal void UseAssociationInstance(IAssociationType association)
        {
            if (!this.referenceAssociationInstances.Contains(association))
            {
                this.referenceAssociationInstances.Add(association);
            }
        }

        internal void UseRole(IRoleType role)
        {
            if (!role.ObjectType.IsUnit && !this.referenceRoles.Contains(role))
            {
                this.referenceRoles.Add(role);
            }
        }

        internal void UseRoleInstance(IRoleType role)
        {
            if (!this.referenceRoleInstances.Contains(role))
            {
                this.referenceRoleInstances.Add(role);
            }
        }
    }
}