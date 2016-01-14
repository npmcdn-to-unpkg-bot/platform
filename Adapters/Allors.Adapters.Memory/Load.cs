// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Load.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using Allors.Meta;
    using Adapters;

    public class Load
    {
        private static readonly byte[] emptyByteArray = new byte[0];

        private readonly Session session;
        private readonly XmlReader reader;

        public Load(Session session, XmlReader reader)
        {
            this.session = session;
            this.reader = reader;
        }

        public void Execute()
        {
            while (this.reader.Read())
            {
                // only process elements, ignore others
                if (this.reader.NodeType.Equals(XmlNodeType.Element))
                {
                    if (this.reader.Name.Equals(Serialization.Population))
                    {
                        Serialization.CheckVersion(this.reader);

                        if (!this.reader.IsEmptyElement)
                        {
                            this.LoadPopulation();
                        }

                        break;
                    }
                }
            }
        }

        private void LoadPopulation()
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (this.reader.Name.Equals(Serialization.Objects))
                        {
                            if (!this.reader.IsEmptyElement)
                            {
                                this.LoadObjects();
                            }
                        }
                        else if (reader.Name.Equals(Serialization.Relations))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadRelations();
                            }
                        }
                        else
                        {
                            throw new Exception("Unknown child element <" + this.reader.Name + "> in parent element <" + Serialization.Population + ">");
                        }

                        break;
                    case XmlNodeType.EndElement:
                        if (!this.reader.Name.Equals(Serialization.Population))
                        {
                            throw new Exception("Expected closing element </" + Serialization.Population + ">");
                        }

                        return;
                }
            }
        }

        private void LoadObjects()
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (this.reader.Name.Equals(Serialization.Database))
                        {
                            if (!this.reader.IsEmptyElement)
                            {
                                this.LoadObjectTypes();
                            }
                        }
                        else if (reader.Name.Equals(Serialization.Workspace))
                        {
                            throw new Exception("Can not load workspace objects in a database.");
                        }
                        else
                        {
                            throw new Exception("Unknown child element <" + this.reader.Name + "> in parent element <" + Serialization.Objects + ">");
                        }

                        break;
                    case XmlNodeType.EndElement:
                        if (!this.reader.Name.Equals(Serialization.Objects))
                        {
                            throw new Exception("Expected closing element </" + Serialization.Objects + ">");
                        }

                        return;
                }
            }
        }

        private void LoadObjectTypes()
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (this.reader.Name.Equals(Serialization.ObjectType))
                        {
                            if (!this.reader.IsEmptyElement)
                            {
                                var objectTypeIdString = this.reader.GetAttribute(Serialization.Id);
                                if (string.IsNullOrEmpty(objectTypeIdString))
                                {
                                    throw new Exception("object type has no id");
                                }

                                var objectTypeId = new Guid(objectTypeIdString);
                                var objectType = this.session.Database.ObjectFactory.GetObjectTypeForType(objectTypeId);

                                var objectIdsString = this.reader.ReadString();
                                var objectIdStringArray = objectIdsString.Split(Serialization.ObjectsSplitterCharArray);

                                foreach (var objectIdString in objectIdStringArray)
                                {
                                    var objectArray = objectIdString.Split(Serialization.ObjectSplitterCharArray);

                                    var objectId = long.Parse(objectArray[0]);
                                    var objectVersion = objectArray.Length > 1 ? long.Parse(objectArray[1]) : Database.IntialVersion; 

                                    if (objectType is IClass)
                                    {
                                        this.session.InsertStrategy((IClass)objectType, objectId, objectVersion);
                                    }
                                    else
                                    {
                                        this.session.MemoryDatabase.OnObjectNotLoaded(objectTypeId, objectId.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Unknown child element <" + this.reader.Name + "> in parent element <" + Serialization.Database + ">");
                        }

                        break;
                    case XmlNodeType.EndElement:
                        if (!this.reader.Name.Equals(Serialization.Database))
                        {
                            throw new Exception("Expected closing element </" + Serialization.Database + ">");
                        }

                        return;
                }
            }
        }

        private void LoadRelations()
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (this.reader.Name.Equals(Serialization.Database))
                        {
                            if (!this.reader.IsEmptyElement)
                            {
                                this.LoadDatabaseRelationTypes();
                            }
                        }
                        else if (reader.Name.Equals(Serialization.Workspace))
                        {
                            throw new Exception("Can not load workspace relations in a database.");
                        }
                        else
                        {
                            throw new Exception("Unknown child element <" + this.reader.Name + "> in parent element <" + Serialization.Relations + ">");
                        }

                        break;

                    case XmlNodeType.EndElement:
                        if (!this.reader.Name.Equals(Serialization.Relations))
                        {
                            throw new Exception("Expected closing element </" + Serialization.Relations + ">");
                        }

                        return;
                }
            }
        }

        private void LoadDatabaseRelationTypes()
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (!this.reader.IsEmptyElement)
                        {
                            if (this.reader.Name.Equals(Serialization.RelationTypeUnit)
                                || this.reader.Name.Equals(Serialization.RelationTypeComposite))
                            {
                                var relationTypeIdString = this.reader.GetAttribute(Serialization.Id);
                                if (string.IsNullOrEmpty(relationTypeIdString))
                                {
                                    throw new Exception("Relation type has no id");
                                }

                                var relationTypeId = new Guid(relationTypeIdString);
                                var relationType = (IRelationType)this.session.Database.MetaPopulation.Find(relationTypeId);

                                if (this.reader.Name.Equals(Serialization.RelationTypeUnit))
                                {
                                    if (relationType == null || relationType.RoleType.ObjectType is IComposite)
                                    {
                                        this.CantLoadUnitRole(relationTypeId);
                                    }
                                    else
                                    {
                                        this.LoadUnitRelations(relationType);
                                    }
                                }
                                else if (this.reader.Name.Equals(Serialization.RelationTypeComposite))
                                {
                                    if (relationType == null || relationType.RoleType.ObjectType is IUnit)
                                    {
                                        this.CantLoadCompositeRole(relationTypeId);
                                    }
                                    else
                                    {
                                        this.LoadCompositeRelations(relationType);
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception(
                                    "Unknown child element <" + this.reader.Name + "> in parent element <"
                                    + Serialization.Database + ">");
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        if (!this.reader.Name.Equals(Serialization.Database))
                        {
                            throw new Exception("Expected closing element </" + Serialization.Database + ">");
                        }

                        return;
                }
            }
        }

        private void LoadUnitRelations(IRelationType relationType)
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (this.reader.Name.Equals(Serialization.Relation))
                        {
                            var associationIdString = this.reader.GetAttribute(Serialization.Association);
                            var associationId = long.Parse(associationIdString);
                            var strategy = this.LoadInstantiateStrategy(associationId);

                            var value = string.Empty;
                            if (!this.reader.IsEmptyElement)
                            {
                                value = this.reader.ReadString();
                            }

                            if (strategy == null)
                            {
                                this.session.MemoryDatabase.OnRelationNotLoaded(relationType.Id, associationIdString, value);
                            }
                            else
                            {
                                try
                                {
                                    this.session.MemoryDatabase.UnitRoleChecks(strategy, relationType.RoleType);
                                    if (this.reader.IsEmptyElement)
                                    {
                                        var unitType = (IUnit)relationType.RoleType.ObjectType;
                                        switch (unitType.UnitTag)
                                        {
                                            case UnitTags.String:
                                                strategy.SetUnitRole(relationType, string.Empty);
                                                break;
                                            case UnitTags.Binary:
                                                strategy.SetUnitRole(relationType, emptyByteArray);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        var unitType = (IUnit)relationType.RoleType.ObjectType;
                                        var unitTypeTag = unitType.UnitTag;

                                        var unit = Serialization.ReadString(value, unitTypeTag);
                                        strategy.SetUnitRole(relationType, unit);
                                    }
                                }
                                catch
                                {
                                    this.session.MemoryDatabase.OnRelationNotLoaded(relationType.Id, associationId.ToString(), value);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Unknown child element <" + this.reader.Name + "> in parent element <" + Serialization.RelationTypeUnit + ">");
                        }

                        break;
                    case XmlNodeType.EndElement:
                        if (!this.reader.Name.Equals(Serialization.RelationTypeUnit))
                        {
                            throw new Exception("Expected closing element </" + Serialization.RelationTypeUnit + ">");
                        }

                        return;
                }
            }
        }

        private void LoadCompositeRelations(IRelationType relationType)
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    // eat everything but elements
                    case XmlNodeType.Element:
                        if (this.reader.Name.Equals(Serialization.Relation))
                        {
                            var associationId = long.Parse(this.reader.GetAttribute(Serialization.Association));
                            var association = this.LoadInstantiateStrategy(associationId);

                            var value = string.Empty;
                            if (!this.reader.IsEmptyElement)
                            {
                                value = this.reader.ReadString();
                            }

                            if (!this.reader.IsEmptyElement)
                            {
                                var roleIdsString = value;
                                var roleIdStringArray = roleIdsString.Split(Serialization.ObjectsSplitterCharArray);

                                if (association == null ||
                                    !this.session.MemoryDatabase.ContainsConcreteClass(relationType.AssociationType.ObjectType, association.UncheckedObjectType) || 
                                    (relationType.RoleType.IsOne && roleIdStringArray.Length != 1))
                                {
                                    foreach (var roleId in roleIdStringArray)
                                    {
                                        this.session.MemoryDatabase.OnRelationNotLoaded(relationType.Id, associationId.ToString(), roleId);
                                    }
                                }
                                else
                                {
                                    if (relationType.RoleType.IsOne)
                                    {
                                        var roleIdString = long.Parse(roleIdStringArray[0]);
                                        var role = this.LoadInstantiateStrategy(roleIdString);
                                        if (role == null || !this.session.MemoryDatabase.ContainsConcreteClass((IComposite)relationType.RoleType.ObjectType, role.UncheckedObjectType))
                                        {
                                            this.session.MemoryDatabase.OnRelationNotLoaded(relationType.Id, associationId.ToString(), roleIdStringArray[0]);
                                        }
                                        else
                                        {
                                            if (relationType.RoleType.AssociationType.IsMany)
                                            {
                                                association.SetCompositeRoleMany2One(relationType.RoleType, role.GetObject());
                                            }
                                            else
                                            {
                                                association.SetCompositeRoleOne2One(relationType.RoleType, role.GetObject());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var roleStrategies = new HashSet<Strategy>();
                                        foreach (var roleIdString in roleIdStringArray)
                                        {
                                            var roleId = long.Parse(roleIdString);
                                            var role = this.LoadInstantiateStrategy(roleId);
                                            if (role == null || !this.session.MemoryDatabase.ContainsConcreteClass((IComposite)relationType.RoleType.ObjectType, role.UncheckedObjectType))
                                            {
                                                this.session.MemoryDatabase.OnRelationNotLoaded(relationType.Id, associationId.ToString(), roleId.ToString());
                                            }
                                            else
                                            {
                                                roleStrategies.Add(role);
                                            }
                                        }

                                        if (relationType.RoleType.AssociationType.IsMany)
                                        {
                                            association.SetCompositeRolesMany2Many(relationType.RoleType, roleStrategies);
                                        }
                                        else
                                        {
                                            association.SetCompositesRoleOne2Many(relationType.RoleType, roleStrategies);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Unknown child element <" + this.reader.Name + "> in parent element <" + Serialization.RelationTypeComposite + ">");
                        }

                        break;
                    case XmlNodeType.EndElement:
                        if (!this.reader.Name.Equals(Serialization.RelationTypeComposite))
                        {
                            throw new Exception("Expected closing element </" + Serialization.RelationTypeComposite + ">");
                        }

                        return;
                }
            }
        }

        private Strategy LoadInstantiateStrategy(long id)
        {
            return this.session.GetStrategy(id);
        }

        private void CantLoadUnitRole(Guid relationTypeId)
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (this.reader.Name.Equals(Serialization.Relation))
                        {
                            var a = this.reader.GetAttribute(Serialization.Association);
                            var value = string.Empty;

                            if (!this.reader.IsEmptyElement)
                            {
                                value = this.reader.ReadString();
                            }

                            this.session.MemoryDatabase.OnRelationNotLoaded(relationTypeId, a, value);
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        private void CantLoadCompositeRole(Guid relationTypeId)
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (this.reader.Name.Equals(Serialization.Relation))
                        {
                            var associationIdString = this.reader.GetAttribute(Serialization.Association);
                            if (string.IsNullOrEmpty(associationIdString))
                            {
                                throw new Exception("Association id is missing");
                            }

                            if (this.reader.IsEmptyElement)
                            {
                                this.session.MemoryDatabase.OnRelationNotLoaded(relationTypeId, associationIdString, null);
                            }
                            else
                            {
                                var value = this.reader.ReadString();
                                var rs = value.Split(Serialization.ObjectsSplitterCharArray);
                                foreach (var r in rs)
                                {
                                    this.session.MemoryDatabase.OnRelationNotLoaded(relationTypeId, associationIdString, r);
                                }
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }
    }
}