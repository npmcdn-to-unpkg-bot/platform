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
    using System.Text;
    using System.Xml;

    using Allors.Meta;

    public class Database : IDatabase
    {
        private readonly Guid id;
        private readonly ObjectIds objectIds;
        private readonly IObjectFactory objectFactory;
        private readonly IWorkspaceFactory workspaceFactory;
        private readonly string connectionString;
        private readonly int commandTimeout;
        private readonly IsolationLevel isolationLevel;
        private readonly Schema schema;
        private readonly RoleChecks roleChecks;

        private Dictionary<string, object> properties;

        public Database(Configuration configuration)
        {
            this.id = configuration.Id;

            this.objectIds = configuration.ObjectIds;
            this.objectFactory = configuration.ObjectFactory;
            this.workspaceFactory = configuration.WorkspaceFactory;
            this.connectionString = configuration.ConnectionString;
            this.commandTimeout = configuration.CommandTimeout;
            this.isolationLevel = configuration.IsolationLevel;
            this.roleChecks = new RoleChecks();

            this.schema = new Schema(this.MetaPopulation, this.ConnectionString, this.ObjectIds);
        }

        public event SessionCreatedEventHandler SessionCreated;

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

        public Schema Schema
        {
            get
            {
                return this.schema;
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
            this.Schema.Init();
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
            return new DatabaseSession(this);
        }

        public void Load(XmlReader reader)
        {
            this.Init();

            this.LoadPreProcess();

            this.LoadAllors(reader);
        }

        public void Save(XmlWriter writer)
        {
            this.SaveAllors(writer);
        }

        #region Serialization
        private void LoadAllors(XmlReader reader)
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
                                this.LoadMetaTypes(reader);
                            }

                            this.LoadObjectsPostProcess();
                        }
                        else if (reader.Name.Equals(Serialization.Relations))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                LoadMetaRelations(reader);
                            }
                        }

                        break;
                    case XmlNodeType.EndElement:
                        return;
                }
            }
        }


        private void LoadCompositeRelations(XmlReader reader, IRelationType relationType)
        {
            LoadComposites loadComposites = LoadCompositesFactory.Create(relationType);

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Relation))
                        {
                            var associationStringValue = reader.GetAttribute(Serialization.Association);
                            var association = int.Parse(associationStringValue);

                            if (reader.IsEmptyElement)
                            {
                                this.OnRelationNotLoaded(relationType.Id, association.ToString(), null);
                            }
                            else
                            {
                                var value = reader.ReadString();
                                var rs = value.Split(Serialization.ObjectsSplitterCharArray);

                                if ((relationType.RoleType.IsOne && rs.Length > 1))
                                {
                                    foreach (string r in rs)
                                    {
                                        this.OnRelationNotLoaded(relationType.Id, association.ToString(), r);
                                    }
                                }

                                foreach (string r in rs)
                                {
                                    if (!loadComposites.Execute(association, Int32.Parse(r)))
                                    {
                                        this.OnRelationNotLoaded(relationType.Id, association.ToString(), r);
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

        private void LoadMetaRelations(XmlReader reader)
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
                                LoadRelations(reader, true);
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
                    default:
                        // eat everything but elements
                        break;
                }
            }
        }

        private void LoadMetaTypes(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.ObjectType))
                        {
                            if (!reader.IsEmptyElement)
                            {
                                LoadObjects(reader);
                            }
                        }
                        break;
                    case XmlNodeType.EndElement:
                        return;
                    default:
                        // eat everything but elements
                        break;
                }
            }
        }

        private void LoadObjects(XmlReader reader)
        {
            string metaObjectIdString = new Guid(reader.GetAttribute(Serialization.Id)).ToString();
            Guid metaObjectId = new Guid(metaObjectIdString);

            ObjectType type = Domain.FindObjectType(metaObjectId);

            if (!reader.IsEmptyElement)
            {
                string oidsString = reader.ReadString();
                string[] oids = oidsString.Split(Serialization.ObjectsSplitterCharArray);

                for (int i = 0; i < oids.Length; i++)
                {
                    ObjectId objectId = allorsObjectIds.Parse(oids[i]);

                    if (type == null || !type.IsConcreteComposite)
                    {
                        OnObjectNotLoaded(metaObjectId, objectId.ToString());
                    }
                    else
                    {
                        string sql = "INSERT INTO " + Schema.OC + " (" + Schema.O + "," + Schema.C + ")\n";
                        sql += "VALUES (" + Schema.O.Param + "," + Schema.C.Param + ")";

                        using (AllorsCommand command = CreateCommand(sql))
                        {
                            command.AddInParameter(Schema.C.Param.Name, type.Id);
                            command.AddInParameter(Schema.O.Param.Name, objectId.Value);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void LoadObjectsPostProcess()
        {
        }

        private void LoadPreProcess()
        {
            DropTable(Schema.AC);
            DropTable(Schema.RC);
            CreateTable(Schema.AC);
            CreateTable(Schema.RC);

            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO " + Schema.AC + " (" + Schema.A + "," + Schema.C + ")\n");
            sql.Append("VALUES (" + Schema.A.Param + "," + Schema.C.Param + ")");
            using (AllorsCommand command = CreateCommand(sql.ToString()))
            {
                command.AddInParameter(Schema.ACA.Param, DBNull.Value);
                command.AddInParameter(Schema.ACC.Param, DBNull.Value);

                foreach (RelationType relation in Domain.RelationTypes)
                {
                    foreach (ObjectType concreteClass in relation.AssociationType.ObjectType.ConcreteClasses)
                    {
                        command.SetParameterValue(Schema.ACA.Param, relation.Id);
                        command.SetParameterValue(Schema.ACC.Param, concreteClass.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }

            sql = new StringBuilder();
            sql.Append("INSERT INTO " + Schema.RC.StatementName + " (" + Schema.R + "," + Schema.C + ")\n");
            sql.Append("VALUES (" + Schema.R.Param + "," + Schema.C.Param + ")");
            using (AllorsCommand command = CreateCommand(sql.ToString()))
            {
                command.AddInParameter(Schema.RCR.Param, DBNull.Value);
                command.AddInParameter(Schema.RCC.Param, DBNull.Value);

                foreach (RelationType relation in Domain.RelationTypes)
                {
                    if (relation.RoleType.ObjectType.IsComposite)
                    {
                        foreach (ObjectType concreteClass in relation.RoleType.ObjectType.ConcreteClasses)
                        {
                            command.SetParameterValue(Schema.RCR.Param, relation.Id);
                            command.SetParameterValue(Schema.RCC.Param, concreteClass.Id);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void LoadRelations(XmlReader reader, bool isUnit)
        {
            string metaRelationIdString = reader.GetAttribute(Serialization.Id);
            Guid metaRelationId = new Guid(metaRelationIdString);

            RelationType relationType = Domain.FindRelationType(metaRelationId);

            if (!reader.IsEmptyElement)
            {
                if (relationType == null || relationType.RoleType.ObjectType.IsUnit != isUnit)
                {
                    CantLoadRelation(reader, metaRelationId);
                }
                else
                {
                    if (relationType.RoleType.ObjectType.IsUnit)
                    {
                        LoadUnitRelations(reader, relationType);
                    }
                    else
                    {
                        LoadCompositeRelations(reader, relationType);
                    }
                }
            }
        }

        private void LoadUnitRelations(XmlReader reader, IRelationType relationType)
        {
            LoadUnits loadUnits = LoadUnitsFactory.Create(relationType);

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Serialization.Relation))
                        {
                            int a = Int32.Parse(reader.GetAttribute(Serialization.Association));
                            if (reader.IsEmptyElement)
                            {
                                if (relationType.RoleType.ObjectType.UnitTag == (int)UnitTypeTag.AllorsString)
                                {
                                    if (!loadUnits.Execute(a, String.Empty))
                                    {
                                        OnRelationNotLoaded(relationType.Id, a.ToString(), String.Empty);
                                    }
                                }
                                else
                                {
                                    OnRelationNotLoaded(relationType.Id, a.ToString(), String.Empty);
                                }
                            }
                            else
                            {
                                string value = reader.ReadString();
                                try
                                {
                                    UnitTypeTag unitTypeTag = (UnitTypeTag)relationType.RoleType.ObjectType.UnitTag;
                                    object unit = Serialization.XmlConvertFrom(value, unitTypeTag);
                                    if (!loadUnits.Execute(a, unit))
                                    {
                                        OnRelationNotLoaded(relationType.Id, a.ToString(), value);
                                    }
                                }
                                catch
                                {
                                    OnRelationNotLoaded(relationType.Id, a.ToString(), value);
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
            if (ObjectNotLoaded != null)
            {
                ObjectNotLoaded(this, new ObjectNotLoadedEventArgs(metaTypeId, allorsObjectId));
            }
            else
            {
                throw new Exception("Object not loaded: " + metaTypeId + ":" + allorsObjectId);
            }
        }

        private void OnRelationNotLoaded(Guid metaRelationId, string associationObjectId, string roleContents)
        {
            RelationNotLoadedEventArgs args = new RelationNotLoadedEventArgs(metaRelationId, associationObjectId, roleContents);
            if (RelationNotLoaded != null)
            {
                RelationNotLoaded(this, args);
            }
            else
            {
                throw new Exception("RelationType not loaded: " + args);
            }
        }

        private void SaveAllors(XmlWriter writer)
        {
            bool writeDocument = false;
            if (writer.WriteState == WriteState.Start)
            {
                writer.WriteStartDocument();
                writeDocument = true;
            }
            writer.WriteStartElement(Serialization.Allors);
            writer.WriteAttributeString(Serialization.Version, Serialization.VersionCurrent);

            SavePopulation(writer);

            writer.WriteEndElement();
            if (writeDocument)
            {
                writer.WriteEndDocument();
            }
        }

        private void SaveObjects(XmlWriter writer)
        {
            foreach (ObjectType type in Domain.ConcreteCompositeTypes)
            {
                bool atLeastOne = false;

                string sql = "SELECT " + Schema.O + "\n";
                sql += "FROM " + Schema.OC + "\n";
                sql += "WHERE " + Schema.C + "=" + Schema.C.Param;

                using (AllorsCommand command = CreateCommand(sql))
                {
                    command.AddInParameter(Schema.C.Param.Name, type.Id);

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

                            long id = Int64.Parse(reader[0].ToString());

                            writer.WriteString(id.ToString());
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

        private void SavePopulation(XmlWriter writer)
        {
            writer.WriteStartElement(Serialization.Population);
            writer.WriteAttributeString(Serialization.Mode, Serialization.ModeConnected);

            writer.WriteStartElement(Serialization.Objects);
            SaveObjects(writer);
            writer.WriteEndElement();

            writer.WriteStartElement(Serialization.Relations);
            SaveRelations(writer);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private void SaveRelations(XmlWriter writer)
        {
            foreach (RelationType relation in Domain.RelationTypes)
            {
                if (relation.AssociationType.ObjectType.ConcreteClasses.Length > 0)
                {
                    RoleType role = relation.RoleType;

                    string sql = "";
                    //TODO: ORDER BY ???
                    sql += "SELECT " + Schema.A + "," + Schema.R + "\n";
                    sql += "FROM " + Schema.Table(relation) + "\n";
                    sql += "ORDER BY " + Schema.A + "\n";

                    using (AllorsCommand command = CreateCommand(sql))
                    {
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            if (role.IsMany)
                            {
                                using (RelationTypeManyXmlWriter relationTypeManyXmlWriter = new RelationTypeManyXmlWriter(relation, writer))
                                {
                                    while (reader.Read())
                                    {
                                        long a = Int64.Parse(reader[Schema.A.Name].ToString());
                                        long r = Int64.Parse(reader[Schema.R.Name].ToString());
                                        relationTypeManyXmlWriter.Write(a, r);
                                    }
                                    relationTypeManyXmlWriter.Close();
                                }
                            }
                            else
                            {
                                using (RelationTypeOneXmlWriter relationTypeOneXmlWriter = new RelationTypeOneXmlWriter(relation, writer))
                                {
                                    while (reader.Read())
                                    {
                                        long a = Int64.Parse(reader[Schema.A.Name].ToString());

                                        if (role.ObjectType.IsUnit)
                                        {
                                            UnitTypeTag unitTypeTag = (UnitTypeTag)role.ObjectType.UnitTag;
                                            object r = command.GetValue(reader, unitTypeTag, reader.GetOrdinal(Schema.R.Name));
                                            string content = Serialization.XmlConvertTo(unitTypeTag, r);
                                            relationTypeOneXmlWriter.Write(a, content);
                                        }
                                        else
                                        {
                                            object r = reader[Schema.R.Name];
                                            relationTypeOneXmlWriter.Write(a, XmlConvert.ToString(Int64.Parse(r.ToString())));
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
    }
}