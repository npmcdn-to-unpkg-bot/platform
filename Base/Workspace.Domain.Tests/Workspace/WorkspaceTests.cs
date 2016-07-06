namespace Tests
{
    using Allors.Data;
    using Allors.Workspace;
    using NUnit.Framework;

    [TestFixture]
    public class WorkspaceTests
    {
        [Test]
        public void Load()
        {
            var workspace = new Workspace(Config.ObjectFactory);
            workspace.sync(Fixture.loadData);

            var martien = workspace.get("3");

            Assert.AreEqual("3", martien.id);
            Assert.AreEqual("1003", martien.version);
            Assert.AreEqual("Person", martien.objectType.Name);
            Assert.AreEqual("Martien", martien.roles["FirstName"]);
            Assert.AreEqual("van", martien.roles["MiddleName"]);
            Assert.AreEqual("Knippenberg", martien.roles["LastName"]);
            Assert.IsFalse(martien.roles.ContainsKey("IsStudent"));
            Assert.IsFalse(martien.roles.ContainsKey("BirthDate"));
        }

        [Test]
        public void CheckVersions()
        {
            var workspace = new Workspace(Config.ObjectFactory)
                                {
                                    userSecurityHash = "#"
                                };
            workspace.sync(Fixture.loadData);

            var required = new PullResponse
                               {
                                   userSecurityHash = "#",
                                   objects =
                                       new[]
                                           {
                                                new[] { "1", "1001" },
                                                new[] { "2", "1002" },
                                                new[] { "3", "1004" }
                                           }
                               };

            var requireLoad = workspace.diff(required);

            Assert.AreEqual(1, requireLoad.objects.Length);
        }

        [Test]
        public void CheckVersionsUserSecurityHash()
        {
            var workspace = new Workspace(Config.ObjectFactory) { userSecurityHash = "abc" };
            workspace.sync(Fixture.loadData);

            var required = new PullResponse
                               {
                                   userSecurityHash = "def",
                                   objects =
                                       new[]
                                           {
                                                new[] { "1", "1001" },
                                                new[] { "2", "1002" },
                                                new[] { "3", "1004" }
                                           }
                               };

            var requireLoad = workspace.diff(required);

            Assert.AreEqual(3, requireLoad.objects.Length);
        }
    }
}