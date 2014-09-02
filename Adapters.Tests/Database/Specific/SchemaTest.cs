// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SchemaTest.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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

namespace Allors.Adapters.Special
{
    using System;

    using Allors;
    using Allors.Adapters.Database.Sql;
    using Allors.Meta;

    using NUnit.Framework;

    using Environment = Allors.Meta.Environment;
    using IDatabase = IDatabase;

    public abstract class SchemaTest
    {
        private Domain domain;

        protected virtual bool DetectBinarySizedDifferences
        {
            get { return true; }
        }

        protected abstract IProfile Profile { get; }

        protected ISession Session
        {
            get
            {
                return this.Profile.Session;
            }
        }

        protected Action[] Markers
        {
            get
            {
                return this.Profile.Markers;
            }
        }

        protected Action[] Inits
        {
            get
            {
                return this.Profile.Inits;
            }
        }

        [Test]
        public void InitAndCreateSession()
        {
            this.DropTable("C1");
            this.DropTable("C2");

            this.domain = new Domain(new Environment(), Guid.NewGuid()) { Name = "MyDomain" };

            this.CreateClass("C1");

            var database = this.CreateDatabase(this.domain.Environment, true);
            ISession session = database.CreateSession();
            session.Rollback();

            this.CreateClass("C2");

            database = this.CreateDatabase(this.domain.Environment, true);
            session = database.CreateSession();
            session.Rollback();
        }

        [Test]
        [ExpectedException]
        public void InitInvalidDomain()
        {
            this.domain = new Domain(new Environment(), Guid.NewGuid()) { Name = "MyDomain" };

            new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            this.CreateDatabase(this.domain.Environment, true);
        }

        [Test]
        public void SystemTables()
        {
            Assert.IsTrue(this.IsUnique("_O", "T"));
            Assert.IsTrue(this.IsInteger("_O", "C"));

            Assert.IsTrue(this.ExistPrimaryKey("_O", "O"));
            Assert.IsFalse(this.ExistIndex("_O", "T"));
            Assert.IsFalse(this.ExistIndex("_O", "C"));
        }

        [Test]
        public void ValidateBinaryRelationDifferentSize()
        {
            if (this.DetectBinarySizedDifferences)
            {
                var environment = new Environment();
                var core = Repository.Core(environment);
                this.domain = new Domain(environment, Guid.NewGuid()) { Name = "MyDomain", DirectSuperdomains = { core } };

                var c1 = this.CreateClass("C1");
                this.CreateClass("C2");

                var allorsBinary = (ObjectType)this.domain.Environment.Find(UnitIds.BinaryId);
                var c1RelationType = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid())
                                         {
                                             AssociationType = { ObjectType = c1 },
                                             RoleType = { ObjectType = allorsBinary, Size = 200 }
                                         };

                this.CreateDatabase(this.domain.Environment, true);

                c1RelationType.RoleType.Size = 300;

                var database = this.CreateDatabase(this.domain.Environment, false);

                var validationErrors = this.GetSchemaValidation(database);

                var tableErrors = validationErrors.TableErrors;

                Assert.AreEqual(1, tableErrors.Length);
                Assert.AreEqual(SchemaValidationErrorKind.Incompatible, tableErrors[0].Kind);

                var error = tableErrors[0];

                Assert.AreEqual(null, error.ObjectType);
                Assert.AreEqual(null, error.RelationType);
                Assert.AreEqual(c1RelationType.RoleType, error.Role);

                Assert.AreEqual("c1", error.TableName);
                Assert.AreEqual("allorsbinary", error.ColumnName);
            }
        }

