// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Load.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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

namespace Allors.Adapters.Object.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Xml;

    using Adapters;

    using Allors.Meta;

    internal class Load
    {
        private static readonly byte[] EmptyByteArray = new byte[0];

        private readonly Database database;
        private readonly ObjectNotLoadedEventHandler objectNotLoaded;
        private readonly RelationNotLoadedEventHandler relationNotLoaded;
        private readonly XmlReader reader;

        private readonly Dictionary<ObjectId, IObjectType> objectTypeByObjectId;
        private readonly Dictionary<ObjectId, ObjectVersion> objectVersionByObjectId;

        internal void Execute(ManagementSession session)
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
                            this.LoadPopulation(session);
                        }

                        return;
                    }
                }
            }
        }

        protected virtual void LoadPopulation(ManagementSession session)
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    // only process elements, ignore others
                    case XmlNodeType.Element:
                        if (this.reader.Name.Equals(Serialization.Objects))
                        {
                            if (!this.reader.IsEmptyElement)
                            {
                                this.LoadObjects(session);
                            }

                            this.LoadObjectsPostProcess(session);
                            this.LoadObjectsSetCache(session);

                            session.Commit();
                        }
                        else if (this.reader.Name.Equals(Serialization.Relations))
                        {
                            if (!this.reader.IsEmptyElement)
                            {
                                this.LoadRelations(session);
                            }

                            session.Commit();
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

        protected virtual void LoadObjects(ManagementSession session)
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
                                this.LoadDatabaseObjectTypes(session);
                            }
                        }
                        else if (reader.Name.Equals(Serialization.Workspace))
                        {
                            throw new Exception("Can not load workspace objects in a Database.");
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

        protected virtual void LoadDatabaseObjectTypes(ManagementSession session)
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    // only process elements, ignore others
                    case XmlNodeType.Element:
                        if (this.reader.Name.Equals(Serialization.ObjectType))
                        {
                            if (!this.reader.IsEmptyElement)
                            {
                                var objectTypeIdString = this.reader.GetAttribute(Serialization.Id);
                                if (string.IsNullOrEmpty(objectTypeIdString))
                                {
                                    throw new Exception("Object type id is missing");
                                }

                                var objectTypeId = new Guid(objectTypeIdString);
                                var objectType = this.database.ObjectFactory.GetObjectTypeForType(objectTypeId);

                                var canLoad = objectType != null && objectType.IsClass;

                                var objectIdsString = this.reader.ReadString();
                                var objectIdStringArray = objectIdsString.Split(Serialization.ObjectsSplitterCharArray);

                                var objectIds = new ObjectId[objectIdStringArray.Length];
                                var versions = new ObjectVersion[objectIdStringArray.Length];
                                for (var i = 0; i < objectIds.Length; i++)
                                {
                                    var objectIdString = objectIdStringArray[i];

                                    if (canLoad)
                                    {
                                        var objectArray = objectIdString.Split(Serialization.ObjectSplitterCharArray);

                                        var objectId = this.database.ObjectIds.Parse(objectArray[0]);
                                        var version = objectArray.Length > 1 ? new ObjectVersionLong(objectArray[1]) : new ObjectVersionLong(Reference.InitialVersion);

                                        objectIds[i] = objectId;
                                        versions[i] = version;

                                        this.objectTypeByObjectId[objectId] = objectType;
                                        this.objectVersionByObjectId[objectId] = version;
                                    }
                                    else
                                    {
                                        this.OnObjectNotLoaded(objectTypeId, objectIdString);
                                    }
                                }

                                if (canLoad)
                                {
                                    session.LoadObjects(objectType, objectIds);
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

        protected virtual void LoadRelations(ManagementSession session)
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
                                this.LoadDatabaseRelationTypes(session);
                            }
                        }
                        else if (this.reader.Name.Equals(Serialization.Workspace))
                        {
                            throw new Exception("Can not load workspace relations in a Database.");
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

        protected virtual void LoadDatabaseRelationTypes(ManagementSession session)
        {
            while (this.reader.Read())
            {
                switch (this.reader.NodeType)
                {
                    // only process elements, ignore others
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
                                var relationType = (IRelationType)this.database.MetaPopulation.Find(relationTypeId);

                                if (this.reader.Name.Equals(Serialization.RelationTypeUnit))
                                {
                                    if (relationType == null || relationType.RoleType.ObjectType.IsComposite)
                                    {
                                        this.CantLoadUnitRole(relationTypeId);
                                    }
                                    else
                                    {
                                        var relationsByExclusiveRootClass = new Dictionary<IObjectType, List<UnitRelation>>();
                                        this.LoadUnitRelations(relationType, relationsByExclusiveRootClass);

                                        foreach (var dictionaryEntry in relationsByExclusiveRootClass)
                                        {
                                            var exclusiveRootClass = dictionaryEntry.Key;
                                            var relations = dictionaryEntry.Value;
                                            session.LoadUnitRelations(relations, exclusiveRootClass, relationType.RoleType);
                                        }
                                    }
                                }
                                else if (this.reader.Name.Equals(Serialization.RelationTypeComposite))
                                {
                                    if (relationType == null || relationType.RoleType.ObjectType.IsUnit)
                                    {
                                        this.CantLoadCompositeRole(relationTypeId);
                                    }
                                    else
                                    {
                                        var relations = new List<CompositeRelation>();
                                        this.LoadCompositeRelations(relationType, relations);
                                        if (relations.Count > 0)
                                        {
                                            session.LoadCompositeRelations(relationType.RoleType, relations);
                                        }
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

        protected void LoadUnitRelations(IRelationType relationType, Dictionary<IObjectType, List<UnitRelation>> relationsByExclusiveRootClass)
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

                            var associationId = this.database.ObjectIds.Parse(associationIdString);
                            IObjectType associationConcreteClass;
                            this.objectTypeByObjectId.TryGetValue(associationId, out associationConcreteClass);

                            if (this.reader.IsEmptyElement)
                            {
                                if (associationConcreteClass == null ||
                                    !this.database.ContainsConcreteClass(relationType.AssociationType.ObjectType, associationConcreteClass))
                                {
                                    this.OnRelationNotLoaded(relationType.Id, associationIdString, string.Empty);
                                }
                                else
                                {
                                    var exclusiveRootClass = associationConcreteClass;
                                    switch (((IUnit)relationType.RoleType.ObjectType).UnitTag)
                                    {
                                        case UnitTags.AllorsString:
                                            {
                                                List<UnitRelation> relations;
                                                if (
                                                    !relationsByExclusiveRootClass.TryGetValue(associationConcreteClass, out relations))
                                                {
                                                    relations = new List<UnitRelation>();
                                                    relationsByExclusiveRootClass[exclusiveRootClass] = relations;
                                                }

                                                var unitRelation = new UnitRelation(associationId, string.Empty);
                                                relations.Add(unitRelation);
                                            }

                                            break;

                                        case UnitTags.AllorsBinary:
                                            {
                                                List<UnitRelation> relations;
                                                if (!relationsByExclusiveRootClass.TryGetValue(associationConcreteClass, out relations))
                                                {
                                                    relations = new List<UnitRelation>();
                                                    relationsByExclusiveRootClass[exclusiveRootClass] = relations;
                                                }

                                                var unitRelation = new UnitRelation(associationId, EmptyByteArray);
                                                relations.Add(unitRelation);
                                            }

                                            break;

                                        default:
                                            this.OnRelationNotLoaded(relationType.Id, associationIdString, string.Empty);
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                var value = this.reader.ReadString();
                                if (associationConcreteClass == null ||
                                   !this.database.ContainsConcreteClass(relationType.AssociationType.ObjectType, associationConcreteClass))
                                {
                                    this.OnRelationNotLoaded(relationType.Id, associationIdString, value);
                                }
                                else
                                {
                                    try
                                    {
                                        var exclusiveRootClass = (IComposite)associationConcreteClass;
                                        var unitTypeTag = ((IUnit)relationType.RoleType.ObjectType).UnitTag;
                                        var unit = Serialization.ReadString(value, unitTypeTag);

                                        List<UnitRelation> relations;
                                        if (!relationsByExclusiveRootClass.TryGetValue(associationConcreteClass, out relations))
                                        {
                                            relations = new List<UnitRelation>();
                                            relationsByExclusiveRootClass[exclusiveRootClass] = relations;
                                        }

                                        var unitRelation = new UnitRelation(associationId, unit);
                                        relations.Add(unitRelation);
                                    }
                                    catch
                                    {
                                        this.OnRelationNotLoaded(relationType.Id, associationIdString, value);
                                    }
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

        protected void LoadCompositeRelations(IRelationType relationType, List<CompositeRelation> relations)
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
                                throw new Exception("Role is missing");
                            }

                            var association = this.database.ObjectIds.Parse(associationIdString);
                            IObjectType associationConcreteClass;
                            this.objectTypeByObjectId.TryGetValue(association, out associationConcreteClass);

                            var value = this.reader.ReadString();
                            var rs = value.Split(Serialization.ObjectsSplitterCharArray);

                            if (associationConcreteClass == null ||
                                !this.database.ContainsConcreteClass(relationType.AssociationType.ObjectType, associationConcreteClass) ||
                                (relationType.RoleType.IsOne && rs.Length > 1))
                            {
                                foreach (var r in rs)
                                {
                                    this.OnRelationNotLoaded(relationType.Id, associationIdString, r);
                                }
                            }
                            else
                            {
                                foreach (var r in rs)
                                {
                                    var role = this.database.ObjectIds.Parse(r);
                                    IObjectType roleConcreteClass;
                                    this.objectTypeByObjectId.TryGetValue(role, out roleConcreteClass);

                                    if (roleConcreteClass == null ||
                                        !this.database.ContainsConcreteClass(relationType.RoleType.ObjectType, roleConcreteClass))
                                    {
                                        this.OnRelationNotLoaded(relationType.Id, associationIdString, r);
                                    }
                                    else
                                    {
                                        relations.Add(new CompositeRelation(association, role));
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

                            this.OnRelationNotLoaded(relationTypeId, a, value);
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
                                this.OnRelationNotLoaded(relationTypeId, associationIdString, null);
                            }
                            else
                            {
                                var value = this.reader.ReadString();
                                var rs = value.Split(Serialization.ObjectsSplitterCharArray);
                                foreach (var r in rs)
                                {
                                    this.OnRelationNotLoaded(relationTypeId, associationIdString, r);
                                }
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        private void OnObjectNotLoaded(Guid objectTypeId, string allorsObjectId)
        {
            if (this.objectNotLoaded != null)
            {
                this.objectNotLoaded(this, new ObjectNotLoadedEventArgs(objectTypeId, allorsObjectId));
            }
            else
            {
                throw new Exception("Object not loaded: " + objectTypeId + ":" + allorsObjectId);
            }
        }

        private void OnRelationNotLoaded(Guid relationTypeId, string associationObjectId, string roleContents)
        {
            var args = new RelationNotLoadedEventArgs(relationTypeId, associationObjectId, roleContents);
            if (this.relationNotLoaded != null)
            {
                this.relationNotLoaded(this, args);
            }
            else
            {
                throw new Exception("Role not loaded: " + args);
            }
        }

        internal Load(Database database, ObjectNotLoadedEventHandler objectNotLoaded, RelationNotLoadedEventHandler relationNotLoaded, XmlReader reader)
        {
            this.database = database;
            this.objectNotLoaded = objectNotLoaded;
            this.relationNotLoaded = relationNotLoaded;
            this.reader = reader;

            this.objectTypeByObjectId = new Dictionary<ObjectId, IObjectType>();
            this.objectVersionByObjectId = new Dictionary<ObjectId, ObjectVersion>();
        }

        private void LoadObjectsPostProcess(ManagementSession session)
        {
            var sql = new StringBuilder();

            sql.Append("SET IDENTITY_INSERT " + this.database.Mapping.TableNameForObjects + " ON");
            lock (this)
            {
                var command = session.Connection.CreateCommand();
                command.CommandText = sql.ToString();
                using (command)
                {
                    command.ExecuteNonQuery();
                }
            }

            foreach (var type in this.database.MetaPopulation.Composites)
            {
                if (type.IsClass)
                {
                    sql = new StringBuilder();
                    sql.Append("INSERT INTO " + this.database.Mapping.TableNameForObjects + " (" + Mapping.ColumnNameForObject + "," + Mapping.ColumnNameForClass + "," + Mapping.ColumnNameForVersion + ")\n");
                    sql.Append("SELECT " + Mapping.ColumnNameForObject + "," + Mapping.ColumnNameForClass + ", " + Reference.InitialVersion + "\n");
                    sql.Append("FROM " + this.database.Mapping.TableNameForObjectByClass[type.ExclusiveClass]);

                    lock (this)
                    {
                        var command = session.Connection.CreateCommand();
                        command.CommandText = sql.ToString();
                        using (command)
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            sql = new StringBuilder();
            sql.Append("SET IDENTITY_INSERT " + this.database.Mapping.TableNameForObjects + " OFF");
            lock (this)
            {
                var command = session.Connection.CreateCommand();
                command.CommandText = sql.ToString();
                using (command)
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void LoadObjectsSetCache(ManagementSession session)
        {
            var sql = this.database.Mapping.ProcedureNameForSetVersion;
            var command = session.Connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.StoredProcedure;

            var sqlParameter = command.CreateParameter();
            sqlParameter.SqlDbType = SqlDbType.Structured;
            sqlParameter.TypeName = this.database.Mapping.TableTypeNameForVersionedObject;
            sqlParameter.ParameterName = Mapping.ParamNameForTableType;
            sqlParameter.Value = this.database.CreateVersionedObjectTable(this.objectVersionByObjectId);

            command.Parameters.Add(sqlParameter);

            command.ExecuteNonQuery();
        }
    }
}