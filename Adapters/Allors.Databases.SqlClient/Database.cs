// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Database.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Xml;

    using Allors.Meta;
    using Allors.Populations;

    public class Database : IDatabase
    {
        internal const int CacheDefaultValue = int.MaxValue;

        private static readonly byte[] EmptyByteArray = new byte[0];

        private readonly object lockObject = new object();

        private readonly Guid id;
        private readonly ObjectIds objectIds;
        private readonly IObjectFactory objectFactory;
        private readonly IWorkspaceFactory workspaceFactory;
        private readonly RoleChecks roleChecks;
        private readonly Mapping mapping;

        // Configuration
        private readonly string connectionString;
        private readonly int commandTimeout;
        private readonly string schemaName;
        private readonly IsolationLevel isolationLevel;

        private Dictionary<string, object> properties;

        private SqlConnection connection;
        private SqlTransaction transaction;

        private bool? isValid;

        public Database(Configuration configuration)
        {
            this.id = configuration.Id;

            this.objectIds = configuration.ObjectIds;
            this.objectFactory = configuration.ObjectFactory;
            this.workspaceFactory = configuration.WorkspaceFactory;
            this.roleChecks = new RoleChecks();

            this.connectionString = configuration.ConnectionString;
            this.commandTimeout = configuration.CommandTimeout;
            this.schemaName = configuration.SchemaName;
            this.isolationLevel = configuration.IsolationLevel;
            
            this.mapping = new Mapping(this);
        }

        public event ObjectNotLoadedEventHandler ObjectNotLoaded;

        public event RelationNotLoadedEventHandler RelationNotLoaded;
        
        public Guid Id 
        {
            get
            {
                return this.id;
            }
        }

        public IObjectFactory ObjectFactory 
        {
            get
            {
                return this.objectFactory;
            }
        }

        public bool IsDatabase
        {
            get
            {
                return true;
            }
        }

        public bool IsWorkspace 
        {
            get
            {
                return false;
            }
        }

        public bool IsShared 
        {
            get
            {
                return true;
            }
        }

        public IMetaPopulation MetaPopulation 
        {
            get
            {
                return this.ObjectFactory.MetaPopulation;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        public string SchemaName
        {
            get
            {
                return this.schemaName;
            }
        }

        public int CommandTimeout
        {
            get
            {
                return this.commandTimeout;
            }
        }

        public IsolationLevel IsolationLevel
        {
            get
            {
                return this.isolationLevel;
            }
        }

        public ObjectIds ObjectIds
        {
            get
            {
                return this.objectIds;
            }
        }

        public Mapping Mapping
        {
            get
            {
                return this.mapping;
            }
        }

        public bool IsValid
        {
            get
            {
                if (!this.isValid.HasValue)
                {
                    lock (this.lockObject)
                    {
                        if (!this.isValid.HasValue)
                        {
                            var validate = this.Validate();
                            return validate.Success;
                        }
                    }
                }

                return this.isValid.Value;
            }
        }

        internal RoleChecks RoleChecks
        {
            get
            {
                return this.roleChecks;
            }
        }

        public object this[string name]
        {
            get
            {
                if (this.properties == null)
                {
                    return null;
                }

                object value;
                this.properties.TryGetValue(name, out value);
                return value;
            }

            set
            {
                if (this.properties == null)
                {
                    this.properties = new Dictionary<string, object>();
                }

                if (value == null)
                {
                    this.properties.Remove(name);
                }
                else
                {
                    this.properties[name] = value;
                }
            }
        }

        public void Init()
        {
            this.Mapping.Init();

            this.properties = null;
        }
        
        public IWorkspace CreateWorkspace()
        {
            if (this.workspaceFactory == null)
            {
                throw new Exception("No workspacefactory defined");
            }

            return this.workspaceFactory.CreateWorkspace(this);
        }

        IDatabaseSession IDatabase.CreateSession()
        {
            return this.CreateSession();
        }

        ISession IPopulation.CreateSession()
        {
            return this.CreateSession();
        }

        public DatabaseSession CreateSession()
        {
            if (!this.IsValid)
            {
                throw new Exception("Schema is invalid.");
            }

            return new DatabaseSession(this);
        }

        public void Load(XmlReader reader)
        {
            this.Init();

            this.LoadAllors(reader);
        }

        public void Save(XmlWriter writer)
        {
            this.SaveAllors(writer);
        }

        public Validation Validate()
        {
            var validateResult = new Validation(this);
            this.isValid = validateResult.Success;
            return validateResult;
        }

        #region Serialization
        private void LoadAllors(XmlReader reader)
        {
            try
            {
                while (reader.Read())
                {
                    // only process elements, ignore others
                    if (reader.NodeType.Equals(XmlNodeType.Element))
                    {
                        if (reader.Name.Equals(Serialization.Population))
                        {
                            Serialization.CheckVersion(reader);

                            if (!reader.IsEmptyElement)
                            {
                                this.LoadPopulation(reader);
                            }

                            return;
                        }
                    }
                }
            }
            finally
            {
                this.Commit();
            }
        }

        private void LoadPopulation(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Objects))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                using (var command = this.CreateCommand("SET IDENTITY_INSERT " + this.SchemaName + "." + Mapping.TableNameForObjects + " ON;"))
                                {
                                    command.ExecuteNonQuery();
                                }

                                try
                                {
                                    this.LoadObjectsX(reader);
                                }
                                finally
                                {
                                    using (var command = this.CreateCommand("SET IDENTITY_INSERT " + this.SchemaName + "." + Mapping.TableNameForObjects + " OFF;"))
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        else if (reader.Name.Equals(Serialization.Relations))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadRelationTypes(reader);
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        private void LoadObjectsX(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Database))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadObjectTypes(reader);
                            }
                        }
                        else if (reader.Name.Equals(Serialization.Workspace))
                        {
                            throw new Exception("Can not load workspace objects in a database.");
                        }
                        else
                        {
                            throw new Exception("Unknown child element <" + reader.Name + "> in parent element <" + Serialization.Objects + ">");
                        }

                        break;
                    case XmlNodeType.EndElement:
                        if (!reader.Name.Equals(Serialization.Objects))
                        {
                            throw new Exception("Expected closing element </" + Serialization.Objects + ">");
                        }

                        return;
                }
            }
        }

        private void LoadObjectTypes(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    // only process elements, ignore others
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.ObjectType))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadObjects(reader);
                            }
                        }
                        else
                        {
                            throw new Exception("Unknown child element <" + reader.Name + "> in parent element <" + Serialization.Database + ">");
                        }

                        break;
                    case XmlNodeType.EndElement:
                        if (!reader.Name.Equals(Serialization.Database))
                        {
                            throw new Exception("Expected closing element </" + Serialization.Database + ">");
                        }

                        return;
                }
            }
        }

        private void LoadObjects(XmlReader reader)
        {
            var metaObjectIdString = reader.GetAttribute(Serialization.Id);
            var metaObjectId = new Guid(metaObjectIdString);

            var objectType = (IObjectType)this.ObjectFactory.MetaPopulation.Find(metaObjectId);

            if (!reader.IsEmptyElement)
            {
                var oidsString = reader.ReadString();
                var oids = oidsString.Split(Serialization.ObjectsSplitterCharArray);

                for (var i = 0; i < oids.Length; i++)
                {
                    var objectId = ObjectIds.Parse(oids[i]);

                    if (!(objectType is IClass))
                    {
                        this.OnObjectNotLoaded(metaObjectId, objectId.ToString());
                    }
                    else
                    {
                        // Objects
                        var cmdText = @"
INSERT INTO " + this.SchemaName + "." + Mapping.TableNameForObjects + " (" + Mapping.ColumnNameForObject + "," + Mapping.ColumnNameForType + "," + Mapping.ColumnNameForCache + @")
VALUES (" + Mapping.ParameterNameForObject + "," + Mapping.ParameterNameForType + "," + Mapping.ParameterNameForCache + ")";

                        using (var command = this.CreateCommand(cmdText))
                        {
                            command.Parameters.Add(Mapping.ParameterNameForObject, Mapping.SqlDbTypeForObject).Value = objectId.Value;
                            command.Parameters.Add(Mapping.ParameterNameForType, Mapping.SqlDbTypeForType).Value = objectType.Id;
                            command.Parameters.Add(Mapping.ParameterNameForCache, Mapping.SqlDbTypeForCache).Value = CacheDefaultValue;

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void LoadCompositeRelations(XmlReader reader, IRelationType relationType)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Relation))
                        {
                            var associationString = reader.GetAttribute(Serialization.Association);
                            var association = ObjectIds.Parse(associationString);

                            if (reader.IsEmptyElement)
                            {
                                this.OnRelationNotLoaded(relationType.Id, association.ToString(), null);
                            }
                            else
                            {
                                var value = reader.ReadString();
                                var roleStrings = value.Split(Serialization.ObjectsSplitterCharArray);

                                if (relationType.RoleType.IsOne && roleStrings.Length > 1)
                                {
                                    foreach (var roleString in roleStrings)
                                    {
                                        this.OnRelationNotLoaded(relationType.Id, association.ToString(), roleString);
                                    }
                                }

                                foreach (var roleString in roleStrings)
                                {
                                    var role = ObjectIds.Parse(roleString);
                                    //this.OnRelationNotLoaded(relationType.Id, association.ToString(), r);

                                    var cmdText = @"
INSERT INTO " + this.SchemaName + "." + this.Mapping.GetTableName(relationType) + " (" + Mapping.ColumnNameForAssociation + "," + Mapping.ColumnNameForRole + @")
VALUES (" + Mapping.ParameterNameForAssociation + "," + Mapping.ParameterNameForRole + ")";

                                    using (var command = this.CreateCommand(cmdText))
                                    {
                                        command.Parameters.Add(Mapping.ParameterNameForAssociation, Mapping.SqlDbTypeForObject).Value = association.Value;
                                        command.Parameters.Add(Mapping.ParameterNameForRole, Mapping.SqlDbTypeForObject).Value = role.Value;

                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        private void LoadRelationTypes(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.RelationTypeUnit))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                this.LoadRelations(reader, true);
                            }
                        }
                        else if (reader.Name.Equals(Serialization.RelationTypeComposite))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                LoadRelations(reader, false);
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        private void LoadRelations(XmlReader reader, bool isUnit)
        {
            var metaRelationIdString = reader.GetAttribute(Serialization.Id);
            var metaRelationId = new Guid(metaRelationIdString);

            var relationType = (IRelationType)this.ObjectFactory.MetaPopulation.Find(metaRelationId);

            if (!reader.IsEmptyElement)
            {
                if (relationType == null || (relationType.RoleType.ObjectType is IUnit) != isUnit)
                {
                    this.CantLoadRelation(reader, metaRelationId);
                }
                else
                {
                    if (relationType.RoleType.ObjectType is IUnit)
                    {
                        this.LoadUnitRelations(reader, relationType);
                    }
                    else
                    {
                        this.LoadCompositeRelations(reader, relationType);
                    }
                }
            }
        }

        private void LoadUnitRelations(XmlReader reader, IRelationType relationType)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Relation))
                        {
                            var associationString = reader.GetAttribute(Serialization.Association);
                            var association = ObjectIds.Parse(associationString);
                            if (reader.IsEmptyElement)
                            {
                                object role;

                                // OnRelationNotLoaded(relationType.Id, association.ToString(), String.Empty);
                                if (relationType.RoleType.ObjectType.Id == UnitIds.StringId)
                                {
                                    role = string.Empty;
                                }
                                else if (relationType.RoleType.ObjectType.Id == UnitIds.BinaryId)
                                {
                                    role = EmptyByteArray;
                                }
                                else
                                {
                                    this.OnRelationNotLoaded(relationType.Id, association.ToString(), string.Empty);
                                    continue;
                                }

                                var cmdText = @"
INSERT INTO " + this.SchemaName + "." + this.Mapping.GetTableName(relationType) + " (" + Mapping.ColumnNameForAssociation + "," + Mapping.ColumnNameForRole + @")
VALUES (" + Mapping.ParameterNameForAssociation + "," + Mapping.ParameterNameForRole + ")";

                                using (var command = this.CreateCommand(cmdText))
                                {
                                    command.Parameters.Add(Mapping.ParameterNameForAssociation, Mapping.SqlDbTypeForObject).Value = association.Value;
                                    command.Parameters.Add(Mapping.ParameterNameForRole, this.Mapping.GetSqlDbType(relationType.RoleType)).Value = role;

                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                var value = reader.ReadString();
                                try
                                {
                                    // OnRelationNotLoaded(relationType.Id, association.ToString(), value);
                                    var unitTypeTag = ((IUnit)relationType.RoleType.ObjectType).UnitTag;
                                    var role = Serialization.ReadString(value, unitTypeTag);

                                    var cmdText = @"
INSERT INTO " + this.SchemaName + "." + this.Mapping.GetTableName(relationType) + " (" + Mapping.ColumnNameForAssociation + "," + Mapping.ColumnNameForRole + @")
VALUES (" + Mapping.ParameterNameForAssociation + "," + Mapping.ParameterNameForRole + ")";

                                    using (var command = this.CreateCommand(cmdText))
                                    {
                                        command.Parameters.Add(Mapping.ParameterNameForAssociation, Mapping.SqlDbTypeForObject).Value = association.Value;
                                        command.Parameters.Add(Mapping.ParameterNameForRole, this.Mapping.GetSqlDbType(relationType.RoleType)).Value = role;

                                        command.ExecuteNonQuery();
                                    }
                                }
                                catch
                                {
                                    this.OnRelationNotLoaded(relationType.Id, association.ToString(), value);
                                }
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        private void OnObjectNotLoaded(Guid metaTypeId, string allorsObjectId)
        {
            if (this.ObjectNotLoaded != null)
            {
                this.ObjectNotLoaded(this, new ObjectNotLoadedEventArgs(metaTypeId, allorsObjectId));
            }
            else
            {
                throw new Exception("Object not loaded: " + metaTypeId + ":" + allorsObjectId);
            }
        }

        private void OnRelationNotLoaded(Guid metaRelationId, string associationObjectId, string roleContents)
        {
            var args = new RelationNotLoadedEventArgs(metaRelationId, associationObjectId, roleContents);
            if (this.RelationNotLoaded != null)
            {
                this.RelationNotLoaded(this, args);
            }
            else
            {
                throw new Exception("RelationType not loaded: " + args);
            }
        }

        private void CantLoadRelation(XmlReader reader, Guid metaRelationId)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Relation))
                        {
                            var a = reader.GetAttribute(Serialization.Association);
                            var value = string.Empty;

                            if (!reader.IsEmptyElement)
                            {
                                value = reader.ReadString();
                            }

                            this.OnRelationNotLoaded(metaRelationId, a, value);
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }

        private void SaveAllors(XmlWriter writer)
        {
            try
            {
                var writeDocument = false;
                if (writer.WriteState == WriteState.Start)
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement(Serialization.Allors);
                    writeDocument = true;
                }

                writer.WriteStartElement(Serialization.Population);
                writer.WriteAttributeString(Serialization.Version, Serialization.VersionCurrent);

                writer.WriteStartElement(Serialization.Objects);
                writer.WriteStartElement(Serialization.Database);
                SaveObjects(writer);
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteStartElement(Serialization.Relations);
                writer.WriteStartElement(Serialization.Database);
                SaveRelations(writer);
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndElement();

                if (writeDocument)
                {
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            finally
            {
                this.Rollback();
            }
        }

        private void SaveObjects(XmlWriter writer)
        {
            var orderedClasses = new List<IClass>(this.ObjectFactory.MetaPopulation.Classes);
            orderedClasses.Sort(MetaObjectComparer.ById);
            foreach (var type in orderedClasses)
            {
                var atLeastOne = false;

                var cmdText = @"
SELECT " + Mapping.ColumnNameForObject + @"
FROM " + this.SchemaName + "." + Mapping.TableNameForObjects + @"
WHERE " + Mapping.ColumnNameForType + "=" + Mapping.ParameterNameForType;

                using (var command = this.CreateCommand(cmdText))
                {
                    command.Parameters.Add(Mapping.ParameterNameForType, Mapping.SqlDbTypeForType).Value = type.Id;

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (atLeastOne == false)
                            {
                                atLeastOne = true;

                                writer.WriteStartElement(Serialization.ObjectType);
                                writer.WriteAttributeString(Serialization.Id, type.IdAsString);
                            }
                            else
                            {
                                writer.WriteString(Serialization.ObjectsSplitter);
                            }

                            var role = reader[0].ToString();
                            writer.WriteString(role);
                        }

                        reader.Close();
                    }
                }

                if (atLeastOne)
                {
                    writer.WriteEndElement();
                }
            }
        }

        private void SaveRelations(XmlWriter writer)
        {
            var orderedRelationType = new List<IRelationType>(this.ObjectFactory.MetaPopulation.RelationTypes);
            orderedRelationType.Sort(MetaObjectComparer.ById);
            foreach (var relation in orderedRelationType)
            {
                if (relation.AssociationType.ObjectType.ExistLeafClasses)
                {
                    var role = relation.RoleType;
                    
                    var cmdText = @"
SELECT " + Mapping.ColumnNameForAssociation + "," + Mapping.ColumnNameForRole + @"
FROM " + this.SchemaName + "." + this.Mapping.GetTableName(relation) + @"
ORDER BY " + Mapping.ColumnNameForAssociation + "," + Mapping.ColumnNameForRole;

                    using (var command = this.CreateCommand(cmdText))
                    {
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            if (role.IsMany)
                            {
                                using (var relationTypeManyXmlWriter = new RelationTypeManyXmlWriter(relation, writer))
                                {
                                    while (reader.Read())
                                    {
                                        var a = long.Parse(reader.GetValue(0).ToString());
                                        var r = long.Parse(reader.GetValue(1).ToString());
                                        relationTypeManyXmlWriter.Write(a, r);
                                    }

                                    relationTypeManyXmlWriter.Close();
                                }
                            }
                            else
                            {
                                using (var relationTypeOneXmlWriter = new RelationTypeOneXmlWriter(relation, writer))
                                {
                                    while (reader.Read())
                                    {
                                        var a = long.Parse(reader.GetValue(0).ToString());

                                        if (role.ObjectType is IUnit)
                                        {
                                            var unitTypeTag = ((IUnit)role.ObjectType).UnitTag;
                                            var r = reader.GetValue(1);
                                            var content = Serialization.WriteString(unitTypeTag, r);
                                            relationTypeOneXmlWriter.Write(a, content);
                                        }
                                        else
                                        {
                                            var r = reader.GetValue(1);
                                            relationTypeOneXmlWriter.Write(a, XmlConvert.ToString(long.Parse(r.ToString())));
                                        }
                                    }

                                    relationTypeOneXmlWriter.Close();
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        private SqlCommand CreateCommand(string cmdText)
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.ConnectionString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction(this.IsolationLevel);
            }

            var command = new SqlCommand(cmdText, this.connection, this.transaction)
                              {
                                  CommandTimeout = this.CommandTimeout
                              };
            return command;
        }

        private void Commit()
        {
            try
            {
                if (this.transaction != null)
                {
                    this.transaction.Commit();
                }
            }
            finally
            {
                this.transaction = null;
                if (this.connection != null)
                {
                    try
                    {
                        this.connection.Close();
                    }
                    finally
                    {
                        this.connection = null;
                    }
                }
            }
        }

        private void Rollback()
        {
            try
            {
                if (this.transaction != null)
                {
                    this.transaction.Rollback();
                }
            }
            finally
            {
                this.transaction = null;
                if (this.connection != null)
                {
                    try
                    {
                        this.connection.Close();
                    }
                    finally
                    {
                        this.connection = null;
                    }
                }
            }
        }
    }
}