        [Test]
        public void ValidateDecimalRelationDifferentPrecision()
        {
            var environment = new Environment();
            var core = Repository.Core(environment);
            this.domain = new Domain(environment, Guid.NewGuid()) { Name = "MyDomain", DirectSuperdomains = { core } };

            var c1 = this.CreateClass("C1");
            this.CreateClass("C2");

            var c1RelationType = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            c1RelationType.AssociationType.ObjectType = c1;
            c1RelationType.RoleType.ObjectType = (ObjectType)this.domain.Environment.Find(UnitIds.DecimalId);
            c1RelationType.RoleType.Precision = 10;
            c1RelationType.RoleType.Scale = 2;

            this.CreateDatabase(this.domain.Environment, true);

            c1RelationType.RoleType.Precision = 11;

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);

            var tableErrors = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErrors.Length);
            Assert.AreEqual(SchemaValidationErrorKind.Incompatible, tableErrors[0].Kind);

            var error = tableErrors[0];

            Assert.AreEqual(null, error.ObjectType);
            Assert.AreEqual(null, error.RelationType);
            Assert.AreEqual(c1RelationType.RoleType, error.Role);

            Assert.AreEqual("c1", error.TableName);
            Assert.AreEqual("allorsdecimal", error.ColumnName);
        }

        [Test]
        public void ValidateDecimalRelationDifferentScale()
        {
            var environment = new Environment();
            var core = Repository.Core(environment);
            this.domain = new Domain(environment, Guid.NewGuid()) { Name = "MyDomain", DirectSuperdomains = { core } }; 
            
            var c1 = this.CreateClass("C1");
            this.CreateClass("C2");

            var c1RelationType = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            c1RelationType.AssociationType.ObjectType = c1;
            c1RelationType.RoleType.ObjectType = (ObjectType)this.domain.Environment.Find(UnitIds.DecimalId);
            c1RelationType.RoleType.Precision = 10;
            c1RelationType.RoleType.Scale = 2;

            this.CreateDatabase(this.domain.Environment, true);

            c1RelationType.RoleType.Scale = 3;

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);

            var tableErrors = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErrors.Length);
            Assert.AreEqual(SchemaValidationErrorKind.Incompatible, tableErrors[0].Kind);

            var error = tableErrors[0];

            Assert.AreEqual(null, error.ObjectType);
            Assert.AreEqual(null, error.RelationType);
            Assert.AreEqual(c1RelationType.RoleType, error.Role);

            Assert.AreEqual("c1", error.TableName);
            Assert.AreEqual("allorsdecimal", error.ColumnName);
        }

        [Test]
        public void ValidateNewConcreteClass()
        {
            this.DropTable("C1");
            this.DropTable("C2");

            this.domain = new Domain(new Environment(), Guid.NewGuid()) { Name = "MyDomain" };
            this.domain.Name = "MyDomain";

            this.CreateClass("C1");

            this.CreateDatabase(this.domain.Environment, true);

            this.CreateClass("C2");

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);

            var tableErrors = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErrors.Length);

            var error = tableErrors[0];

            Assert.AreEqual("c2", error.TableName);
            Assert.AreEqual(SchemaValidationErrorKind.Missing, tableErrors[0].Kind);
        }

        [Test]
        public void ValidateNewInterfaceInheritanceWithBooleanRelation()
        {
            var environment = new Environment();
            var core = Repository.Core(environment);
            this.domain = new Domain(environment, Guid.NewGuid()) { Name = "MyDomain", DirectSuperdomains = { core } };

            var c1 = this.CreateClass("C1");
            var c2 = this.CreateClass("C2");

            var i12 = this.CreateInterface("I12");

            var i12AllorsString = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            i12AllorsString.AssociationType.ObjectType = i12;
            i12AllorsString.RoleType.ObjectType = (ObjectType)this.domain.Environment.Find(UnitIds.BooleanId);

            new Inheritance(this.domain, Guid.NewGuid()) { Subtype = c1, Supertype = i12 };
 
            this.CreateDatabase(this.domain.Environment, true);

            new Inheritance(this.domain, Guid.NewGuid()) { Subtype = c2, Supertype = i12 };

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);

            var tableErrors = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErrors.Length);

            var error = tableErrors[0];

            Assert.AreEqual("c2", error.TableName);
            Assert.AreEqual("allorsboolean", error.ColumnName);
            Assert.AreEqual(SchemaValidationErrorKind.Missing, tableErrors[0].Kind);
        }

        [Test]
        public void ValidateNewMany2ManyRelation()
        {
            this.domain = new Domain(new Environment(), Guid.NewGuid()) { Name = "MyDomain" };

            var c1 = this.CreateClass("C1");
            var c2 = this.CreateClass("C2");

            this.CreateDatabase(this.domain.Environment, true);

            var fromC1ToC2 = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            fromC1ToC2.AssociationType.ObjectType = c1;
            fromC1ToC2.AssociationType.IsMany = true;
            fromC1ToC2.RoleType.ObjectType = c2;
            fromC1ToC2.RoleType.IsMany = true;

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);

            var tableErros = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErros.Length);
            Assert.AreEqual(SchemaValidationErrorKind.Missing, tableErros[0].Kind);

            var error = tableErros[0];

            Assert.AreEqual(null, error.ObjectType);
            Assert.AreEqual(fromC1ToC2, error.RelationType);
            Assert.AreEqual(null, error.Role);

            Assert.AreEqual("c1c2", error.TableName);
            Assert.AreEqual(null, error.ColumnName);
        }

        [Test]
        public void ValidateNewMany2OneRelation()
        {
            this.domain = new Domain(new Environment(), Guid.NewGuid()) { Name = "MyDomain" };

            var c1 = this.CreateClass("C1");
            var c2 = this.CreateClass("C2");

            this.CreateDatabase(this.domain.Environment, true);

            var fromC1ToC2 = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            fromC1ToC2.AssociationType.ObjectType = c1;
            fromC1ToC2.AssociationType.IsMany = true;
            fromC1ToC2.RoleType.ObjectType = c2;

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);
            var tableErros = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErros.Length);
            Assert.AreEqual(SchemaValidationErrorKind.Missing, tableErros[0].Kind);

            var error = tableErros[0];

            Assert.AreEqual(null, error.ObjectType);
            Assert.AreEqual(null, error.RelationType);
            Assert.AreEqual(fromC1ToC2.RoleType, error.Role);
            Assert.AreEqual("c1", error.TableName);
            Assert.AreEqual("c2", error.ColumnName);
        }

        [Test]
        public void ValidateNewOne2ManyRelation()
        {
            this.domain = new Domain(new Environment(), Guid.NewGuid()) { Name = "MyDomain" };

            var c1 = this.CreateClass("C1");
            var c2 = this.CreateClass("C2");

            this.CreateDatabase(this.domain.Environment, true);

            var fromC1ToC2 = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            fromC1ToC2.AssociationType.ObjectType = c1;
            fromC1ToC2.RoleType.ObjectType = c2;
            fromC1ToC2.RoleType.IsMany = true;

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);

            var tableErrors = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErrors.Length);
            Assert.AreEqual(SchemaValidationErrorKind.Missing, tableErrors[0].Kind);

            var error = tableErrors[0];

            Assert.AreEqual(null, error.ObjectType);
            Assert.AreEqual(null, error.RelationType);
            Assert.AreEqual(fromC1ToC2.RoleType, error.Role);
            Assert.AreEqual("c2", error.TableName);
            Assert.AreEqual("c2c1", error.ColumnName);
        }

        [Test]
        public void ValidateNewOne2OneRelation()
        {
            this.domain = new Domain(new Environment(), Guid.NewGuid()) { Name = "MyDomain" };

            var c1 = this.CreateClass("C1");
            var c2 = this.CreateClass("C2");

            this.CreateDatabase(this.domain.Environment, true);

            var fromC1ToC2 = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            fromC1ToC2.AssociationType.ObjectType = c1;
            fromC1ToC2.RoleType.ObjectType = c2;

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);

            var tableErrors = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErrors.Length);
            Assert.AreEqual(SchemaValidationErrorKind.Missing, tableErrors[0].Kind);

            var error = tableErrors[0];

            Assert.AreEqual(null, error.ObjectType);
            Assert.AreEqual(null, error.RelationType);
            Assert.AreEqual(fromC1ToC2.RoleType, error.Role);
            Assert.AreEqual("c1", error.TableName);
            Assert.AreEqual("c2", error.ColumnName);
        }

        [Test]
        public void ValidateStringRelationDifferentSize()
        {
            var environment = new Environment();
            var core = Repository.Core(environment);
            this.domain = new Domain(environment, Guid.NewGuid()) { Name = "MyDomain", DirectSuperdomains = { core } };

            var c1 = this.CreateClass("C1");
            this.CreateClass("C2");

            var c1RelationType = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            c1RelationType.AssociationType.ObjectType = c1;
            c1RelationType.RoleType.ObjectType = (ObjectType)this.domain.Environment.Find(UnitIds.StringId);
            c1RelationType.RoleType.Size = 100;

            this.CreateDatabase(this.domain.Environment, true);

            c1RelationType.RoleType.Size = 101;

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);

            var tableErrors = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErrors.Length);
            Assert.AreEqual(SchemaValidationErrorKind.Incompatible, tableErrors[0].Kind);

            var error = tableErrors[0];

            Assert.AreEqual(null, error.ObjectType);
            Assert.AreEqual(null, error.RelationType);
            Assert.AreEqual(c1RelationType.RoleType, error.Role);

            Assert.AreEqual("c1", error.TableName);
            Assert.AreEqual("allorsstring", error.ColumnName);
        }

        [Test]
        public void ValidateStringToOne2One()
        {
            var environment = new Environment();
            var core = Repository.Core(environment);
            this.domain = new Domain(environment, Guid.NewGuid()) { Name = "MyDomain", DirectSuperdomains = { core } };

            var c1 = this.CreateClass("C1");
            var c2 = this.CreateClass("C2");

            var c1RelationType = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            c1RelationType.AssociationType.ObjectType = c1;
            c1RelationType.RoleType.ObjectType = (ObjectType)this.domain.Environment.Find(UnitIds.StringId);
            c1RelationType.RoleType.Size = 100;
            c1RelationType.RoleType.AssignedSingularName = "RelationType";
            c1RelationType.RoleType.AssignedPluralName = "RelationTypes";

            this.CreateDatabase(this.domain.Environment, true);

            c1RelationType.RoleType.Size = null;
            c1RelationType.RoleType.ObjectType = c2;

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);

            var tableErrors = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErrors.Length);
            Assert.AreEqual(SchemaValidationErrorKind.Incompatible, tableErrors[0].Kind);

            var error = tableErrors[0];

            Assert.AreEqual(null, error.ObjectType);
            Assert.AreEqual(null, error.RelationType);
            Assert.AreEqual(c1RelationType.RoleType, error.Role);

            Assert.AreEqual("c1", error.TableName);
            Assert.AreEqual("relationtype", error.ColumnName);
        }

        [Test]
        public void ValidateUnitRelationDifferentType()
        {
            var environment = new Environment();
            var core = Repository.Core(environment);
            this.domain = new Domain(environment, Guid.NewGuid()) { Name = "MyDomain", DirectSuperdomains = { core } };

            var c1 = this.CreateClass("C1");
            this.CreateClass("C2");

            var c1RelationType = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            c1RelationType.AssociationType.ObjectType = c1;
            c1RelationType.RoleType.ObjectType = (ObjectType)this.domain.Environment.Find(UnitIds.BooleanId);
            c1RelationType.RoleType.AssignedSingularName = "RelationType";
            c1RelationType.RoleType.AssignedPluralName = "RelationTypes";

            this.CreateDatabase(this.domain.Environment, true);

            c1RelationType.RoleType.ObjectType = (ObjectType)this.domain.Environment.Find(UnitIds.Unique);

            var database = this.CreateDatabase(this.domain.Environment, false);

            var validationErrors = this.GetSchemaValidation(database);

            var tableErrors = validationErrors.TableErrors;

            Assert.AreEqual(1, tableErrors.Length);
            Assert.AreEqual(SchemaValidationErrorKind.Incompatible, tableErrors[0].Kind);

            var error = tableErrors[0];

            Assert.AreEqual(null, error.ObjectType);
            Assert.AreEqual(null, error.RelationType);
            Assert.AreEqual(c1RelationType.RoleType, error.Role);

            Assert.AreEqual("c1", error.TableName);
            Assert.AreEqual("relationtype", error.ColumnName);
        }

        [Test]
        public void IndexesMany2Many()
        {
            this.CreateDatabase().Init();

            Assert.IsTrue(this.ExistIndex("CompanyIndexedMany2ManyPerson", "R"));
            Assert.IsFalse(this.ExistIndex("CompanyMany2ManyPerson", "R"));
        }

        [Test]
        public void IndexesUnits()
        {
            this.CreateDatabase().Init();

            Assert.IsTrue(this.ExistIndex("C1", "C1AllorsInteger"));
            Assert.IsFalse(this.ExistIndex("C1", "C1AllorsString"));
        }

        [Test]
        public void IncompatiblePopulation()
        {
            this.DropTable("C1");
            this.DropTable("C2");

            this.domain = new Domain(new Environment(), Guid.NewGuid()) { Name = "MyDomain" };

            var c1 = this.CreateClass("C1");
            var c2 = this.CreateClass("C2");

            this.CreateDatabase(this.domain.Environment, true);

            var c1AllorsString = new RelationType(this.domain, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            c1AllorsString.AssociationType.ObjectType = c1;
            c1AllorsString.RoleType.ObjectType = c2;

            var database = this.CreateDatabase(this.domain.Environment, false);

            ISession session = null;
            try
            {
                session = database.CreateSession();
                Assert.Fail();
            }
            catch (SchemaValidationException e)
            {
                var validationErrors = e.ValidationErrors;
                Assert.IsTrue(validationErrors.HasErrors);
            }
            finally
            {
                if (session != null)
                {
                    session.Rollback();
                }
            }
        }
        
        protected Class CreateClass(string name)
        {
            return new Class(this.domain, Guid.NewGuid()) { SingularName = name, PluralName = name + "s" };
        }

        protected Interface CreateInterface(string name)
        {
            return new Interface(this.domain, Guid.NewGuid()) { SingularName = name, PluralName = name + "s" };
        }

        protected abstract IDatabase CreateDatabase(Environment environment, bool init);

        protected IDatabase CreateDatabase()
        {
            return this.Profile.CreateDatabase();
        }

        protected abstract void DropTable(string tableName);

        protected abstract void DropProcedure(string procedure);

        protected abstract bool ExistProcedure(string procedure);

        protected abstract bool ExistPrimaryKey(string table, string column);

        protected abstract bool ExistIndex(string table, string column);

        protected abstract bool IsInteger(string table, string column);

        protected abstract bool IsLong(string table, string column);

        protected abstract bool IsUnique(string table, string column);

        protected abstract SchemaValidationErrors GetSchemaValidation(IDatabase database);
    }

    public abstract class SchemaIntegerIdTest : SchemaTest
    {
        [Test]
        public void AssertIntegerIds()
        {
            Assert.IsTrue(this.IsInteger("_O", "O"));
        }
    }

    public abstract class SchemaLongIdTest : SchemaTest
    {
        [Test]
        public void AssertLongIds()
        {
            Assert.IsTrue(this.IsLong("_O", "O"));
        }
    }
}