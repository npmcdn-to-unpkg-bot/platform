namespace Tests
{
    using Allors.Data;
    using Allors.Workspace;

    using NUnit.Framework;

    [TestFixture]
    public class WorkspaceTests
    {
        [Test]
        public void load() {
            var workspace = new Workspace();
            workspace.sync(Fixture.loadData);

            var martien = workspace.get("3");

            Assert.AreEqual("3", martien.id);
            Assert.AreEqual("1003", martien.version);
            Assert.AreEqual("Person", martien.objectType.Name);
            Assert.AreEqual("Martien", martien.roles["FirstName"]);
            Assert.AreEqual("van", martien.roles["MiddleName"]);
            Assert.AreEqual("Knippenberg", martien.roles["LastName"]);
            Assert.IsNull(martien.roles["IsStudent"]);
            Assert.IsNull(martien.roles["BirthDate"]);
        }

        [Test]
        public void checkVersions() {
            var workspace = new Workspace();
            workspace.userSecurityHash = "#";
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
        public void checkVersionsUserSecurityHash() {
            var workspace = new Workspace();
            workspace.userSecurityHash = "abc";
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