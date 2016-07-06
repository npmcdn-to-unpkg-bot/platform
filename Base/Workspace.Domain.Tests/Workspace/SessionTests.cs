namespace Tests
{
    using System;
    using System.Linq;

    using Allors.Workspace.Data;
    using Allors.Meta;
    using Allors.Workspace;
    using Allors.Workspace.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class SessionTests
    {
        [Test]
        public void UnitGet()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session = new Session(workspace);

            var koen = session.get(1) as Person;

            Assert.AreEqual("Koen", koen.FirstName);
            Assert.AreEqual(null, koen.MiddleName);
            Assert.AreEqual("Van Exem", koen.LastName);
            Assert.AreEqual(DateTime.Parse("1973-03-27T18:00:00Z"), koen.BirthDate);
            Assert.AreEqual(true, koen.IsStudent);

            var patrick = session.get(2) as Person;

            Assert.AreEqual("Patrick", patrick.FirstName);
            Assert.AreEqual("De Boeck", patrick.LastName);
            Assert.AreEqual(null, patrick.MiddleName);
            Assert.AreEqual(null, patrick.BirthDate);
            Assert.AreEqual(false, patrick.IsStudent);

            var martien = session.get(3) as Person;

            Assert.AreEqual("Martien", martien.FirstName);
            Assert.AreEqual("Knippenberg", martien.LastName);
            Assert.AreEqual("van", martien.MiddleName);
            Assert.AreEqual(null, martien.BirthDate);
            Assert.AreEqual(null, martien.IsStudent);
        }

        [Test]
        public void UnitSet()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session1 = new Session(workspace);
            var martien1 = session1.get(3) as Person;

            var session2 = new Session(workspace);
            var martien2 = session2.get(3) as Person;

            martien2.FirstName = "Martinus";
            martien2.MiddleName = "X";

            Assert.AreEqual("Martien", martien1.FirstName);
            Assert.AreEqual("Knippenberg", martien1.LastName);
            Assert.AreEqual("van", martien1.MiddleName);

            Assert.AreEqual("Martinus", martien2.FirstName);
            Assert.AreEqual("Knippenberg", martien2.LastName);
            Assert.AreEqual("X", martien2.MiddleName);
        }

        [Test]
        public void UnitSave()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session = new Session(workspace);
            var koen = session.get(1) as Person;
            var patrick = session.get(2) as Person;
            var martien = session.get(3) as Person;

            koen.FirstName = "K";
            koen.LastName = "VE";
            martien.FirstName = "Martinus";
            martien.MiddleName = "X";

            var save = session.pushRequest();

            Assert.AreEqual(2, save.objects.Count);

            var savedKoen = save.objects.First(v => (v.i == "1"));

            Assert.AreEqual("1001", savedKoen.v);
            Assert.AreEqual(2, savedKoen.roles.Length);

            var savedKoenFirstName = savedKoen.roles.First(v => v.t == "FirstName");
            var savedKoenLastName = savedKoen.roles.First(v => v.t == "LastName");

            Assert.AreEqual("K", savedKoenFirstName.s);
            Assert.IsNull(savedKoenFirstName.a);
            Assert.IsNull(savedKoenFirstName.r);
            Assert.AreEqual("VE", savedKoenLastName.s);
            Assert.IsNull(savedKoenLastName.a);
            Assert.IsNull(savedKoenLastName.r);

            var savedMartien = save.objects.First(v => v.i == "3");

            Assert.AreEqual("1003", savedMartien.v);
            Assert.AreEqual(2, savedMartien.roles.Length);

            var savedMartienFirstName = savedMartien.roles.First(v => v.t == "FirstName");
            var savedMartienMiddleName = savedMartien.roles.First(v => v.t == "MiddleName");

            Assert.AreEqual("Martinus", savedMartienFirstName.s);
            Assert.IsNull(savedMartienFirstName.a);
            Assert.IsNull(savedMartienFirstName.r);
            Assert.AreEqual("X", savedMartienMiddleName.s);
            Assert.IsNull(savedMartienMiddleName.a);
            Assert.IsNull(savedMartienMiddleName.r);
        }

        [Test]
        public void OneGet()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session = new Session(workspace);

            var koen = session.get(1) as Person;
            var patrick = session.get(2) as Person;
            var martien = session.get(3) as Person;

            var acme = session.get(101) as Organisation;
            var ocme = session.get(102) as Organisation;
            var icme = session.get(103) as Organisation;

            Assert.AreEqual(koen, acme.Owner);
            Assert.AreEqual(patrick, ocme.Owner);
            Assert.AreEqual(martien, icme.Owner);

            Assert.AreEqual(null, acme.Manager);
            Assert.AreEqual(null, ocme.Manager);
            Assert.AreEqual(null, icme.Manager);
        }

        [Test]
        public void OneSet()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session1 = new Session(workspace);

            var session2 = new Session(workspace);

            var koen1 = session1.get(1) as Person;
            var patrick1 = session1.get(2) as Person;
            var martien1 = session1.get(3) as Person;

            var acme1 = session1.get(101) as Organisation;
            var ocme1 = session1.get(102) as Organisation;
            var icme1 = session1.get(103) as Organisation;

            var koen2 = session2.get(1) as Person;
            var patrick2 = session2.get(2) as Person;
            var martien2 = session2.get(3) as Person;

            var acme2 = session2.get(101) as Organisation;
            var ocme2 = session2.get(102) as Organisation;
            var icme2 = session2.get(103) as Organisation;

            acme2.Owner = martien2;
            ocme2.Owner = null;
            acme2.Manager = patrick2;

            Assert.AreEqual(koen1, acme1.Owner);
            Assert.AreEqual(patrick1, ocme1.Owner);
            Assert.AreEqual(martien1, icme1.Owner);

            Assert.AreEqual(null, acme1.Manager);
            Assert.AreEqual(null, ocme1.Manager);
            Assert.AreEqual(null, icme1.Manager);

            Assert.AreEqual(martien2, acme2.Owner); //x
            Assert.AreEqual(null, ocme2.Owner);
            Assert.AreEqual(martien2, icme2.Owner);

            Assert.AreEqual(patrick2, acme2.Manager); //x
            Assert.AreEqual(null, ocme2.Manager);
            Assert.AreEqual(null, icme2.Manager);
        }

        [Test]
        public void OneSave()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session = new Session(workspace);

            var koen = session.get(1) as Person;
            var patrick = session.get(2) as Person;
            var martien = session.get(3) as Person;

            var acme = session.get(101) as Organisation;
            var ocme = session.get(102) as Organisation;
            var icme = session.get(103) as Organisation;

            acme.Owner = martien;
            ocme.Owner = null;

            acme.Manager = patrick;

            var save = session.pushRequest();

            Assert.AreEqual(2, save.objects.Count);

            var savedAcme = save.objects.First(v => v.i == "101");

            Assert.AreEqual("1101", savedAcme.v);
            Assert.AreEqual(2, savedAcme.roles.Length);

            var savedAcmeOwner = savedAcme.roles.First(v => v.t == "Owner");
            var savedAcmeManager = savedAcme.roles.First(v => v.t == "Manager");

            Assert.AreEqual("3", savedAcmeOwner.s);
            Assert.IsNull(savedAcmeOwner.a);
            Assert.IsNull(savedAcmeOwner.r);
            Assert.AreEqual("2", savedAcmeManager.s);
            Assert.IsNull(savedAcmeManager.a);
            Assert.IsNull(savedAcmeManager.r);

            var savedOcme = save.objects.First(v => v.i == "102");

            Assert.AreEqual("1102", savedOcme.v);
            Assert.AreEqual(1, savedOcme.roles.Length);

            var savedOcmeOwner = savedOcme.roles.First(v => v.t == "Owner");

            Assert.AreEqual(null, savedOcmeOwner.s);
            Assert.IsNull(savedOcmeOwner.a);
            Assert.IsNull(savedOcmeOwner.r);
        }

        [Test]
        public void ManyGet()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session = new Session(workspace);

            var koen = session.get(1) as Person;
            var patrick = session.get(2) as Person;
            var martien = session.get(3) as Person;

            var acme = session.get(101) as Organisation;
            var ocme = session.get(102) as Organisation;
            var icme = session.get(103) as Organisation;

            Assert.IsTrue(acme.Employees.SequenceEqual(new[] { koen, patrick, martien }));
            Assert.IsTrue(ocme.Employees.SequenceEqual(new[] { koen }));
            Assert.AreEqual(0, icme.Employees.Length);

            Assert.AreEqual(0, acme.Shareholders.Length);
            Assert.AreEqual(0, ocme.Shareholders.Length);
            Assert.AreEqual(0, icme.Shareholders.Length);
        }

        [Test]
        public void ManySet()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session1 = new Session(workspace);

            var session2 = new Session(workspace);

            var koen1 = session1.get(1) as Person;
            var patrick1 = session1.get(2) as Person;
            var martien1 = session1.get(3) as Person;

            var acme1 = session1.get(101) as Organisation;
            var ocme1 = session1.get(102) as Organisation;
            var icme1 = session1.get(103) as Organisation;

            var koen2 = session2.get(1) as Person;
            var patrick2 = session2.get(2) as Person;
            var martien2 = session2.get(3) as Person;

            var acme2 = session2.get(101) as Organisation;
            var ocme2 = session2.get(102) as Organisation;
            var icme2 = session2.get(103) as Organisation;

            acme2.Employees = null;
            icme2.Employees = new[] {koen2, patrick2, martien2};

            Assert.IsTrue(acme1.Employees.SequenceEqual(new[] { koen1, patrick1, martien1 }));
            Assert.IsTrue(ocme1.Employees.SequenceEqual(new[] { koen1 }));
            Assert.AreEqual(0, icme1.Employees.Count());

            Assert.AreEqual(0, acme2.Employees.Count());
            Assert.IsTrue(ocme2.Employees.SequenceEqual(new[] { koen2 }));
            Assert.IsTrue(icme2.Employees.SequenceEqual(new[] { koen2, patrick2, martien2 }));
        }

        [Test]
        public void ManySaveWithExistingObjects()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session = new Session(workspace);

            var koen = session.get(1) as Person;
            var patrick = session.get(2) as Person;
            var martien = session.get(3) as Person;

            var acme = session.get(101) as Organisation;
            var ocme = session.get(102) as Organisation;
            var icme = session.get(103) as Organisation;

            acme.Employees = null;
            ocme.Employees = new[] {martien, patrick};
            icme.Employees = new[] {koen, patrick, martien};

            var save = session.pushRequest();

            Assert.AreEqual(0, save.newObjects.Count);
            Assert.AreEqual(3, save.objects.Count);

            var savedAcme = save.objects.First(v => v.i == "101");

            Assert.AreEqual(savedAcme.v, "1101");
            Assert.AreEqual(savedAcme.roles.Length, 1);

            var savedAcmeEmployees = savedAcme.roles.First(v => v.t == "Employees");

            Assert.IsNull(savedAcmeEmployees.s);
            Assert.AreEqual(0, savedAcmeEmployees.a.Length);
            Assert.IsTrue(savedAcmeEmployees.r.SequenceEqual(new[] { "1", "2", "3" }));

            var savedOcme = save.objects.First(v => v.i == "102");

            Assert.AreEqual("1102", savedOcme.v);
            Assert.AreEqual(1, savedOcme.roles.Length);

            var savedOcmeEmployees = savedOcme.roles.First(v => v.t == "Employees");

            Assert.IsNull(savedOcmeEmployees.s);
            Assert.IsTrue(savedAcmeEmployees.a.SequenceEqual(new[] { "2", "3" }));
            Assert.IsTrue(savedAcmeEmployees.r.SequenceEqual(new[] { "1" }));

            var savedIcme = save.objects.First(v => v.i == "103");

            Assert.AreEqual("1103", savedIcme.v);
            Assert.AreEqual(1, savedIcme.roles.Length);

            var savedIcmeEmployees = savedIcme.roles.First(v => v.t == "Employees");

            Assert.IsNull(savedIcmeEmployees.s);
            Assert.IsTrue(savedAcmeEmployees.a.SequenceEqual(new[] { "1", "2", "3" }));
            Assert.IsNull(savedIcmeEmployees.r);
        }

        [Test]
        public void ManySaveWithNewObjects()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session = new Session(workspace);

            var martien = session.get(3) as Person;

            var mathijs = session.create(M.Person.Class) as Person;
            mathijs.FirstName = "Mathijs";
            mathijs.LastName = "Verwer";

            var acme2 = session.create(M.Organisation.Class) as Organisation;
            acme2.Name = "Acme 2";
            acme2.Manager = mathijs;
            acme2.AddEmployee(mathijs);

            var acme3 = session.create(M.Organisation.Class) as Organisation;
            acme3.Name = "Acme 3";
            acme3.Manager = martien;
            acme3.AddEmployee(martien);

            var save = session.pushRequest();

            Assert.AreEqual(3, save.newObjects.Count);
            Assert.AreEqual(0, save.objects.Count);
            {
                var savedMathijs = save.newObjects.First(v => v.ni == mathijs.newId?.ToString());

                Assert.AreEqual("Person", savedMathijs.t);
                Assert.AreEqual(2, savedMathijs.roles.Length);

                var savedMathijsFirstName = savedMathijs.roles.First(v => v.t == "FirstName");
                Assert.AreEqual("Mathijs", savedMathijsFirstName.s);

                var savedMathijsLastName = savedMathijs.roles.First(v => v.t == "LastName");
                Assert.AreEqual("Verwer", savedMathijsLastName.s);
            }

            {
                var savedAcme2 = save.newObjects.First(v => v.ni == acme2.newId?.ToString());

                Assert.AreEqual("Organisation", savedAcme2.t);
                Assert.AreEqual(3, savedAcme2.roles.Length);

                var savedAcme2Manager = savedAcme2.roles.First(v => v.t == "Manager");

                Assert.AreEqual(mathijs.newId, savedAcme2Manager.s);

                var savedAcme2Employees = savedAcme2.roles.First(v => v.t == "Employees");

                Assert.IsNull(savedAcme2Employees.s);
                Assert.IsTrue(savedAcme2Employees.a.SequenceEqual(new[] { mathijs.newId?.ToString()}));
                Assert.IsNull(savedAcme2Employees.r);
            }

            {
                var savedAcme3 = save.newObjects.First(v => v.ni == acme3.newId?.ToString());

                Assert.AreEqual("Organisation", savedAcme3.t);
                Assert.AreEqual(3, savedAcme3.roles.Length);

                var savedAcme3Manager = savedAcme3.roles.First(v => v.t == "Manager");

                Assert.AreEqual("3", savedAcme3Manager.s);

                var savedAcme3Employees = savedAcme3.roles.First(v => v.t == "Employees");

                Assert.IsNull(savedAcme3Employees.s);
                Assert.IsTrue(savedAcme3Employees.a.SequenceEqual(new[] { "3" }));
                Assert.IsNull(savedAcme3Employees.r);
            }

        }

        [Test]
        public void SyncWithNewObjects()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session = new Session(workspace);

            var martien = session.get(3) as Person;

            var mathijs = session.create(M.Person.Class) as Person;
            mathijs.FirstName = "Mathijs";
            mathijs.LastName = "Verwer";

            var acme2 = session.create(M.Organisation.Class) as Organisation;
            acme2.Name = "Acme 2";
            acme2.Owner = martien;
            acme2.Manager = mathijs;
            acme2.AddEmployee(martien);
            acme2.AddEmployee(mathijs);

            session.reset();

            Assert.IsNull(mathijs.id);
            Assert.IsTrue(mathijs.newId < 0);
            Assert.AreEqual(null, mathijs.FirstName);
            Assert.AreEqual(null, mathijs.LastName);

            Assert.IsNull(acme2.id);
            Assert.IsTrue(acme2.newId < 0);
            Assert.AreEqual(null, acme2.Owner);
            Assert.AreEqual(null, acme2.Manager);

            Assert.AreEqual(0, acme2.Employees.Count());
        }

        [Test]
        public void Onsaved()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var session = new Session(workspace);

            var saveResponse = new PushResponse { hasErrors = false };

            session.pushResponse(saveResponse);

            var mathijs = session.create(M.Person.Class) as Person;
            mathijs.FirstName = "Mathijs";
            mathijs.LastName = "Verwer";

            var newId = mathijs.newId.Value;

            saveResponse = new PushResponse
                               {
                                   hasErrors = false,
                                   newObjects = new[] { new PushResponseNewObject { i = "10000", ni = newId.ToString() } }
                               };

            session.pushResponse(saveResponse);

            Assert.IsNull(mathijs.newId);
            Assert.AreEqual(10000, mathijs.id);
            Assert.AreEqual("Person", mathijs.objectType.Name);

            mathijs = session.get(10000) as Person;

            Assert.IsNotNull(mathijs);

            var exceptionThrown = false;
            try
            {
                session.get(newId);
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        // TODO:

        //[Test]
        //public void methodCanExecute() {
        //    var workspace = new Workspace();
        //    workspace.sync(Fixture.loadData);

        //    var session = new Session(workspace);

        //    var acme = session.get("101") as Organisation;
        //    var ocme = session.get("102") as Organisation;
        //    var icme = session.get("102") as Organisation;

        //    Assert.IsTrue(acme.CanExecuteJustDoIt);
        //    this.isFalse(ocme.CanExecuteJustDoIt);
        //    this.isFalse(icme.CanExecuteJustDoIt);
        //}
    }
}